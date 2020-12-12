using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.Models.ChatModels
{
    public class UserInRoomObject
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public DateTime JoinedDate { get; set; }
    }
}
