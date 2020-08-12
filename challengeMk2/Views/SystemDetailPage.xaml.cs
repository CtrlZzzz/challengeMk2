using ChallengeMk2.ViewModels;

using Xamarin.Forms;

namespace ChallengeMk2.Views
{
    public partial class SystemDetailPage : ContentPage
    {
        public SystemDetailPage(SystemDetailViewModel selectedSystemViewModel)
        {
            InitializeComponent();

            BindingContext = selectedSystemViewModel;
        }
    }
}
