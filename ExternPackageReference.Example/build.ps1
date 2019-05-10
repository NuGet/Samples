<#
.SYNOPSIS
Builds the packages with conflicting namespaces. 
Builds and runs a PackageReference based project (both SDK (.NET Core) & old style csproj)

.PARAMETER NetFX
By default this will run and build SDK based csproj. 
Pass this switch to build the old style csproj

#>
[CmdletBinding()]
param (
    [switch]$NetFX
)

# Set up the packages
dotnet pack ClassLib1\ClassLib1.csproj
dotnet pack ClassLib2\ClassLib2.csproj

# Run the app showing that methods from both namespaces are being invoked.
dotnet build ConsoleApp\ConsoleApp.csproj
dotnet exec ConsoleApp\bin\Debug\netcoreapp2.1\ConsoleApp.dll

# Run the app showing that methods from both namespaces are being invoked.
msbuild /restore /t:build ConsoleAppNetFX\ConsoleAppNetFX.csproj
ConsoleAppNetFX\bin\Debug\ConsoleApp5.exe