using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PokedexXamarin.Models
{
    public partial class Pokemon2
    {
        [JsonProperty("base_experience")]
        public int BaseExperience { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("moves")]
        public MoveElement[] Moves { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }

        [JsonProperty("types")]
        public TypeElement[] Types { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }
    }

    public partial class MoveElement
    {
        [JsonProperty("move")]
        public MoveMove Move { get; set; }
    }

    public partial class MoveMove
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Sprites
    {
        [JsonProperty("front_default")]
        public Uri FrontDefault { get; set; }

        [JsonProperty("other")]
        public Other Other { get; set; }
    }

    public partial class Other
    {
        [JsonProperty("dream_world")]
        public DreamWorld DreamWorld { get; set; }
    }

    public partial class DreamWorld
    {
        [JsonProperty("front_default")]
        public Uri FrontDefault { get; set; }

        [JsonProperty("front_female")]
        public object FrontFemale { get; set; }
    }

    public partial class TypeElement
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public TypeType Type { get; set; }
    }

    public partial class TypeType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

