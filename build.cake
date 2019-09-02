#tool nuget:?package=ReportGenerator&version=4.2.15

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var outputPath = Argument<DirectoryPath>("OutputPath", "output");
var codeCoveragePath = Argument<DirectoryPath>("CodeCoveragePath", "output/coverage");
var solutionFile = Argument("solutionFile", "dotnet-base.sln");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

// Target : Clean
// 
// Description
// Clean all directories used by build artifacts.
Task("Clean")
    .Does(() =>
{
    CleanDirectory(outputPath);
    CleanDirectory(codeCoveragePath);

    // remove all binaries in source files
    var srcBinDirectories = GetDirectories("./src/**/bin");
    foreach(var directory in srcBinDirectories)
    {
        CleanDirectory(directory);
    }

    // remove all binaries in test files
    var testsBinDirectories = GetDirectories("./tests/**/bin");
    foreach(var directory in testsBinDirectories)
    {
        CleanDirectory(directory);
    }
});

// Target : Restore-NuGet-Packages
// 
// Description
// Restores all needed NuGet packages for projects.
Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    // https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore
    //
    // Reload all nuget packages used by the solution
    NuGetRestore(solutionFile);
});

// Target : Build
// 
// Description
// Creates the artifacts.
Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
    
      // Use MSBuild
      MSBuild(solutionFile, settings =>
        settings.SetConfiguration(configuration));
    
    } else {
    
      // Use XBuild
      XBuild(solutionFile, settings =>
        settings.SetConfiguration(configuration));

    }
});

// Target : Test-With-Coverage
// 
// Beschreibung
// Führt alle gefundenen Unit Tests in der Solution aus.
Task("Test-With-Coverage")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projectFiles = GetFiles("./tests/**/*Tests.csproj");
   
    foreach(var file in projectFiles)
    {
        var coverageFile = file.GetFilenameWithoutExtension() + ".cobertura.xml";
        var coveragePath = MakeAbsolute(codeCoveragePath).CombineWithFilePath(coverageFile);

        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            ArgumentCustomization = args => args
                .Append("/p:CollectCoverage=true")
                .Append("/p:CoverletOutputFormat=cobertura")
                .Append("/p:CoverletOutput=" + coveragePath.FullPath)
        };
        DotNetCoreTest(file.FullPath, settings);
    }

    var project35Tests  = GetFiles("./tests/*Tests35/bin/*/*Tests35.dll");
    foreach(var file35 in project35Tests)
    {
        var dir35 = file35.GetDirectory();
        VSTest(file35.FullPath, new VSTestSettings() 
        { 
            // https://github.com/cake-build/cake/issues/2077
            #tool Microsoft.TestPlatform
            ToolPath = Context.Tools.Resolve("vstest.console.exe"),
            InIsolation = true,
            FrameworkVersion = VSTestFrameworkVersion.NET35
        });
    }

    ReportGenerator( 
        MakeAbsolute(codeCoveragePath).FullPath + "/*.cobertura.xml", 
        MakeAbsolute(codeCoveragePath).FullPath + "/report",
        new ReportGeneratorSettings(){
            ReportTypes = new[] { 
                ReportGeneratorReportType.HtmlInline,
                ReportGeneratorReportType.Badges 
            }
        }
    );
});

// Target : Build
// 
// Description
// Setup the default task.
Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
