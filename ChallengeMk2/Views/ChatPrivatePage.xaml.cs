using Xamarin.Forms;
using ChallengeMk2.ViewModels;

namespace ChallengeMk2.Views
{
    public partial class ChatPrivatePage : ContentPage
    {
        public ChatPrivatePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as ChatPrivatePageViewModel;
            vm.CollectionToScroll = MessageCollection;

            //MessageCollection.ScrollTo(vm.PrivateMessages.Count - 1);
        }
    }
}
