using ChallengeMk2.ViewModels;
using Xamarin.Forms;
using ChallengeMk2.DataBase;

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

            var vm = BindingContext as StarSystemsViewModel;

            if (vm != null)
            {
                vm.NavigateTodetailPage = async (starSystem) => await Navigation.PushAsync(new SystemDetailCarouselPage(starSystem));
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as StarSystemsViewModel;

            //To trigger Auto PullToRefresh collection
            if (vm.Systems.Count == 0)
            {
                vm.IsBusy = true;
            }
        }
    }
}
