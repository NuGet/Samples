
# Extension SDKs as NuGet Package Sample
Provides an end-to-end sample to show how to package UWP XAML controls as a NuGet package. 

Specifically, this sample will cover how to:
* [Add Toolbox support](#add-toolbox-support)
* [Support specific platform versions](#support-specific-platform-versions)
* [Add Design Time support](#add-design-time-support)


## Add Toolbox support

To enable controls to show up in the toolbox, create a VisualStudioToolsManifest.xml and place it in the root of the tools folder when packing your nupkg.

    \build
    \lib
    \tools
	\VisualStudioToolsManifest.xml

VisualStudioToolsManifest is only required to specify metadata used by the XAML designer tools in Visual Studio and only if you are shipping controls. 
Here is a sample of VisualStudioToolsManifest.xml. 

    <FileList>
      MinVSVersion = "14.0">
      <File Reference = "MyControl.dll" Implementation = "MyControl.dll">
        <ToolboxItems VSCategory="Graph">
        </ToolboxItems>
        <ToolboxItems BlendCategory="Controls/sample/Graph">
        </ToolboxItems>
      </File>
    </FileList>

## Support specific platform versions

UAP packages have a TargetPlatformVersion(TPV) and TargetPlatformMinVersion(TPM) that defines the upper and lower bounds of the OS version where the app can be installed into. TPV further specifies the version of the SDK that the app is compiled against.

When authoring a UAP package, be cognizant of these properties while designing and coding your libraries. Using API's outside of the bounds of the platform versions defined in the app will either cause the build to fail or the app to fail at runtime.
Some examples of possible combinations of TPV and TPM are given below.

Target Platform Version	| Target Platform Minimum Version
------------------------|--------------------------------
14393|14393
14393|10586
14393|10240
10586|10586
10586|10240
10240|10240


###TargetPlatformVersion(TPV)
For example, if you want to target version 10586 of the SDK and above, you can name the folder as following:

    \lib\uap10.0.10586.0\*
    \ref\uap10.0.10586.0\*

This nuget package will be applicable to all projects where TPV >= 10.0.10586.0.
Note: `ref` is given here for completeness and is only required if you have a reference assembly that is used to compile the app and there is a different implementation assembly in lib that is copied into the application output.
If you need to specify multiple versions of the assembly targeting specific versions of the SDK, you can do that by creating multiple libraries targeting specific versions of the OS. For example:

    \lib\uap10.0.14393.0\*
    \lib\uap10.0.10586.0\*
    \ref\uap10.0.14393.0\*
    \ref\uap10.0.10586.0\*


###TargetPlatformMinVersion(TPM)
To enforce TPM check, you can write a build task that checks the TargetPlatformMinVersion at build time and causes the build to fail if the TPM is lower than one supported by your library. 
Package the build task and author a targets file. For example, the targets file will look like:

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

# Add Design Time support
A design.dll allows you to make control properties editable from the property inspector, add custom adorners, etc. To add this support, place the design.dll inside the Design folder.

	\build
		\uap
			\Design
				\MyControl.design.dll
				
	\lib
	\tools
