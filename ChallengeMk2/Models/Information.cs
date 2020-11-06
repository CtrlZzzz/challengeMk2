using System;
using Newtonsoft.Json;

namespace ChallengeMk2.Models
{
    public class Information
    {
        public string Allegiance { get; set; }

        public string Government { get; set; }

        public string Faction { get; set; }

        public string FactionState { get; set; }

        public long Population { get; set; }

        public string Security { get; set; }

        public string Economy { get; set; }

        public string SecondEconomy { get; set; }

        public string Reserve { get; set; }
    }
}
