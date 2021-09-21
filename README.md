# Samples

This repo contains NuGet sample packages and sample code.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

1. [ExtensionSDKasNuGetPackage](ExtensionSDKasNuGetPackage) is a sample solution that walks you through several scenarios for shipping an extension SDK as nuget package

1. [VsCredentialProvider](VsCredentialProvider) is a sample for creating a NuGet credential provider for Visual Studio

1. [Satellite-Packages](Satellite-Packages) is a sample class library and console app that demonstrates how to localize your NuGet packege using the Sattelite Package feature.

1. [PackageReferenceProjects](PackageReferenceProjects) demonstrates how you can manually migrate to the new PackageReference format.

1. [ContentFilesExample](ContentFilesExample) is a sample package and project that demonstrates how to use the contentFiles feature in NuGet 3.3+

1. [Preinstalled-Packages](Preinstalled-Packages) demonstrates how template authors can instruct NuGet to install the necessary packages, rather than individual libraries. Read more about [Packages in Visual Studio templates](https://docs.microsoft.com/en-us/nuget/visual-studio-extensibility/visual-studio-templates)

1. [CatalogReaderExample](CatalogReaderExample) is a sample showing how to read the NuGet API's catalog resource.

1. [NuGetProtocolSamples](NuGetProtocolSamples) shows how to use the [`NuGet.Protocol`](https://www.nuget.org/packages/NuGet.Protocol) package.

1. [PackageDownloadsExample](PackageDownloadsExample) shows how to get a package's download counts.

1. [PackageSourceMappingExample](PackageSourceMappingExample) is a sample solution that shows how to configure Package Source Mapping for a couple projects and packages. [The README](PackageSourceMappingExample/README.md) contains many smaller example scenarios and results.

## Documentation and Further Learning

The [NuGet Docs](http://docs.nuget.org) cover creating NuGet packages in more detail:

* [Create NuGet packages](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package)
* [Creating and Publishing a Symbol Package](http://docs.nuget.org/Create/Creating-and-Publishing-a-Symbol-Package)
* [Creating Localized Packages](http://docs.nuget.org/Create/Creating-Localized-Packages)
* [Configuration File and Source Code Transformations](http://docs.nuget.org/Create/Configuration-File-and-Source-Code-Transformations)
* [ContentFiles in NuGet 3.3 and Visual Studio 2015 Update 1 and Later](https://docs.microsoft.com/en-us/nuget/schema/nuspec#including-content-files)
* [Preinstalled-Packages](Preinstalled-Packages) demonstrates how template authors can instruct NuGet to install the necessary packages, rather than individual libraries. Read more about [Packages in Visual Studio templates](https://docs.microsoft.com/en-us/nuget/visual-studio-extensibility/visual-studio-templates)

## Feedback

If you're having trouble with the NuGet.org website or HTTP API, file a bug on the [NuGet Gallery Issue Tracker](https://github.com/nuget/NuGetGallery/issues). 

If you're having trouble with the NuGet client tools (the Visual Studio extension, NuGet.exe command line tool, etc.), file a bug on [NuGet Home](https://github.com/nuget/home/issues).

Check out the [contributing](https://github.com/NuGet/Home/wiki/Contribute-to-NuGet) page to see the best places to log issues and start discussions. The [NuGet Home](https://github.com/NuGet/Home) repo provides an overview of the different NuGet projects available.
