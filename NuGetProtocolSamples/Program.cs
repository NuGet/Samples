using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace NuGet.Protocol.Samples
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Listing package versions...");
            await ListPackageVersionsAsync();

            Console.WriteLine();
            Console.WriteLine("Downloading package...");
            await DownloadPackageAsync();

            Console.WriteLine();
            Console.WriteLine("Get package metadata...");
            await GetPackageMetadataAsync();

            Console.WriteLine();
            Console.WriteLine("Searching packages...");
            await SearchPackages();

            Console.WriteLine();
            Console.WriteLine("Creating a package...");
            CreatePackage();

            Console.WriteLine();
            Console.WriteLine("Reading a package...");
            ReadPackage();

            Console.WriteLine();
            Console.WriteLine("Pushing a package...");
            await PushPackageAsync();

            Console.WriteLine();
            Console.WriteLine("Deleting a package...");
            await DeletePackageAsync();
        }

        public static async Task ListPackageVersionsAsync()
        {
            // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
            // or move it to a different file.
            #region ListPackageVersions
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            IEnumerable<NuGetVersion> versions = await resource.GetAllVersionsAsync(
                "Newtonsoft.Json",
                cache,
                logger,
                cancellationToken);

            foreach (NuGetVersion version in versions)
            {
                Console.WriteLine($"Found version {version}");
            }
            #endregion
        }

        public static async Task DownloadPackageAsync()
        {
            // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
            // or move it to a different file.
            #region DownloadPackage
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            string packageId = "Newtonsoft.Json";
            NuGetVersion packageVersion = new NuGetVersion("12.0.1");
            using MemoryStream packageStream = new MemoryStream();

            await resource.CopyNupkgToStreamAsync(
                packageId,
                packageVersion,
                packageStream,
                cache,
                logger,
                cancellationToken);

            Console.WriteLine($"Downloaded package {packageId} {packageVersion}");

            using PackageArchiveReader packageReader = new PackageArchiveReader(packageStream);
            NuspecReader nuspecReader = await packageReader.GetNuspecReaderAsync(cancellationToken);

            Console.WriteLine($"Tags: {nuspecReader.GetTags()}");
            Console.WriteLine($"Description: {nuspecReader.GetDescription()}");
            #endregion
        }

        public static async Task GetPackageMetadataAsync()
        {
            // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
            // or move it to a different file.
            #region GetPackageMetadata
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            PackageMetadataResource resource = await repository.GetResourceAsync<PackageMetadataResource>();

            IEnumerable<IPackageSearchMetadata> packages = await resource.GetMetadataAsync(
                "Newtonsoft.Json",
                includePrerelease: true,
                includeUnlisted: false,
                cache,
                logger,
                cancellationToken);

            foreach (IPackageSearchMetadata package in packages)
            {
                Console.WriteLine($"Version: {package.Identity.Version}");
                Console.WriteLine($"Listed: {package.IsListed}");
                Console.WriteLine($"Tags: {package.Tags}");
                Console.WriteLine($"Description: {package.Description}");
            }
            #endregion
        }

        public static async Task SearchPackages()
        {
            // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
            // or move it to a different file.
            #region SearchPackages
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            PackageSearchResource resource = await repository.GetResourceAsync<PackageSearchResource>();
            SearchFilter searchFilter = new SearchFilter(includePrerelease: true);

            IEnumerable<IPackageSearchMetadata> results = await resource.SearchAsync(
                "json",
                searchFilter,
                skip: 0,
                take: 20,
                logger,
                cancellationToken);

            foreach (IPackageSearchMetadata result in results)
            {
                Console.WriteLine($"Found package {result.Identity.Id} {result.Identity.Version}");
            }
            #endregion
        }

        public static void CreatePackage()
        {
            // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
            // or move it to a different file.
            //
            // It is strongly recommended that NuGet packages are created using the official NuGet tooling and not
            // using this low level API. There are a variety of characteristics important for a well-formed package
            // and the latest version of tooling helps incorporate these best practices.
            //
            // For more information about creating NuGet packages, see the overview of the package creation workflow
            // and the documentation for official pack tooling (e.g. using the dotnet CLI):
            // https://docs.microsoft.com/en-us/nuget/create-packages/overview-and-workflow
            // https://docs.microsoft.com/en-us/nuget/create-packages/creating-a-package-dotnet-cli
            #region CreatePackage
            PackageBuilder builder = new PackageBuilder();
            builder.Id = "MyPackage";
            builder.Version = new NuGetVersion("1.0.0-beta");
            builder.Description = "My package created from the API.";
            builder.Authors.Add("Sample author");
            builder.DependencyGroups.Add(new PackageDependencyGroup(
                targetFramework: NuGetFramework.Parse("netstandard1.4"),
                packages: new[]
                {
                    new PackageDependency("Newtonsoft.Json", VersionRange.Parse("10.0.1"))
                }));

            using FileStream outputStream = new FileStream("MyPackage.nupkg", FileMode.Create);
            builder.Save(outputStream);
            Console.WriteLine($"Saved a package to {outputStream.Name}");
            #endregion
        }

        private static void ReadPackage()
        {
            // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
            // or move it to a different file.
            #region ReadPackage
            using FileStream inputStream = new FileStream("MyPackage.nupkg", FileMode.Open);
            using PackageArchiveReader reader = new PackageArchiveReader(inputStream);
            NuspecReader nuspec = reader.NuspecReader;
            Console.WriteLine($"ID: {nuspec.GetId()}");
            Console.WriteLine($"Version: {nuspec.GetVersion()}");
            Console.WriteLine($"Description: {nuspec.GetDescription()}");
            Console.WriteLine($"Authors: {nuspec.GetAuthors()}");

            Console.WriteLine("Dependencies:");
            foreach (var dependencyGroup in nuspec.GetDependencyGroups())
            {
                Console.WriteLine($" - {dependencyGroup.TargetFramework.GetShortFolderName()}");
                foreach (var dependency in dependencyGroup.Packages)
                {
                    Console.WriteLine($"   > {dependency.Id} {dependency.VersionRange}");
                }
            }

            Console.WriteLine("Files:");
            foreach (var file in reader.GetFiles())
            {
                Console.WriteLine($" - {file}");
            }
            #endregion
        }

        public static async Task UseNuGetProtocolWithAnAuthenticatedFeed()
        {
            // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
            // or move it to a different file.
            #region AuthenticatedFeed
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;
            SourceCacheContext cache = new SourceCacheContext();
            var sourceUri = "https://contoso.privatefeed/v3/index.json";
            var packageSource = new PackageSource(sourceUri)
            {
                Credentials = new PackageSourceCredential(
                    source: sourceUri,
                    username: "myUsername",
                    passwordText: "myVerySecretPassword",
                    isPasswordClearText: true,
                    validAuthenticationTypesText: null)
            };
            // If the `SourceRepository` is created with a `PackageSource`, the rest of APIs will consume the credentials attached to `PackageSource.Credentials`.
            SourceRepository repository = Repository.Factory.GetCoreV3(packageSource);
            PackageMetadataResource resource = await repository.GetResourceAsync<PackageMetadataResource>();

            IEnumerable<IPackageSearchMetadata> packages = await resource.GetMetadataAsync(
                "MyPackage",
                includePrerelease: true,
                includeUnlisted: false,
                cache,
                logger,
                cancellationToken);

            foreach (IPackageSearchMetadata package in packages)
            {
                Console.WriteLine($"Version: {package.Identity.Version}");
                Console.WriteLine($"Listed: {package.IsListed}");
                Console.WriteLine($"Tags: {package.Tags}");
                Console.WriteLine($"Description: {package.Description}");
            }
            #endregion
        }

        private static async Task PushPackageAsync()
        {
            try
            {
                // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
                // or move it to a different file.
                #region PushPackage
                ILogger logger = NullLogger.Instance;
                CancellationToken cancellationToken = CancellationToken.None;

                SourceCacheContext cache = new SourceCacheContext();
                SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
                PackageUpdateResource resource = await repository.GetResourceAsync<PackageUpdateResource>();

                string apiKey = "my-api-key";

                await resource.Push(
                    "MyPackage.nupkg",
                    symbolSource: null,
                    timeoutInSecond: 5 * 60,
                    disableBuffering: false,
                    getApiKey: packageSource => apiKey,
                    getSymbolApiKey: packageSource => null,
                    noServiceEndpoint: false,
                    skipDuplicate: false,
                    symbolPackageUpdateResource: null,
                    logger);
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine($"Pushing package failed: {e}");
            }
        }

        private static async Task DeletePackageAsync()
        {
            try
            {
                // This code region is referenced by the NuGet docs. Please update the docs if you rename the region
                // or move it to a different file.
                #region DeletePackage
                ILogger logger = NullLogger.Instance;
                CancellationToken cancellationToken = CancellationToken.None;

                SourceCacheContext cache = new SourceCacheContext();
                SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
                PackageUpdateResource resource = await repository.GetResourceAsync<PackageUpdateResource>();

                string apiKey = "my-api-key";

                await resource.Delete(
                    "MyPackage",
                    "1.0.0-beta",
                    getApiKey: packageSource => apiKey,
                    confirm: packageSource => true,
                    noServiceEndpoint: false,
                    logger);
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine($"Deleting package failed: {e}");
            }
        }
    }
}
