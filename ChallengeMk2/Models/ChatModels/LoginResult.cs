using System;
using System.Collections.Generic;
using System.Text;
using ChallengeMk2.Models.ChatModels;

namespace ChallengeMk2.Models.ChatModels
{
    public class LoginResult
    {
        public LoginResult(bool isSuccessful, string resultMessage)
        {
            IsSuccessful = isSuccessful;
            Message = resultMessage;
        }
        public LoginResult(bool isSuccessful, User user)
        {
            IsSuccessful = isSuccessful;
            LoggedUser = user;
        }


        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public User LoggedUser { get; set; }
    }
}
