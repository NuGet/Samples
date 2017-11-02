using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CatalogReaderExample
{
    public abstract class CatalogEntity
    {
        [JsonProperty("@id")]
        public string Url { get; set; }

        public DateTime CommitTimeStamp { get; set; }
    }

    public class CatalogIndex : CatalogEntity
    {
        public List<CatalogPage> Items { get; set; }
    }

    public class CatalogPage : CatalogEntity
    {
        public List<CatalogLeaf> Items { get; set; }
    }

    public class CatalogLeaf : CatalogEntity
    {
        [JsonProperty("nuget:id")]
        public string Id { get; set; }

        [JsonProperty("nuget:version")]
        public string Version { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
    }

    public class Cursor
    {
        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }
}
