using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace PackageDownloadsExample
{
    /// <summary>
    /// Find the download count for each version of a package.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Find the download count for each version of a package.
        /// </summary>
        /// <param name="packageId">The package ID. Example: "newtonsoft.json"</param>
        public static async Task Main(string packageId)
        {
            if (string.IsNullOrEmpty(packageId))
            {
                Console.WriteLine("The --package-id option is required. Run --help for more information");
                return;
            }

            var source = new PackageSource("https://api.nuget.org/v3/index.json");
            var providers = Repository.Provider.GetCoreV3();
            var repository = new SourceRepository(source, providers);

            var search = await repository.GetResourceAsync<RawSearchResourceV3>();
            var filter = new SearchFilter(includePrerelease: true);

            var response = await search.Search($"packageid:{packageId}", filter, skip: 0, take: 20, NullLogger.Instance, CancellationToken.None);
            var results = response.Select(result => result.ToObject<SearchResult>()).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine($"Could not find any results for package ID '{packageId}'");
                return;
            }

            foreach (var result in results)
            {
                Console.WriteLine($"Package {result.PackageId} has {result.TotalDownloads} total downloads.");
                Console.WriteLine();

                foreach (var version in result.Versions)
                {
                    Console.WriteLine($"Version {version.Version} has {version.Downloads} downloads");
                }
            }
        }
    }
}
