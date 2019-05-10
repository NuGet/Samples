This is a sample that illustrates the recommended way to add an extern alias to assemblies brought in through a package in PackageReference. 

This sample requires a 2.1 version of the .NET Core SDK and the respective Visual Studio installation. (VS 2017 latest or VS 2017)
This sample has a `build.ps1` script that allows you to test the scenario. This sample is self-contained, meaning it will not pollute your global packages folder. 

We create 2 different packages (`ClassLib1`, `ClassLib2`) that have a conflicting namespace, `Library.Lib`. 
Then we reference both packages in the result project leading to a conflict raised by the compiler. 
The below target is an example how to add an extern alias for one of the assemblies. 
There a few things that are important and need to be customized when using. 

1. Target name. Notice that it is `AddCustomAliases`. The only requirement is that the name is unique. Make sure that you are not overwriting a target already existing somewhere within your project context.
1. PackageId, ideally you want to specify the PackageId to avoid additional conflicts.
1. FileName, this is the assembly name in question. 
1. Aliases, what you want the extern alias to be. 

```xml
<Target Name="AddCustomAliases" BeforeTargets="FindReferenceAssembliesForReferences;ResolveReferences">
    <ItemGroup>
        <ReferencePath Condition="'%(FileName)' == 'ClassLib2' AND '%(ReferencePath.NuGetPackageId)' == 'ClassLib2'">
        <Aliases>ClassLib2</Aliases>
        </ReferencePath>
    </ItemGroup>
</Target>
```