using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.Models.ChatModels
{
    public class RegisterForm
    {
        public RegisterForm(string username, string password, string firstname, string lastname)
        {
            Username = username;
            Password = password;
            FirstName = firstname;
            LastName = lastname;
        }


        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
