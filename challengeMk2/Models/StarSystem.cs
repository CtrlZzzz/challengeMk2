using Newtonsoft.Json;
using SQLite;

namespace ChallengeMk2.Models
{
    public class StarSystem
    {
        public double Distance { get; set; }

        public int BodyCount { get; set; }

        public string Name { get; set; }

        public bool RequirePermit { get; set; }

        public string PermitName { get; set; }

        public Coords Coords { get; set; }

        public Information Information { get; set; }

        public PrimaryStar PrimaryStar { get; set; }


        public int DataSelectorID { get; set; }     //For carousel dataTemplate selection


        //Debug
        public override string ToString()
        {
            return Name;
        }
    }
}
