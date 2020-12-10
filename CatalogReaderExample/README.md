# Catalog Reader Examples

These are the code samples from the guide on how to [query for all packages published to nuget.org](https://docs.microsoft.com/en-us/nuget/guides/api/query-for-all-published-packages) using the [catalog resource](https://docs.microsoft.com/en-us/nuget/api/catalog-resource). These are the interesting folders:

* `NuGet.Protocol.Catalog.Sample` - A sample using the pre-release catalog SDK, [`NuGet.Protocol.Catalog`](https://github.com/NuGet/NuGet.Jobs/tree/master/src/NuGet.Protocol.Catalog), which is available on Azure Artifacts using the following NuGet package source URL: `https://pkgs.dev.azure.com/dnceng/public/_packaging/nuget-build/nuget/v3/index.json`.
* `CatalogReaderExample` - A minimal sample with fewer dependencies that illustrates the interaction with the catalog in greater detail.

We recommend you start with `NuGet.Protocol.Catalog.Sample`.
