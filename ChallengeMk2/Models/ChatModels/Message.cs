using System;

namespace ChallengeMk2.Models.ChatModels
{
    public class Message
    {
        public string Id { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public string Content { get; set; }

        public DateTime SentDate { get; set; }
    }
}
