$NuGetExe = Join-Path $PSScriptRoot '.nuget\nuget.exe'

# Download NuGet.exe if missing
if (-not (Test-Path $NuGetExe)) {
    Write-Host 'Downloading nuget.exe'
    New-Item  -ItemType directory  (Join-Path $PSScriptRoot '.nuget') | Out-Null
    wget https://dist.nuget.org/win-x86-commandline/v4.4.1/nuget.exe -OutFile $NuGetExe
}

# Pack will copy all files in this folder into the nupkg
# The contentFiles section of the nuspec is used by the consuming client, not during pack.
& $NuGetExe pack XMLTransformExample.nuspec -Exclude pack.ps1