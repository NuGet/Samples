# README

The following example describes the side-effects of the fix for the following bug, [https://github.com/NuGet/Home/issues/5957](Project A referencing package B via AssetTargetFallback, doesn't use that same AssetTargetFallback to pull B's dependency package C).

Up to 6.1, NuGet would not consider the transitive dependency for packages that were brought in through AssetTargetFallback. [Learn more about AssetTargetFallback](https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files#assettargetfallback)

## Minimal theoretical example

```console
A/project (net6.0) -> NETFrameworkPackage/2.0.0 -> NETFrameworkDependency/1.0.0
```

Note that NETFrameworkPackage is not directly compatible with project A. SDK style projects automatically set `AssetTargetFallback` allowing these packages to be installed, but with a warning.

If you were to install the NETFrameworkPackage package into A, restore with 6.1 and then ran `dotnet list package --include-transitive`, you'd get:

```console
Project 'A' has the following package references
   [net6.0]:
   Top-level Package              Requested   Resolved
   > NETFrameworkPackage          2.0.0       2.0.0
```

Note that the `NETFrameworkDependency` is not installed. This would usually lead to compile or runtime errors. Note that there's a chance that you might not have had *any* problems. The behavior is scenario dependent.

If you were to restore with 6.2 and then run `dotnet list package --include-transitive`, you'd get:

```console
Project 'A' has the following package references
   [net6.0]:
   Top-level Package              Requested   Resolved
   > NETFrameworkPackage          2.0.0       2.0.0

   Transitive Package                                                                   Resolved
   > NETFrameworkDependency                                                             1.0.0
```

Note that the dependency package is installed now.

## Practical example

```console
A/project (net6.0) -> NuGet.PackageManagement/4.8.0 (net472)
```

.\run.ps1 restores with 6.1.0 first, and then with 6.2.0 later and prints the packages after that.

Notice that 6.1.0 was not including all the necessary packages, but 6.2.0 is.
The transitive dependencies of NuGet.PackageManagement were not getting included in 6.1.0, but they are in 6.2.0 and they should have been.
