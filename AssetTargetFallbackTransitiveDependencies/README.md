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
A/project (net6.0) -> NuGet.PackageManagement/4.8.0 (net472) -> Microsoft.Web.Xdt/2.1.2 (net40)
```

Note that `NuGet.PackageManagement` has other dependencies as well, in addition to Microsoft.Web.Xdt.

Run the `.\run.ps1` script for a demo.

.\run.ps1 restores with 6.1.0 first, and then with 6.2.0 later and prints the packages after that.

Notice that 6.1.0 was not including all the necessary packages, but 6.2.0 is.

## What might I see

If you have a straightforward scenario like some of the above, you're likely to see more packages that should've downloaded before be downloaded now.
You may not need to do anything.

Depending on your scenario you may see one or more of the following:

* You may get more NU1701 warnings. This simply indicates that more packages that are not immediately compatible are getting restored.
* Other NU warnings and errors. Unfortunately when more packages are brought in a package graph, they may affect the resolution of previous packages. The coded NuGet warnings/errors usually have a root cause and wherever possible instructions how to address them.
If there are issues that you don't know how to resolve, consider filing a NuGet/Home issue or discussion.
* If you were aware of this issue existing in the first place, you may have added the transitive packages as top-level already. You might be able to remove those. (Un)fortunately you may not see any impact if you had already done that.

## ProjectReference and AssetTargetFallback

Note that the equivalent behavior of transitive dependencies applies to transitives through ProjectReference as well, whether those are packages or projects.
We don't expect this to be a common scenario, but the similar guidances apply.

## References

* Issue filled about consequences from this change: <https://github.com/NuGet/Home/issues/11564>
* Design document for the change: <https://github.com/NuGet/Home/blob/dev/proposed/2022/AssetTargetFallback-DependenciesResolution.md>
