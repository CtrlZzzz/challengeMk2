using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.Models.ChatModels
{
    public class RoomListObject
    {
        public RoomListObject(string id, string name)
        {
            Id = id;
            RoomName = name;
        }

        public string Id { get; set; }

        public string RoomName { get; set; }
    }
}
