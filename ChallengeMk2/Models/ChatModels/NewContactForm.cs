using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.Models.ChatModels
{
    public class NewContactForm
    {
        public NewContactForm(string userName, string contactId, string contactName)
        {
            Username = userName;
            ContactId = contactId;
            ContactName = contactName;
        }


        public string Username { get; set; }

        public string ContactId { get; set; }

        public string ContactName { get; set; }
    }
}
