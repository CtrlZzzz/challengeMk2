using Prism.Mvvm;
using Prism.Navigation;

namespace ChallengeMk2.ViewModels
{
    public class PrismBaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public PrismBaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }


        public void Initialize(INavigationParameters parameters)
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public void Destroy()
        {
        }
    }
}
