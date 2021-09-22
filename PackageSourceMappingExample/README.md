# Package Source Mapping sample

This folder is an example demonstrating the [Package Source Mapping feature](https://devblogs.microsoft.com/nuget/introducing-package-source-mapping/) (initially proposed as "Package Namespaces").

In addition to the `nuget.config` file in this folder, you can also check out how we set up our own [`nuGet.config` for the NuGet.Client repo](https://github.com/NuGet/NuGet.Client/blob/dev-kartheekp-ms-dogfood-namespaces/NuGet.Config) to use this feature internally.

## Package Source Mapping tooling

This feature is compatible with the following tools:

*   [Visual Studio 2022 preview 4][1] and later
*   [nuget.exe 6.0.0-preview.4][5] and later
*   [.NET SDK 6.0.100-rc.1][6] and later

Older tooling will ignore the Package Source Mapping configuration. To use this feature, ensure all your build environments use compatible tooling versions.

Package Source Mappings will apply to all project types - including .NET Framework - as long as compatible tooling is used for build and restore.

Here's an [example](https://github.com/NuGet/NuGet.Client/blob/dev-kartheekp-ms-dogfood-namespaces/NuGet.Config) of how the NuGet.Config for the NuGet.Client repo looks like in our dogfooding efforts.
.