namespace JavaNet.Mvn.Model
{
    using System;
    using Newtonsoft.Json;

    public class RegistrationItem
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("items")]
        public RegistrationPage[] Items { get; set; }
    }

    public class RegistrationPage
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("items")]
        public RegistrationLeaf[] Items { get; set; }

        [JsonProperty("lower")]
        public string Lower { get; set; }

        [JsonProperty("upper")]
        public string Upper { get; set; }
    }

    public class RegistrationLeaf
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("catalogEntry")]
        public CatalogEntry CatalogEntry { get; set; }

        [JsonProperty("packageContent")]
        public Uri PackageContent { get; set; }

        [JsonProperty("registration")]
        public Uri Registration { get; set; }
    }

    public class CatalogEntry
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("authors")]
        public string Authors { get; set; }

        [JsonProperty("dependencyGroups")]
        public DependencyGroup[] DependencyGroups { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }

        [JsonProperty("id")]
        public string CatalogEntryId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("licenseUrl")]
        public Uri LicenseUrl { get; set; }

        [JsonProperty("listed")]
        public bool Listed { get; set; }

        [JsonProperty("minClientVersion")]
        public string MinClientVersion { get; set; }

        [JsonProperty("packageContent")]
        public Uri PackageContent { get; set; }

        [JsonProperty("projectUrl")]
        public Uri ProjectUrl { get; set; }

        [JsonProperty("published")]
        public DateTimeOffset Published { get; set; }

        [JsonProperty("requireLicenseAcceptance")]
        public bool RequireLicenseAcceptance { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class DependencyGroup
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("dependencies")]
        public Dependency[] Dependencies { get; set; }
    }

    public class Dependency
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("id")]
        public string DependencyId { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("registration")]
        public Uri Registration { get; set; }
    }
}
