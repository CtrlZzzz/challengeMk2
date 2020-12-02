using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ChallengeMk2.Services;

namespace ChallengeMk2.ViewModels
{
    public class ChatLoginPageViewModel : PrismBaseViewModel
    {
        readonly IChatService chat;

        public ChatLoginPageViewModel(INavigationService navigationService, IChatService chatService) : base(navigationService)
        {
            Title = "Discussions";

            chat = chatService;
        }
    }
}
