@echo off > nul

REM Build ClassLibrary
%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild SatellitePackageSample\ClassLibrary\ClassLibrary.csproj

SETLOCAL
SET nuget=SatellitePackageSample\.nuget\NuGet.exe

REM Build the NuGet packages from ClassLibrary
%nuget% pack SatellitePackageSample\ClassLibrary\ClassLibrary.nuspec
%nuget% pack SatellitePackageSample\ClassLibrary\ClassLibrary.ja-jp.nuspec
%nuget% pack SatellitePackageSample\ClassLibrary\ClassLibrary.ru-ru.nuspec
%nuget% pack SatellitePackageSample\ClassLibrary\ClassLibrary.cs.nuspec
%nuget% pack SatellitePackageSample\ClassLibrary\ClassLibrary.Localization.nuspec

REM Make those packages available through a new feed source called SatellitePackageSample
%nuget% sources add -Name SatellitePackageSample -Source "%CD%"

REM Build the ConsoleApp
%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild SatellitePackageSample\ConsoleApp\ConsoleApp.csproj

ENDLOCAL