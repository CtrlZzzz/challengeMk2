using Xamarin.Forms;
using ChallengeMk2.ViewModels;

namespace ChallengeMk2.Views
{
    public partial class ChatPublicPage : ContentPage
    {
        public ChatPublicPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as ChatPublicPageViewModel;

            if (vm.PublicMessages.Count != 0)
            {
                MessageCollection.ScrollTo(vm.PublicMessages.Count - 1);
            }
        }
    }
}
