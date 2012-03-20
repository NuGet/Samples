This repository contains the following:

1. ConsoleApp - This console application will set the current UI culture to English, Japanese, and Russian, and output a string from ClassLibrary

2. ClassLibrary - This "localized" class library project exposes string properties for consumption within ConsoleApp.

3. ClassLibrary.nuspec - The spec for creating the ClassLibrary runtime package

4. ClassLibrary.ja-jp.nuspec - The spec for creating the ClassLibrary.ja-jp satellite package

5. ClassLibrary.ru-ru.nuspec - The spec for creating the ClassLibrary.ru-ru satellite package

6. ClassLibrary.Localization.nuspec - The meta package that pulls in both ClassLibrary.ja-jp and ClassLibrary.ru-ru


ConsoleApp has a NuGet package reference to ClassLibrary.  By adding a package reference to ClassLibrary.ja-jp, ClassLibrary.ru-ru, or ClassLibrary.Localization,
the output of the console application will change to include localized strings.