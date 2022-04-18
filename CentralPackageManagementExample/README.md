# Central Package Management (CPM) Example
This example shows how to use NuGet's central package management (CPM).  See our [official documentation](https://docs.microsoft.com/nuget/consume-packages/central-package-management) for more information.

## Requirements
This example will only work in Visual Studio 2022 17.2 Preview 3 or above.

## Specifying package versions
When using CPM, you cannot specify package versions in individual projects.  Instead, you specify `<PackageVersion />` items in a file named [`Directory.Packages.props`](Directory.Packages.props).


## Overridding centrally defined package versions
In some cases, you may want to override the version defined centrally.  This is useful for projects that aren't related to the rest of the repository or test utilities that aren't run as part of production.
To use a different package version, you must specify the `VersionOverride` metadata on a `<PackageReference />`.  See the [ClassLibraryWithVersionOverride](src/ClassLibraryWithVersionOverride/ClassLibraryWithVersionOverride.csproj) project
for an example.

## Pinning transitive package versions
Transitive package versions are defined by the graph of packages you consume.  The only way to override the version of a transitive package is to add a top-level package reference.  CPM introduces a way
of "pinning" transitive package versions.  You must enable this functionality by setting the MSBuild property `CentralPackageTransitivePinningEnabled` to `true`.

```xml
<PropertyGroup>
  <CentralPackageTransitivePinningEnabled>true</CentralPackageTransitivePinningEnabled>
</PropertyGroup>
```

If a `<PackageVersion />` is defined in [`Directory.Packages.props`](Directory.Packages.props) for a transitive dependency, that version will be used instead and that package is elevated to be
a top-level package reference.  

See the [ClassLibraryWithTransitivePinning](src/ClassLibraryWithTransitivePinning/ClassLibraryWithTransitivePinning.csproj) project
