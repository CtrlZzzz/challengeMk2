namespace ChallengeMk2.Models
{
    public class StarSystem
    {
        public int Id { get; set; }

        public double Distance { get; set; }

        public int? BodyCount { get; set; }

        public string Name { get; set; }

        public bool RequirePermit { get; set; }

        public string PermitName { get; set; }

        public Coords Coords { get; set; }

        public Information Information { get; set; }

        public PrimaryStar PrimaryStar { get; set; }


        //Debug
        public override string ToString()
        {
            return Name;
        }
    }
}
