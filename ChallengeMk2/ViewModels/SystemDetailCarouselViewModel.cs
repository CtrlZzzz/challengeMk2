using ChallengeMk2.Models;
using Prism.Navigation;
using Prism.AppModel;


namespace ChallengeMk2.ViewModels
{
    public class SystemDetailCarouselViewModel : PrismBaseViewModel, IAutoInitialize
    {
        public SystemDetailCarouselViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Star System Details";
        }


        StarSystem currentSystem;
        public StarSystem CurrentSystem
        {
            get => currentSystem;
            set => SetProperty(ref currentSystem, value);
        }
    }
}
