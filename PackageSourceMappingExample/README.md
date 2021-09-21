# Package Source Mapping sample

This folder is an example demonstrating the [Package Source Mapping feature](https://devblogs.microsoft.com/nuget/introducing-package-source-mapping/) (initially proposed as "Package Namespaces").

In addition to the `nuget.config` file in this folder, you can also check out how we set up our own [`nuGet.config` for the NuGet.Client repo](https://github.com/NuGet/NuGet.Client/blob/dev-kartheekp-ms-dogfood-namespaces/NuGet.Config) to use this feature internally.

## Package Source Mapping tooling

This feature is compatible with the following tools:

*   [Visual Studio 2022 preview 4][1] and later
*   [nuget.exe 6.0.0-preview.4][5] and later
*   [.NET SDK 6.0.100-rc.1][6] and later

Older tooling will ignore the Package Source Mapping configuration. To use this feature, ensure all your build environments use compatible tooling versions.

Package Source Mappings will apply to all project types - including .NET Framework - as long as compatible tooling is used for build and restore.

Here's an [example](https://github.com/NuGet/NuGet.Client/blob/dev-kartheekp-ms-dogfood-namespaces/NuGet.Config) of how the NuGet.Config for the NuGet.Client repo looks like in our dogfooding efforts.

## Package Source Mapping rules

1. Two types of package ID patterns are supported:

    a. `NuGet.*` - Package prefixes. Must end with a `*`, which may match 0 or more characters. `*` is the broadest valid prefix that matches all package IDs, but will have the lowest precedence by default. `NuGet*` is also valid and will match package IDs `NuGet`, `NuGetFoo`, and `NuGet.Bar`.
    
    b. `NuGet.Common` - Exact package IDs.

2. Any requested package ID must map to one or more sources by matching a defined package ID pattern. In other words, once you have defined a `packageSourceMapping` element you must explicitly define which sources *every* package - *including transitive packages* - will be restored from.

    a. Both top level (directly installed) *and transitive* packages must match defined patterns. There is no requirement that a top level package and its dependencies come from the same source.
    
    b. The same ID pattern can be defined on multiple sources, allowing matching package IDs to be restored from any of the feeds that define the pattern. However, this isn't recommended due to the impact on restore predictability (a given package could come from multiple sources).

3. When multiple unique patterns match a package ID, the most specific (longest) match will be preferred. 

    a. Exact package ID patterns always have the highest precedence while the generic `*` always has the lowest precedence. For an example package ID `NuGet.Common`, the following package ID patterns are ordered from highest to lowest precedence: `NuGet.Common`, `NuGet.*`, `*`. 

4. Package Source Mapping settings are applied following [nuget.config precedence rules](https://docs.microsoft.com/nuget/consume-packages/configuring-nuget-behavior#how-settings-are-applied) when multiple `nuget.config` files at various levels (machine-level, user-level, repo-level) are present. 

> Important: When the requested package already exists in the global packages folder, no source look-up will happen and the mappings will be ignored. Declare a [global packages folder for your repo](https://docs.microsoft.com/nuget/reference/nuget-config-file#config-section) to gain the full security benefits of this feature. Work to improve the experience with the default global packages folder in planned for a next iteration.

## Package Source Mapping onboarding

To fully onboard your repository take the following steps:

1.  Declare a new [global packages folder for your repo][9].
2.  Run [`dotnet list package --include-transitive`][12] to view all top-level and transitive packages in your solution. 
    *   For .NET framework projects using [`packages.config`][13], the `packages.config` file will have a flat list of all direct and transitive packages.
3.  Define mappings such that every package ID in your solution - *including transitive packages* - matches a package ID pattern for the target source.
4.  Run restore to validate that you have configured your mappings correctly. If your mappings don't fully cover every package ID in your solution, the error messages will help you identify the issue.
5.  When restore succeeds, you are done! Optionally consider: 
    *   simplifying the configuration to fewer declarations by using broader package ID prefixes or [setting a default source][14] where possible.
    *   verifying the source each package was restored from by checking the [metadata files in the global packages folder or reviewing the restore logs][15].

## Short example scenarios and results

**Scenario 1:**

The following are single project scenarios.

The sources are:

- `nuget.org` : `https://api.nuget.org/v3/index.json`
- `contoso` : `https://contoso.org/v3/index.json`

**Scenario 1A:**

NuGet.A 1.0.0 -> Microsoft.B 1.0.0

Microsoft.C 1.0.0 -> Microsoft.B 1.0.0

```xml
<PackageReference Include="NuGet.A" Version="1.0.0" />
<PackageReference Include="Microsoft.C" Version="1.0.0" />
```

```xml
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="NuGet.*" />
    </packageSource>
    <packagesource key="contoso">
        <package pattern="Microsoft.*" />
    </packageSource>
</packageSourceMapping>
```

**Result:**

- Package `NuGet.A` gets installed from `nuget.org`.
- Package `Microsoft.C` gets installed from `contoso`.
- Package `Microsoft.B` gets installed from `contoso`.

**Scenario 1B:**

NuGet.A 1.0.0 -> Microsoft.B 1.0.0

Microsoft.C 1.0.0 -> Microsoft.B 2.0.0

NuGet.Internal.D 1.0.0

```xml
<PackageReference Include="NuGet.A" Version="1.0.0" />
<PackageReference Include="Microsoft.C" Version="1.0.0" />
<PackageReference Include="NuGet.Internal.D" Version="1.0.0" />
```

```xml
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="NuGet.*" />
        <package pattern="Microsoft.B" />
    </packageSource>
    <packagesource key="contoso">
        <package pattern="Microsoft.*" />
        <package pattern="NuGet.Internal.*" />
    </packageSource>
</packageSourceMapping>
```

**Result:**

- Package `NuGet.A` gets installed from `nuget.org`.
- Package `Microsoft.C` gets installed from `contoso`.
- Package `Microsoft.B` gets installed from `nuget.org`. Even though the `contoso` namespace matches, the `nuget.org` one is an exact package id match.
- Package `NuGet.Internal.D` gets installed from `contoso`, because the prefix match is more specific.

**Scenario 1C:**

A 1.0.0 -> Microsoft.B 1.0.0

Microsoft.C 1.0.0 -> Microsoft.B 2.0.0

```xml
<PackageReference Include="A" Version="1.0.0" />
<PackageReference Include="Microsoft.C" Version="1.0.0" />
```

```xml
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="NuGet.*" />
    </packageSource>
    <packagesource key="contoso">
        <package pattern="Microsoft.*" />
    </packageSource>
</packageSourceMapping>

```

**Result:**

- Package `A` fails installation as none of the patterns match.

**Scenario 1D:**

NuGet.A 1.0.0 -> Microsoft.B 1.0.0

Microsoft.C 1.0.0

```xml
<PackageReference Include="NuGet.A" Version="1.0.0"/>
<PackageReference Include="Microsoft.C" Version="1.0.0"/>
```

```xml
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="NuGet.*" />
    </packageSource>
    <!-- no patterns for contoso source -->
</packageSourceMapping>
```

**Result:**

- Package `NuGet.A` will be installed from `nuget.org`.
- Package `Microsoft.B` will fail installing as there's no matching pattern.
- Package `Microsoft.C` will fail installing as there's no matching pattern.

**Scenario 1E:**

NuGet.A 1.0.0 -> Microsoft.B 1.0.0

Microsoft.C 1.0.0 -> Microsoft.B 2.0.0

```xml
<PackageReference Include="NuGet.A" Version="1.0.0" />
<PackageReference Include="Microsoft.C" Version="1.0.0" />
```

```xml
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="NuGet.*" />
        <package pattern="Microsoft.*" />
    </packageSource>
    <packagesource key="contoso">
        <package pattern="Microsoft.*" />
    </packageSource>
</packageSourceMapping>

```

**Result:**

- Package `NuGet.A` will installed from `nuget.org`.
- Package `Microsoft.C` is inconsistent, can get installed from either `nuget.org` or `contoso`.
- Package `Microsoft.B` is inconsistent, can get installed from either `nuget.org` or `contoso`.

**Scenario 1F:**

Microsoft.A 1.0.0

Microsoft.Community.B 1.0.0

```xml
<PackageReference Include="Microsoft.A" Version="1.0.0" />
<PackageReference Include="Microsoft.Community.B" Version="1.0.0" />
```

```xml
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="Microsoft.Community.*" />
    </packageSource>
    <packagesource key="contoso">
        <package pattern="Microsoft.Community.*" />
        <package pattern="Microsoft.*" />
    </packageSource>
</packageSourceMapping>

```

**Result:**

- Package `Microsoft.A` gets installed from `contoso`.
- Package `Microsoft.Community.B` is inconsistent, can get installed from either `nuget.org` or `contoso`.

**Scenario 1G:**

NuGetA 1.0.0 -> Microsoft.B 1.0.0

```xml
<PackageReference Include="NuGetA" Version="1.0.0"/>
```

```xml
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="NuGet*" />
    </packageSource>
    <packagesource key="contoso">
        <package pattern="*" />
    </packageSource>
</packageSourceMapping>

```

**Result:**

- Package `NuGetA` will be installed from `nuget.org`. ID prefixes do not need `.` delimiters.
- Package `Microsoft.B` will be installed from `contoso`. `*` is a valid ID prefix that matches all package IDs and be used to define a default/fallback source. However, `*` has the lowest precedence and will "lose" if a package ID matches a more specific pattern.

---

**Scenario 2:**

The following are multi project scenarios.

The sources are:

- `nuget.org` : `https://api.nuget.org/v3/index.json`
- `contoso` : `https://contoso.org/v3/index.json`

**Scenario 2A:**

Commandline PackageReference restore supports project level configuration. This equivalent is not support in Visual Studio, so this is not a recommended setup.

Given that, it's theoretically possible to get into an ambigious scenario.

NuGet.A 1.0.0 -> Microsoft.B 1.0.0

```xml
<!-- Project 1 Config -->
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="NuGet.*" />
    </packageSource>
    <packagesource key="contoso">
        <package pattern="Microsoft.*" />
    </packageSource>
</packageSourceMapping>
```

```xml
<!-- Project 1 -->
<PackageReference Include="NuGet.A" Version="1.0.0" />
```

```xml
<!-- Project 2 Config -->
<packageSourceMapping>
    <packagesource key="nuget.org">
        <package pattern="Microsoft.*" />
    </packageSource>
    <packagesource key="contoso">
        <package pattern="NuGet.*" />
    </packageSource>
</packageSourceMapping>
```

```xml
<!-- Project 2 -->
<PackageReference Include="Microsoft.B" Version="1.0.0" />
```

**Result:**

- Not deterministic, because it depends on the order in which the projects are restored, this could lead to either package getting restored from either source.
This is *not* a common, nor a recommended scenario.
