# Package Downloads Example

This sample finds the download count for each version of a package using the [`NuGet.Protocol`](https://www.nuget.org/packages/NuGet.Protocol) package and the [NuGet V3 Search API](https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource).

## Requirements

Install the latest version of the [.NET Core SDK](https://dotnet.microsoft.com/download).

## Example usage

You can run the app with:

```ps1
dotnet build
dotnet .\bin\Debug\netcoreapp3.1\PackageDownloadsExample.dll --package-id "Newtonsoft.Json"
```

You should see the following output:

```
Package Newtonsoft.Json has 363187770 total downloads.

Version 3.5.8 has 371478 downloads
Version 4.0.1 has 104026 downloads
Version 4.0.2 has 72464 downloads
Version 4.0.3 has 56128 downloads
Version 4.0.4 has 35853 downloads
...
```