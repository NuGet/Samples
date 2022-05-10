Remove-Item $(Join-Path "A" "obj" -Resolve) -Force -Recurse -Confirm:$false
Write-Host "Running restore with 6.1" -ForegroundColor green
.\nuget-6.1.0.exe restore NU1701Transitive.sln -Verbosity Quiet
Write-Host "Running dotnet list package" -ForegroundColor green
dotnet list package --include-transitive
Remove-Item $(Join-Path "A" "obj" -Resolve) -Force -Recurse -Confirm:$false
Write-Host "Running restore with 6.2" -ForegroundColor green
.\nuget-6.2.0.exe restore NU1701Transitive.sln -Verbosity Quiet
Write-Host "Running dotnet list package" -ForegroundColor green
dotnet list package --include-transitive