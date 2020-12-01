using SQLite;

namespace ChallengeMk2.Models
{
    public class StarSystemDbItem
    {
        [PrimaryKey, AutoIncrement]
        public int DbID { get; set; }

        public int Id { get; set; }

        public double Distance { get; set; }

        public int BodyCount { get; set; }

        public string Name { get; set; }

        public bool RequirePermit { get; set; }

        public string PermitName { get; set; }

        //Coords
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        //Information
        public string Allegiance { get; set; }

        public string Government { get; set; }

        public string Faction { get; set; }

        public string FactionState { get; set; }

        public long Population { get; set; }

        public string Security { get; set; }

        public string Economy { get; set; }

        public string SecondEconomy { get; set; }

        public string Reserve { get; set; }

        //PrimaryStar
        public string Type { get; set; }

        public string StarName { get; set; }

        public bool IsScoopable { get; set; }
    }
}
