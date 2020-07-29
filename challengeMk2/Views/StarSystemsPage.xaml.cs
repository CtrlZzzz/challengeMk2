using System;
using challengeMk2.Models;
using challengeMk2.ViewModels;
using Xamarin.Forms;

namespace challengeMk2.Views
{
    public partial class StarSystemsPage : ContentPage
    {
        public StarSystemsPage()
        {
            InitializeComponent();
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var vm = BindingContext as StarSystemsViewModel;

            if (vm!= null)
            {
                vm.NavigateTodetailPage = async (starSystem) => await Navigation.PushAsync(new SystemDetailPage(new SystemDetailViewModel(starSystem)));
            }
        }


        //private async void OnSystemTapped(object sender, EventArgs args)
        //{
        //    var layout = (BindableObject)sender;
        //    var system = (StarSystem)layout.BindingContext;

            

        //    await Navigation.PushAsync(new SystemDetailPage(new SystemDetailViewModel(system)));
        //}

    }
}
