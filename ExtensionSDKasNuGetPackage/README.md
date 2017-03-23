	
# Extension SDKs as NuGet Package Sample
This provides an end-to-end sample to show how to package UWP XAML controls as a NuGet package. Refer to [Create UWP Controls as NuGet Packages](https://docs.microsoft.com/en-us/nuget/guides/create-uwp-controls) in the NuGet documentation for details.

To build the nuget package samples:

1. Batch build the solution for all `Release` configurations
2. From command prompt, navigate to `/ExtensionSDKasNuGetPackage/ManagedPackage/` (managed controls) or `ExtensionSDKasNuGetPackage/NativePackage` (native controls)
3. Execute `nuget pack ManagedPackage.nuspec` or `nuget pack NativePackage.nuspec` respectively

This will generate a `nupkg` and place it in the same directory as the `nuspec`.
