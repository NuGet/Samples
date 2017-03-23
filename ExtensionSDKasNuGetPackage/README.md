	
# Extension SDKs as NuGet Package Sample
This provides an end-to-end sample to show how to package UWP XAML controls as a NuGet package. 

Specifically, this sample will cover how to:
* [Add toolbox/assets pane support ](https://docs.microsoft.com/en-us/nuget/guides/create-uwp-controls#add-toolboxassets-pane-support-for-xaml-controls)
* [Add custom icons to your controls](https://docs.microsoft.com/en-us/nuget/guides/create-uwp-controls#add-custom-icons-to-your-controls)
* [Support specific Windows platform versions](https://docs.microsoft.com/en-us/nuget/guides/create-uwp-controls#support-specific-windows-platform-versions)
* [Add design-time support](https://docs.microsoft.com/en-us/nuget/guides/create-uwp-controls#add-design-time-support)
* [Use strings and resources](https://docs.microsoft.com/en-us/nuget/guides/create-uwp-controls#use-strings-and-resources)
* [Package content such as images](https://docs.microsoft.com/en-us/nuget/guides/create-uwp-controls#package-content-such-as-images)

To build the nuget package samples:

1. Batch build the solution for all `Release` configurations
2. From command prompt, navigate to `/ExtensionSDKasNuGetPackage/ManagedPackage/` (managed controls) or `ExtensionSDKasNuGetPackage/NativePackage` (native controls)
3. Execute `nuget pack ManagedPackage.nuspec` or `nuget pack NativePackage.nuspec` respectively

This will generate a `nupkg` and place it in the same directory as the `nuspec`.
