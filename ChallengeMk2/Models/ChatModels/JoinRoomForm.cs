using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.Models.ChatModels
{
    public class JoinRoomForm
    {
        public JoinRoomForm(string roomName, string userId, string userName)
        {
            RoomName = roomName;
            UserId = userId;
            Username = userName;
        }

        public string RoomName { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }
    }
}
