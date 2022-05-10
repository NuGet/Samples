.\nuget-6.1.0.exe restore NU1701Transitive.sln
dotnet list package --include-transitive
Remove-Item $(Join-Path "A" "obj" -Resolve)
.\nuget-6.2.0.exe restore NU1701Transitive.sln
dotnet list package --include-transitive