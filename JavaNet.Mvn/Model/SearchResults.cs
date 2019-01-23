
using System;
using Newtonsoft.Json;

namespace JavaNet.Mvn.Model
{

    public class SearchResults
    {
        [JsonProperty("totalHits")]
        public long TotalHits { get; set; }

        [JsonProperty("data")]
        public SearchResult[] Data { get; set; }
    }

    public class SearchResult
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("registration")]
        public Uri Registration { get; set; }

        [JsonProperty("id")]
        public string DatumId { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("licenseUrl")]
        public Uri LicenseUrl { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("authors")]
        public string[] Authors { get; set; }

        [JsonProperty("totalDownloads")]
        public long TotalDownloads { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("versions")]
        public MvnVersion[] Versions { get; set; }

        [JsonProperty("projectUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ProjectUrl { get; set; }
    }

    public class MvnVersion
    {
        [JsonProperty("version")]
        public string VersionVersion { get; set; }

        [JsonProperty("downloads")]
        public long Downloads { get; set; }

        [JsonProperty("@id")]
        public Uri Id { get; set; }
    }
}
