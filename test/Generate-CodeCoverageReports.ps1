<#
.SYNOPSIS
This is a Powershell script to run tests and build code coverage reports.

.PARAMETER Tag
The Report Tag.

.PARAMETER CoverletOutput
.
#>

[CmdletBinding()]
 Param(
    [string]$Tag = "Manual"
)

ForEach ($projectPath in (Get-ChildItem -recurse *Test.csproj)) {
	# Write-Host "Execute test project file $projectFile"
	$projectFolder = Split-Path -parent $projectPath
	$projectBasename = (Get-Item $projectPath).Basename
	dotnet test $projectPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=$projectFolder\..\..\TestResult\Coverage\$projectBasename
}

#
# dotnet tool install dotnet-reportgenerator-globaltool --tool-path ..\tools
#
$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
Start-Process -FilePath "$scriptPath\..\tools\reportgenerator.exe" -Wait -NoNewWindow -ArgumentList "-reports:$scriptPath\..\TestResult\Coverage\*.cobertura.xml", "-targetdir:$scriptPath\..\TestResult\Coverage\Reports", "-tag:$Tag", "-reportTypes:htmlInline"
