This is a sample illustrating the recommended practice for authoring package reference compatible packages that contain interop assemblies. 

In the past, NuGet packages used to be managed in two different ways – packages.config and project.json, each with their own set of advantages and limitations. With Visual Studio 2017 and .NET Core, we have improved the NuGet package management experience using PackageReference that brings new and improved capabilities such as deep MSBuild integration, improved performance for everyday tasks such as install and restore, multi-targeting and more. You can read the details in our blog post, https://blog.nuget.org/20170316/NuGet-now-fully-integrated-into-MSBuild.html.

In the packages.config world, when adding the references to the project file, NuGet and Visual Studio would figure out which assemblies are interop or not and reference/link accordingly by adding the EmbedInteropTypes metadata if needed. 

In the Package Reference world, due to performance considerations this is not done. We are recommending for package authors to explicitly specify which assemblies need to be embedded. 
We recommed for this to be done via MsBuild targets in the package. 
More information on that [here](https://docs.microsoft.com/en-us/nuget/create-packages/creating-a-package#including-msbuild-props-and-targets-in-a-package). 

The consumer of the package will still retain control over the package assemblies like in packages.config. The difference being that they need to add the metadata in their csproj specifically and that metadata will not being cleaned up by NuGet when a package is removed from the dependency list. 