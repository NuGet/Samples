$NuGetExe610 = Join-Path $PSScriptRoot '.nuget\nuget-6.1.0.exe'
$NuGetExe620 = Join-Path $PSScriptRoot '.nuget\nuget-6.2.0.exe'

if (-not (Test-Path $NuGetExe610)) {
    Write-Host "Downloading NuGet.exe to $NuGetExe610" -ForegroundColor blue
    New-Item  -ItemType directory  (Join-Path $PSScriptRoot '.nuget') | Out-Null
    Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/v6.1.0/nuget.exe -OutFile $NuGetExe610
}

if (-not (Test-Path $NuGetExe620)) {
    Write-Host "Downloading NuGet.exe to $NuGetExe620" -ForegroundColor blue
    New-Item  -ItemType directory  (Join-Path $PSScriptRoot '.nuget') | Out-Null
    Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/v6.2.0/nuget.exe -OutFile $NuGetExe610
}

Remove-Item $(Join-Path "A" "obj" -Resolve) -Force -Recurse -Confirm:$false
Write-Host "Running restore with 6.1" -ForegroundColor green
. $NuGetExe610 restore NU1701Transitive.sln -Verbosity Quiet
Write-Host "Running dotnet list package" -ForegroundColor green
dotnet list package --include-transitive
Remove-Item $(Join-Path "A" "obj" -Resolve) -Force -Recurse -Confirm:$false
Write-Host "Running restore with 6.2" -ForegroundColor green
. $NuGetExe620 restore NU1701Transitive.sln -Verbosity Quiet
Write-Host "Running dotnet list package" -ForegroundColor green
dotnet list package --include-transitive