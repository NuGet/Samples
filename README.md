# Samples

This repo contains NuGet sample packages.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

1. `Satellite-Packages/ConsoleApp` - This console application will set the current UI culture to English, Japanese, and Russian, and output a string from `ClassLibrary`

2. `Satellite-Packages/ClassLibrary` - This "localized" class library project exposes string properties for consumption within `ConsoleApp`.

3. `Satellite-Packages/ClassLibrary/ClassLibrary.nuspec` - The spec for creating the `ClassLibrary` runtime package

4. `Satellite-Packages/ClassLibrary/ClassLibrary.ja-jp.nuspec` - The spec for creating the `ClassLibrary.ja-jp` satellite package

5. `Satellite-Packages/ClassLibrary/ClassLibrary.ru-ru.nuspec` - The spec for creating the `ClassLibrary.ru-ru` satellite package

6. `Satellite-Packages/ClassLibrary/ClassLibrary.Localization.nuspec` - The meta package that pulls in both `ClassLibrary.ja-jp` and `ClassLibrary.ru-ru`

    `Satellite-Packages/ConsoleApp` has a NuGet package reference to `Satellite-Packages/ClassLibrary`. By adding a package reference to `ClassLibrary.ja-jp`, `ClassLibrary.ru-ru`, or `ClassLibrary.Localization`, the output of the console application will change to include localized strings.

7.  `ContentFilesExample` is a sample package and project that demonstrates how to use the contentFiles feature in NuGet 3.3+

## Documentation and Further Learning

The [NuGet Docs](http://docs.nuget.org) cover creating NuGet packages in more detail:

* [Create NuGet packages](http://docs.nuget.org/create)
* [Creating and Publishing a Symbol Package](http://docs.nuget.org/Create/Creating-and-Publishing-a-Symbol-Package)
* [Creating Localized Packages](http://docs.nuget.org/Create/Creating-Localized-Packages)
* [Configuration File and Source Code Transformations](http://docs.nuget.org/Create/Configuration-File-and-Source-Code-Transformations)
* [ContentFiles in NuGet 3.3 and Visual Studio 2015 Update 1 and Later](http://docs.nuget.org/Create/NuSpec-Reference#contentfiles-with-visual-studio-2015-update-1-and-later)

## Feedback

If you're having trouble with the NuGet.org Website, file a bug on the [NuGet Gallery Issue Tracker](https://github.com/nuget/NuGetGallery/issues). 

If you're having trouble with the NuGet client tools (the Visual Studio extension, NuGet.exe command line tool, etc.), file a bug on [NuGet Home](https://github.com/nuget/home/issues).

Check out the [contributing](http://docs.nuget.org/contribute) page to see the best places to log issues and start discussions. The [NuGet Home](https://github.com/NuGet/Home) repo provides an overview of the different NuGet projects available.
