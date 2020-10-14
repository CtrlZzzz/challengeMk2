using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using ChallengeMk2.Services;
using ChallengeMk2.DataBase;
using ChallengeMk2.Views;
using ChallengeMk2.ViewModels;
using Prism.Navigation;

namespace ChallengeMk2
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            ConfigureServices();

            //MainPage = new AppShell();
            //await NavigationService.NavigateAsync("MainTabbedPage/StarSystemsPage");
            await NavigationService.NavigateAsync("MainTabbedPage?selectedTab=StarSystemsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainTabbedPage>();
            containerRegistry.RegisterForNavigation<StarSystemsPage, StarSystemsViewModel>();
            containerRegistry.RegisterForNavigation<PuzzlePage, PuzzleViewModel>();
        }

        void ConfigureServices()
        {
            DependencyService.Register<ILocalDataService, SQLiteDataService>();
            DependencyService.Register<IWebDataService, ApiDataService>();
            DependencyService.Register<IMapperService, SystemDbMapperService>();
            DependencyService.Register<IStarSystemService, StarSystemService>();
            DependencyService.Register<IPuzzleService, PuzzleService>();

            //Debug
            DependencyService.Register<MockDataStore>();
        }
    }
}
