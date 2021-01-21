using Xamarin.Forms;
using ChallengeMk2.ViewModels;
using System.Threading.Tasks;

namespace ChallengeMk2.Views
{
    public partial class ChatRoomPage : ContentPage
    {
        public ChatRoomPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as ChatRoomPageViewModel;
            vm.CollectionToScroll = MessageCollection;

            //MessageCollection.ScrollTo(vm.RoomMessages.Count - 1);
        }
    }
}
