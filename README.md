# Samples

This repo contains NuGet sample packages.

1. `ConsoleApp` - This console application will set the current UI culture to English, Japanese, and Russian, and output a string from `ClassLibrary`

2. `ClassLibrary` - This "localized" class library project exposes string properties for consumption within `ConsoleApp`.

3. `ClassLibrary.nuspec` - The spec for creating the `ClassLibrary` runtime package

4. `ClassLibrary.ja-jp.nuspec` - The spec for creating the `ClassLibrary.ja-jp` satellite package

5. `ClassLibrary.ru-ru.nuspec` - The spec for creating the `ClassLibrary.ru-ru` satellite package

6. `ClassLibrary.Localization.nuspec` - The meta package that pulls in both `ClassLibrary.ja-jp` and `ClassLibrary.ru-ru`

`ConsoleApp` has a NuGet package reference to `ClassLibrary`. By adding a package reference to `ClassLibrary.ja-jp`, `ClassLibrary.ru-ru`, or `ClassLibrary.Localization`, the output of the console application will change to include localized strings.

## Documentation and Further Learning

The [NuGet Docs](http://docs.nuget.org) cover creating NuGet packages in more detail:

* [Create NuGet packages](http://docs.nuget.org/create)
* [Creating and Publishing a Symbol Package](http://docs.nuget.org/Create/Creating-and-Publishing-a-Symbol-Package)
* [Creating Localized Packages](http://docs.nuget.org/Create/Creating-Localized-Packages)
* [Configuration File and Source Code Transformations](http://docs.nuget.org/Create/Configuration-File-and-Source-Code-Transformations)

## Feedback

Check out the [contributing](http://docs.nuget.org/contribute) page to see the best places to log issues and start discussions. The [NuGet Home](https://github.com/NuGet/Home) repo provides an overview of the different NuGet projects available.
