using System;
using Newtonsoft.Json;

namespace challengeMk2.Models
{
    public class Information
    {
        [JsonProperty("allegiance")]
        public string Allegiance { get; set; }

        [JsonProperty("government")]
        public string Government { get; set; }

        [JsonProperty("faction")]
        public string Faction { get; set; }

        [JsonProperty("factionState")]
        public string FactionState { get; set; }

        [JsonProperty("population")]
        public long Population { get; set; }

        [JsonProperty("security")]
        public string Security { get; set; }

        [JsonProperty("economy")]
        public string Economy { get; set; }

        [JsonProperty("secondEconomy")]
        public string SecondEconomy { get; set; }

        [JsonProperty("reserve")]
        public string Reserve { get; set; }
    }
}
