using System;
using System.Collections.Generic;

using ChallengeMk2.ViewModels;
using ChallengeMk2.Models;
using Xamarin.Forms;

namespace ChallengeMk2.Views
{
    public partial class SystemDetailCarouselPage : ContentPage
    {
        private readonly StarSystem currentSystem;


        //public SystemDetailCarouselPage(SystemDetailCarouselViewModel selectedSystemViewModel)
        //{
        //    InitializeComponent();

        //    BindingContext = selectedSystemViewModel;
        //}

        public SystemDetailCarouselPage(StarSystem selectedSystem)
        {
            InitializeComponent();

            currentSystem = selectedSystem; 
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as SystemDetailCarouselViewModel;

            vm.GetCurrentSystem(currentSystem);
            await vm.UpdateSystemData();
        }
    }
}
