
# Extension SDKs as NuGet Package Sample
Provides an end-to-end sample to show how to package UWP XAML controls as a NuGet package. 

Specifically, this sample will cover how to:
* [Add Toolbox support](#add-toolbox-support)
* Support specific platform versions
* Add Design Time support


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
      <File Reference = "ManagedSdk.winmd" Implementation = "ManagedSdk.winmd">
        <ToolboxItems VSCategory="Graph">
        </ToolboxItems>
        <ToolboxItems BlendCategory="Controls/sample/Graph">
        </ToolboxItems>
      </File>
    </FileList>


