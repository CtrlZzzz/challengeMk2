using System;
using System.Collections.Generic;
using ChallengeMk2.Models;

namespace ChallengeMk2.DataBase
{
    public interface ILocalDataService
    {
        void Initialize();

        List<StarSystemDbItem> GetFullDb();

        StarSystemDbItem GetItem(string name);
        StarSystemDbItem GetItem(int id);

        void SaveItem(StarSystemDbItem starSystem);

        void ClearDb();
    }
}
