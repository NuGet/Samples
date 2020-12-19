# PackageReference Aliases Example

This project contains an example for how to use `Aliases` metadata with `PackageReference`.

In the [project file](PackageReferenceAliasesExample.csproj), specify

```xml
  <ItemGroup>
    <PackageReference Include="NuGet.Versioning" Version="5.8.0" Aliases="ExampleAlias" />
  </ItemGroup>
```

and in the code use it as following: 

```cs
extern alias ExampleAlias;

namespace PackageReferenceAliasesExample
{
...
        {
            ...
            var version = ExampleAlias.NuGet.Versioning.NuGetVersion.Parse("5.0.0");
            Console.WriteLine($"Version : {version}");
            ...
        }
...
}

```