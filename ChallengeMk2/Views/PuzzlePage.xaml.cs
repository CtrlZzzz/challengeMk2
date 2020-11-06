using ChallengeMk2.ViewModels;
using Xamarin.Forms;

namespace ChallengeMk2.Views
{
    public partial class PuzzlePage : ContentPage
    {
        public PuzzlePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var vm = BindingContext as PuzzleViewModel;

            vm.InitializeViewModel();
        }
    }
}
