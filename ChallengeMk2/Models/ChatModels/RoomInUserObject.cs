using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.Models.ChatModels
{
    public class RoomInUserObject
    {
        public string Id { get; set; }

        public string RoomId { get; set; }

        public string RoomName { get; set; }

        public DateTime JoinedDate { get; set; }
    }
}
