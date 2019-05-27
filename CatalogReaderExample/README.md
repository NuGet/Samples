# Catalog Reader Examples

These are the code samples from the guide on how to [query for all packages published to nuget.org](https://docs.microsoft.com/en-us/nuget/guides/api/query-for-all-published-packages) using the [catalog resource](https://docs.microsoft.com/en-us/nuget/api/catalog-resource). These are the interesting folders:

* `NuGet.Protocol.Catalog` - The pre-release catalog SDK. This package is available on the `nuget-build` MyGet feed, for which you can use the NuGet package source URL `https://dotnet.myget.org/F/nuget-build/api/v3/index.json`. You can install this package to a project compatible with `netstandard1.3` or greater (such as .NET Framework 4.6).
* `NuGet.Protocol.Catalog.Sample` - A sample using the pre-release catalog SDK.
* `CatalogReaderExample` - A minimal sample with fewer dependencies that illustrates the interaction with the catalog in more detail.