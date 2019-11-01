ExamplePackage.WithContentFiles-nuspec\
    contains content and a.nuspec to create nupkg v1.0.0 via nuget.exe pack a.nuspec

ExamplePackage.WithContentFiles\
    contains content and a csproj to create nupkg v2.0.0 via `dotnet pack` or `msbuild /t:pack`

MyUWPApp contains an example project that uses contentFiles from ContentFilesExample 1.0.0.

To build and pack all projects:
1) Run "powershell .\pack.ps1" to create Packages\ContentFilesExample1.0.0.nupkg
2) Run "msbuild /restore ContentFilesExample.sln"
3) RUn "msbuild /t:pack ContentFilesExample.sln"
3) Upgrade MyUWPApp to use v2 of the package (build via the sdk style project, not the nuspec)




