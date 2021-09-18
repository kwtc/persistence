Generate new nuget package (from PowerShell) with Nuke build automation
.\build.ps1 Pack

Publish nuget package
dotnet nuget push ".\output\Kwtc.Persistence.0.0.1.nupkg" --api-key {personal access token} --source "github"