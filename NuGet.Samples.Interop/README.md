This is a sample illustrating the recommended practice for authoring package reference compatible packages that contain interop assemblies. 

In the past, NuGet packages used to be managed in two different ways – packages.config and project.json, each with their own set of advantages and limitations. With Visual Studio 2017 and .NET Core, we have improved the NuGet package management experience using PackageReference that brings new and improved capabilities such as deep MSBuild integration, improved performance for everyday tasks such as install and restore, multi-targeting and more. You can read the details in our blog post, https://blog.nuget.org/20170316/NuGet-now-fully-integrated-into-MSBuild.html.

Packages that contain COM interop assemblies need to include an appropriate targets file so that the correct `EmbedInteropTypes` metadata is added to projects using the PackageReference format.

By default, the `EmbedInteropTypes` metadata is always false for all assemblies when PackageReference is used. Package authors must explicitly add this metadata by including a [targets file](#including-msbuild-props-and-targets-in-a-package). The target name should be unique to avoid conflicts; ideally, use a combination of your package name and the assembly being embedded. For an example, see [NuGet.Samples.Interop](https://github.com/NuGet/Samples/tree/master/NuGet.Samples.Interop).

```xml      
<Target Name="EmbeddingAssemblyNameFromPackageId" AfterTargets="ResolveReferences" BeforeTargets="FindReferenceAssembliesForReferences">
  <PropertyGroup>
    <_InteropAssemblyFileName>{InteropAssemblyName}</_InteropAssemblyFileName>
  </PropertyGroup>
  <ItemGroup>
    <ReferencePath Condition=" '%(FileName)' == '$(_InteropAssemblyFileName)' AND '%(ReferencePath.NuGetPackageId)' == '$(MSBuildThisFileName)' ">
      <EmbedInteropTypes>true</EmbedInteropTypes>
    </ReferencePath>
  </ItemGroup>
</Target>
```

Note that when using the `packages.config` reference format, adding references to the assemblies from the packages causes NuGet and Visual Studio to check for COM interop assemblies and set the `EmbedInteropTypes` to true in the project file. In this case the targets are overriden.

Additionally, by default the [build assets do not flow transitively](https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files#controlling-dependency-assets).  Packages authored as described here work differently when they are pulled as a transitive dependency from a project to project reference. The package consumer can allow them to flow by modifying the PrivateAssets default value to not include build.
