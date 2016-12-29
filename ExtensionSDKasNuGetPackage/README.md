	
# Extension SDKs as NuGet Package Sample
This provides an end-to-end sample to show how to package UWP XAML controls as a NuGet package. 

Specifically, this sample will cover how to:
* [Add Toolbox support](#add-toolbox-support)
* [Support specific Windows platform versions](#support-specific-platform-versions)
* [Add design time support](#add-design-time-support)
* Use strings/resources
* Pacakge content such as images

To build the nuget package samples:

1. Batch build the solution for all `Release` configurations
2. From command prompt, navigate to `/ExtensionSDKasNuGetPackage/ManagedPackage/` (managed controls) or `ExtensionSDKasNuGetPackage/NativePackage` (native controls)
3. Execute `nuget pack ManagedPackage.nuspec` or `nuget pack NativePackage.nuspec` respectively

This will generate a `nupkg` and place it in the same directory as the `nuspec`.

## Add Toolbox support

To enable controls to show up in the Toolbox, create a `VisualStudioToolsManifest.xml` file and place it in the root of the `tools` folder when packing your nupkg.

    \build
    \lib
    \tools
		\VisualStudioToolsManifest.xml

VisualStudioToolsManifest is only required to specify metadata used by the XAML designer tools in Visual Studio and only if you are shipping XAML controls. 
Here is a sample of VisualStudioToolsManifest.xml:

    <FileList>
      <File Reference = "MyControl.winmd">
        <ToolboxItems VSCategory="Graph">
        </ToolboxItems>
        <ToolboxItems BlendCategory="Controls/sample/Graph">
        </ToolboxItems>
      </File>
    </FileList>

## Support specific Windows platform versions

UWP packages have a TargetPlatformVersion (TPV) and TargetPlatformMinVersion (TPMinV) that defines the upper and lower bounds of the OS version where the app can be installed. TPV further specifies the version of the SDK that the app is built against.

When authoring a UWP package, be cognizant of these properties while designing and coding your libraries. Using API's outside of the bounds of the platform versions defined in the app will either cause the build to fail or the app to fail at runtime.

For this example, you have tested your controls and want your control library to be consumed by projects where
* TargetPlatformVersion(TPV) >= Windows 10 Anniversary Edition (10.0; Build 14393) and 
* TargetPlatformMinVersion(TPMinV) >= Windows 10 (10.0; Build 10586)

Below, we talk about how to ensure that your control library is consumed only by UWP projects that fall within the bounds of our example.

### TargetPlatformVersion(TPV) check
For this example, to enforce TPV check, you must package the controls as follows:

    \lib\uap10.0.14393.0\*
    \ref\uap10.0.14393.0\*

This nuget package will be applicable to all projects where TPV >= 10.0.14393.0.
Note: `ref` is given here for completeness and is only required if you have a reference assembly that is used to compile the app and there is a different implementation assembly in lib that is copied into the application output.

If you need to specify multiple versions of the assembly targeting specific versions of the SDK, you can do that by creating multiple libraries targeting specific versions of the OS. For example:

    \lib\uap10.0.14393.0\*
    \lib\uap10.0.10586.0\*
    \ref\uap10.0.14393.0\*
    \ref\uap10.0.10586.0\*

In this case, the correct version of the assembly will be selected based on the consuming project's TPV. If the consuming project's TPV is higher than all available assemblies, the highest version of the assembly will be selected.

Note: `\lib\uap10.0` will continue to work. However, there will be no TPV check and the package can be consumed by a UWP project irrespective of its TPV.

###TargetPlatformMinVersion (TPMinV) check
To enforce TPMinV check, you can write a build task that checks the TargetPlatformMinVersion at build time and causes the build to fail if the TPMinV is lower than the one supported by your library. 
Package the build task and author a targets file. For this example, the targets file will look like:

	<?xml version="1.0" encoding="utf-8"?>
	<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
	   <UsingTask TaskName="VersionCheckTask" 
	    AssemblyFile = "UWPVersionCheck.dll"/>
	  <Target Name="BeforeBuild">
		<VersionCheckTask MinVersionSupported="10.0.10586.0" TargetMinVersion="$(TargetPlatformMinVersion)"/>
	  </Target>
	</Project>

Assuming, the build task library is called UWPVersionCheck.dll and the targets file is called MyControl.targets, package them as below:

	\build
		\uap
			\MyControl.targets
			\UWPVersionCheck.dll
	\lib
	\tools

## Add Design Time support
A design.dll allows you to make control properties editable from the property inspector, add custom adorners, etc. To add this support, place the design.dll inside the Design folder.

	\build
	\lib
		\uap
			\Design
				\MyControl.design.dll
			\Themes		
	\tools


## Use strings/resources
You can embed string resources (.resw) in your package that are either consumed by your control or by the consuming UWP project. You can do that by setting the `Build Action` property of the `.resw` file to `PRIResource`.

## Package content such as images
You can package content such as images that are either consumed by your control or by the consuming UWP project. You can do that by adding the content to the UAP folder as follows:


	\build
	\lib
		\uap
			\Design
			\Themes	
			\contosoSampleImage.jpg
	\tools
	

