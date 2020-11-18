using ChallengeMk2.ViewModels;
using Xamarin.Forms;

namespace ChallengeMk2.Views
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

            if (BindingContext is StarSystemsViewModel vm)
            {
                vm.NavigateTodetailPage = async (starSystem) => await Navigation.PushAsync(new SystemDetailCarouselPage(starSystem));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as StarSystemsViewModel;

            if (vm.Systems.Count == 0)
            {
                vm.IsBusy = true;
            }
        }
    }
}
