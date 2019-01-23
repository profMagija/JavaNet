namespace JavaNet.Mvn.Model
{
    using System;
    using Newtonsoft.Json;

    public class MvnSearchResult
    {
        [JsonProperty("responseHeader")]
        public MvnResponseHeader ResponseHeader { get; set; }

        [JsonProperty("response")]
        public MvnResponse Response { get; set; }

        [JsonProperty("spellcheck")]
        public MvnSpellcheck Spellcheck { get; set; }
    }

    public class MvnResponse
    {
        [JsonProperty("numFound")]
        public long NumFound { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("docs")]
        public MvnDoc[] Docs { get; set; }
    }

    public class MvnDoc
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("g")]
        public string Group { get; set; }

        [JsonProperty("a")]
        public string Artifact { get; set; }

        [JsonProperty("latestVersion")]
        public string LatestVersion { get; set; }

        [JsonProperty("repositoryId")]
        public string RepositoryId { get; set; }

        [JsonProperty("p")]
        public string P { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("versionCount")]
        public long VersionCount { get; set; }

        [JsonProperty("text")]
        public string[] Text { get; set; }

        [JsonProperty("ec")]
        public string[] Ec { get; set; }
    }

    public class MvnResponseHeader
    {
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("QTime")]
        public long QTime { get; set; }

        [JsonProperty("params")]
        public MvnParams Params { get; set; }
    }

    public class MvnParams
    {
        [JsonProperty("q")]
        public string Q { get; set; }

        [JsonProperty("defType")]
        public string DefType { get; set; }

        [JsonProperty("spellcheck")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Spellcheck { get; set; }

        [JsonProperty("qf")]
        public string Qf { get; set; }

        [JsonProperty("indent")]
        public string Indent { get; set; }

        [JsonProperty("fl")]
        public string Fl { get; set; }

        [JsonProperty("start")]
        [JsonConverter(typeof(IntegerConverter))]
        public long Start { get; set; }

        [JsonProperty("spellcheck.count")]
        [JsonConverter(typeof(IntegerConverter))]
        public long SpellcheckCount { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }

        [JsonProperty("rows")]
        [JsonConverter(typeof(IntegerConverter))]
        public long Rows { get; set; }

        [JsonProperty("wt")]
        public string Wt { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class MvnSpellcheck
    {
        [JsonProperty("suggestions")]
        public object[] Suggestions { get; set; }
    }
    
    internal class IntegerConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly IntegerConverter Singleton = new IntegerConverter();
    }

    internal class BoolConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            bool b;
            if (Boolean.TryParse(value, out b))
            {
                return b;
            }
            throw new Exception("Cannot unmarshal type bool");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (bool)untypedValue;
            var boolString = value ? "true" : "false";
            serializer.Serialize(writer, boolString);
            return;
        }

        public static readonly BoolConverter Singleton = new BoolConverter();
    }
}
