using System;
using System.Collections.Generic;
using System.Text;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Xamarin.Forms;

namespace ChallengeMk2.ViewModels
{
    class MainTabbedViewModel : PrismBaseViewModel
    {
        public MainTabbedViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
