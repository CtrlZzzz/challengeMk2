using challengeMk2.ViewModels;

using Xamarin.Forms;

namespace challengeMk2.Views
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
