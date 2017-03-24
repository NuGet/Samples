NB: The nuget.org ClassLibrary file is not being maintained, it can be used as a base. Please see step 8 for building the ClassLibrary nuget pkg from source code provided.

1. `Satellite-Packages/ConsoleApp` - This console application will set the current UI culture to English, Japanese, and Russian, and output a string from `ClassLibrary`

2. `Satellite-Packages/ClassLibrary` - This "localized" class library project exposes string properties for consumption within `ConsoleApp`.

3. `Satellite-Packages/ClassLibrary/ClassLibrary.nuspec` - The spec for creating the `ClassLibrary` runtime package

4. `Satellite-Packages/ClassLibrary/ClassLibrary.ja-jp.nuspec` - The spec for creating the `ClassLibrary.ja-jp` satellite package

5. `Satellite-Packages/ClassLibrary/ClassLibrary.ru-ru.nuspec` - The spec for creating the `ClassLibrary.ru-ru` satellite package

6. `Satellite-Packages/ClassLibrary/ClassLibrary.Localization.nuspec` - The meta package that pulls in both `ClassLibrary.ja-jp` and `ClassLibrary.ru-ru`

7. `Satellite-Packages/ConsoleApp` has a NuGet package reference to `Satellite-Packages/ClassLibrary`. By adding a package reference to `ClassLibrary.ja-jp`, `ClassLibrary.ru-ru`, or `ClassLibrary.Localization`, the output of the console application will change to include localized strings.

8. For local build: the following 'After Build' step to ClassLibrary project properties exists
   mkdir ..\\..\\..\packages\ClassLibrary.1.0.0.1\lib\net45\
   copy \*.* ..\\..\\..\packages\ClassLibrary.1.0.0.1\lib\net45\
   If you want to use Nuget or a Local Nuget repo: delete the 'After Build' steps and 'publish' the .nupkg files and ensure the local nuget is in a higher position than http://nuget.org/ otherwise the out-of-date ClassLibrary in Nuget will be downloaded and ConsoleApp project will not build (on [2017-03-02] it's broken).