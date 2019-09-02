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
