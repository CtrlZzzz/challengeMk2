using System;
using System.Collections.Generic;
using System.Text;
using ChallengeMk2.Models;

namespace ChallengeMk2.DataBase
{
    public interface IMapperService
    {
        StarSystem ConvertFromDb(StarSystemDbItem dbItem);

        StarSystemDbItem ConvertToDbItem(StarSystem starSystem);
    }
}
