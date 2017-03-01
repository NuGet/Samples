Instructions for converting a project.json UWP app to package reference UWP app.

1. Add the following properties to the property group at the top of the csproj file:

    <NuGetTargetFramework Condition="'$(NuGetTargetFramework)'==''">$(TargetPlatformIdentifier),Version=v$(TargetPlatformMinVersion)</NuGetTargetFramework>
    <RuntimeIdentifiers Condition="'$(RuntimeIdentifiers)'==''">win10-arm;win10-arm-aot;win10-x86;win10-x86-aot;win10-x64;win10-x64-aot</RuntimeIdentifiers>

2. Remove the following from the csproj file:

  <ItemGroup>
    <!-- A reference to the entire .Net Framework and Windows SDK are automatically included -->
    <None Include="project.json" />
  </ItemGroup>

3. Add the following reference to the csproj file, before the final targets import line starting with <Import Project="$(MSBuildExtensionsPath)...

  <ItemGroup>
    <PackageReference Include="Microsoft.NETCore.UniversalWindowsPlatform">
      <Version>5.2.2</Version>
    </PackageReference>
  </ItemGroup>

  If the project.json file contains any other package references (if it's a new project, it will only have this one), add them in a similar format to the csproj file.

4. Delete the project's project.json file, and (if it has one) its project.lock.json file.

5. Reload the project in VS and run a restore (right-click solution, Restore NuGet Packages).
