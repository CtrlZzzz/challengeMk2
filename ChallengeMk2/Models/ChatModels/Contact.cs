using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.Models.ChatModels
{
    public class Contact
    {
        public string Id { get; set; }

        public string ContactId { get; set; }

        public string ContactName { get; set; }

        public DateTime AddedDate { get; set; }

        public IList<Message> Messages { get; set; }
    }
}
