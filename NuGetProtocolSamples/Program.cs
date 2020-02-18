using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Packaging;
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
            Console.WriteLine("Search packages..");
            await SearchPackages();
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
    }
}
