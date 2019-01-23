using System;
using Newtonsoft.Json;

namespace JavaNet.Mvn.Model
{
    public class ServiceIndex
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        
        [JsonProperty("resources")]
        public ServiceResource[] Resources { get; set; }
    }

    public class ServiceResource
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }
        
        [JsonProperty("@type")]
        public object Type { get; set; }

        public ServiceResource(Uri id, object type)
        {
            Id = id;
            Type = type;
        }
    }
}