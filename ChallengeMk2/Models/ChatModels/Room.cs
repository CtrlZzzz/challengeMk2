using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.Models.ChatModels
{
    public class Room
    {
        public string Id { get; set; }

        public string RoomName { get; set; }

        public DateTime CreatedDate { get; set; }

        public IList<UserInRoomObject> RoomUsers { get; set; }

        public IList<Message> RoomMessages { get; set; }
    }
}
