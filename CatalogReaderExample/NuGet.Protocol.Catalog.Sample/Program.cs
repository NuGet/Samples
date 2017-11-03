// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NuGet.Protocol.Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            // Set up the dependency injection container.
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(
                x => x.AddConsole().SetMinimumLevel(LogLevel.Warning));
            serviceCollection.AddTransient<CatalogProcessor>();
            serviceCollection.AddTransient<ICursor, FileCursor>(
                x => new FileCursor("cursor.json", x.GetRequiredService<ILogger<FileCursor>>()));
            serviceCollection.AddSingleton(
                x => new HttpClient());
            serviceCollection.AddTransient<ICatalogClient, CatalogClient>();
            serviceCollection.AddTransient(
                x => new CatalogProcessorSettings
                {
                    DefaultMinCommitTimestamp = DateTimeOffset.UtcNow.AddHours(-2),
                    ExcludeRedundantLeaves = false,
                });

            // Add our custom leaf processor.
            serviceCollection.AddTransient<ICatalogLeafProcessor, LoggerCatalogLeafProcessor>();

            // Initialize the catalog processor.
            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                var processor = serviceProvider.GetRequiredService<CatalogProcessor>();

                // Process the items.
                await processor.ProcessAsync();
            }
        }
    }
}
