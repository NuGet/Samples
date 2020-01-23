using Newtonsoft.Json;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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

            // 1. Find the latest search endpoint using the NuGet Service Index API
            // See: https://docs.microsoft.com/en-us/nuget/api/service-index
            var source = new PackageSource("https://api.nuget.org/v3/index.json");
            var providers = Repository.Provider.GetCoreV3();
            var repository = new SourceRepository(source, providers);

            var serviceIndex = await repository.GetResourceAsync<ServiceIndexResourceV3>();
            var searchEndpoints = serviceIndex.GetServiceEntryUris(ServiceTypes.SearchQueryService);

            if (!searchEndpoints.Any())
            {
                Console.WriteLine("Unable to find search endpoints");
                return;
            }

            // 2. Find your package's latest metadata using the NuGet Search V3 API 
            // See: https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource
            var query = "packageid:" + WebUtility.UrlEncode(packageId);
            var request = new Uri(searchEndpoints.First().ToString() + $"?q={query}&prerelease=true&semVerLevel=2.0.0");

            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"Unexpected response status code {response.StatusCode}: {response.ReasonPhrase}");
                return;
            }

            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<SearchResponse>(content);

            if (results.Data.Count == 0)
            {
                Console.WriteLine($"Could not find any results for package ID '{packageId}'");
                return;
            }

            foreach (var result in results.Data)
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
