using ChallengeMk2.ViewModels;
using ChallengeMk2.Models;
using Xamarin.Forms;

namespace ChallengeMk2.Views
{
    public partial class SystemDetailCarouselPage : ContentPage
    {
        readonly StarSystem currentSystem;


        public SystemDetailCarouselPage(StarSystem selectedSystem)
        {
            InitializeComponent();

            currentSystem = selectedSystem; 
        }


        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    var vm = BindingContext as SystemDetailCarouselViewModel;

        //    vm.InitializeViewModel(currentSystem);
        //}
    }
}
