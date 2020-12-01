using ChallengeMk2.Models;

namespace ChallengeMk2.DataBase
{
    class SystemDbMapperService : IMapperService
    {
        public StarSystem ConvertFromDb(StarSystemDbItem dbItem)
        {
            var converted = new StarSystem
            {
                Id = dbItem.Id,
                Distance = dbItem.Distance,
                BodyCount = dbItem.BodyCount,
                Name = dbItem.Name,
                RequirePermit = dbItem.RequirePermit,
                PermitName = dbItem.PermitName,

                Coords = new Coords
                {
                    X = dbItem.X,
                    Y = dbItem.Y,
                    Z = dbItem.Z
                },

                Information = new Information
                {
                    Allegiance = dbItem.Allegiance,
                    Government = dbItem.Government,
                    Faction = dbItem.Faction,
                    FactionState = dbItem.FactionState,
                    Population = dbItem.Population,
                    Security = dbItem.Security,
                    Economy = dbItem.Economy,
                    SecondEconomy = dbItem.SecondEconomy,
                    Reserve = dbItem.Reserve
                },

                PrimaryStar = new PrimaryStar
                {
                    Type = dbItem.Type,
                    Name = dbItem.StarName,
                    IsScoopable = dbItem.IsScoopable
                }
            };

            return converted;
        }

        public StarSystemDbItem ConvertToDbItem(StarSystem starSystem)
        {
            var converted = new StarSystemDbItem
            {
                Id = starSystem.Id,
                Distance = starSystem.Distance,
                BodyCount = starSystem.BodyCount ?? 0,
                Name = starSystem.Name,
                RequirePermit = starSystem.RequirePermit,
                PermitName = starSystem.PermitName,

                X = starSystem.Coords.X,
                Y = starSystem.Coords.Y,
                Z = starSystem.Coords.Z,

                Allegiance = starSystem.Information.Allegiance,
                Government = starSystem.Information.Government,
                Faction = starSystem.Information.Faction,
                FactionState = starSystem.Information.FactionState,
                Population = starSystem.Information.Population,
                Security = starSystem.Information.Security,
                Economy = starSystem.Information.Economy,
                SecondEconomy = starSystem.Information.SecondEconomy,
                Reserve = starSystem.Information.Reserve,

                Type = starSystem.PrimaryStar.Type,
                StarName = starSystem.PrimaryStar.Name,
                IsScoopable = starSystem.PrimaryStar.IsScoopable
            };

            return converted;
        }
    }
}
