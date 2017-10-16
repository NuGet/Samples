This is a sample illustrating the recommended practice for authoring package reference compatible packages that contain interop assemblies. 

In the past, NuGet packages used to be managed in two different ways – packages.config and project.json, each with their own set of advantages and limitations. With Visual Studio 2017 and .NET Core, we have improved the NuGet package management experience using PackageReference that brings new and improved capabilities such as deep MSBuild integration, improved performance for everyday tasks such as install and restore, multi-targeting and more. You can read the details in our blog post, https://blog.nuget.org/20170316/NuGet-now-fully-integrated-into-MSBuild.html.

In the packages.config case, no special authoring was needed for packages contain interop assemblies because when adding references to the project file NuGet and Visual Studio would test which assemblies are interop and set the EmbedInteropTypes to true.
In Package Reference case,  due to performance considerations this is not done. The EmbedInteropTypes metadata is always false for all assemblies.

We are recommending for package authors to explicitly specify which assemblies need to be embedded. 
We recommend for this to be done via MsBuild targets in the package. 
More information on that [here](https://docs.microsoft.com/en-us/nuget/create-packages/creating-a-package). 

**Note**
By default the build assets will not flow transitively. 
The [default value for private assets](https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files#controlling-dependency-assets) is:
```
<PrivateAssets>contentfiles;analyzers;build</PrivateAssets>
```

Packages authored this way, will work differently when they are a pulled as a transitive dependency from a project to project reference. 
