using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ChallengeMk2.ViewModels
{
    public class ChatLoginPageViewModel : PrismBaseViewModel
    {
        public ChatLoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Discussions";
        }
    }
}
