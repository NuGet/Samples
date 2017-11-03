using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace CatalogReaderExample
{
    public class Program
    {
        private const string CursorFileName = "cursor.json";

        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            // Use nuget.org's V3 package source URL, a.k.a. the service index.
            var sourceUrl = "https://api.nuget.org/v3/index.json";

            // Define a lower time bound for leaves to fetch. This exclusive minimum time bound is called the cursor.
            var cursor = GetCursor();

            // Discover the catalog index URL from the service index.
            var catalogIndexUrl = await GetCatalogIndexUrlAsync(sourceUrl);

            var httpClient = new HttpClient();

            // Download the catalog index and deserialize it.
            var indexString = await httpClient.GetStringAsync(catalogIndexUrl);
            Console.WriteLine($"Fetched catalog index {catalogIndexUrl}.");
            var index = JsonConvert.DeserializeObject<CatalogIndex>(indexString);

            // Find all pages in the catalog index that meet the time bound.
            var pageItems = index
                .Items
                .Where(x => x.CommitTimeStamp > cursor);

            var allLeafItems = new List<CatalogLeaf>();

            foreach (var pageItem in pageItems)
            {
                // Download the catalog page and deserialize it.
                var pageString = await httpClient.GetStringAsync(pageItem.Url);
                Console.WriteLine($"Fetched catalog page {pageItem.Url}.");
                var page = JsonConvert.DeserializeObject<CatalogPage>(pageString);

                // Find all leaves in the catalog page that meet the time bound.
                var pageLeafItems = page
                    .Items
                    .Where(x => x.CommitTimeStamp > cursor);

                allLeafItems.AddRange(pageLeafItems);
            }

            allLeafItems = allLeafItems
                .OrderBy(x => x.CommitTimeStamp)
                .ToList();

            // Process all of the catalog leaf items.
            Console.WriteLine($"Processing {allLeafItems.Count} catalog leaves.");
            foreach (var leafItem in allLeafItems)
            {
                ProcessCatalogLeaf(leafItem);
            }

            // If we have processed any items, write the new cursor.
            if (allLeafItems.Any())
            {
                var newCursor = allLeafItems.Max(x => x.CommitTimeStamp);
                SetCursor(newCursor);
            }
        }

        private static async Task<Uri> GetCatalogIndexUrlAsync(string sourceUrl)
        {
            // This code uses the NuGet client SDK, which are the libraries used internally by the official
            // NuGet client.
            var sourceRepository = Repository.Factory.GetCoreV3(sourceUrl);
            var serviceIndex = await sourceRepository.GetResourceAsync<ServiceIndexResourceV3>();
            var catalogIndexUrl = serviceIndex.GetServiceEntryUri("Catalog/3.0.0");
            return catalogIndexUrl;
        }

        private static DateTime GetCursor()
        {
            try
            {
                var cursorString = File.ReadAllText(CursorFileName);
                var cursor = JsonConvert.DeserializeObject<Cursor>(cursorString);
                Console.WriteLine($"Read cursor value: {cursor.Value}.");
                return cursor.Value;
            }
            catch (FileNotFoundException)
            {
                var value = DateTime.UtcNow.AddHours(-1);
                Console.WriteLine($"No cursor found. Defaulting to {value}.");
                return value;
            }
        }

        private static void ProcessCatalogLeaf(CatalogLeaf leaf)
        {
            // Here, you can do whatever you want with each catalog item. If you want the full metadata about
            // the catalog leaf, you can use the leafItem.Url property to fetch the full leaf document. In this case,
            // we'll just keep it simple and output the details about the leaf that are included in the catalog page.
            Console.WriteLine($"{leaf.CommitTimeStamp}: {leaf.Id} {leaf.Version} (type is {leaf.Type})");
        }

        private static void SetCursor(DateTime value)
        {
            Console.WriteLine($"Writing cursor value: {value}.");
            var cursorString = JsonConvert.SerializeObject(new Cursor { Value = value });
            File.WriteAllText(CursorFileName, cursorString);
        }
    }
}
