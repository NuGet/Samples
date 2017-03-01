Instructions for converting a packages.config app to package reference app.

1. Add the following properties to the property group at the top of the csproj file:

    <RuntimeIdentifiers Condition="'$(RuntimeIdentifiers)'==''">win</RuntimeIdentifiers>

2. Remove the following from the csproj file (if it exists--it won't in a new project):

    <None Include="packages.config" />

3. Add the following reference to the csproj file, before the final targets import line starting with <Import Project="$(MSBuild...
   NOTE: we just need a package, *any* package declared here for the project to be loaded as a package reference project. We'll use Newtonsoft.Json for this exercise.

  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json">
      <Version>9.0.1</Version>
    </PackageReference>
  </ItemGroup>

  If there is a packages.config for the project and it contains any other package references, add them in a similar format to the csproj file.

4. Delete the project's packages.config file (if it has one).

5. Delete the solution's packages directory, off the solution root (if it has one).

6. Reload the project in VS and run a restore (right-click solution, Restore NuGet Packages).
