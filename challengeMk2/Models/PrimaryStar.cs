using System;
using Newtonsoft.Json;

namespace challengeMk2.Models
{
    public class PrimaryStar
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isScoopable")]
        public bool IsScoopable { get; set; }
    }
}
