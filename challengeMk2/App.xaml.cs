using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using ChallengeMk2.Services;
using ChallengeMk2.DataBase;
using ChallengeMk2.Views;
using ChallengeMk2.ViewModels;
using Prism.Navigation;

[assembly: ExportFont("fa-brands-400.ttf", Alias = "fab")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "fas")]
[assembly: ExportFont("fa-regular-400.ttf", Alias = "far")]
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
            await NavigationService.NavigateAsync("MainTabbedPage?selectedTab=AboutPage");
            //await NavigationService.NavigateAsync("/NavigationPage/MainTabbedPage?selectedTab=StarSystemsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedViewModel>();
            containerRegistry.RegisterForNavigation<StarSystemsPage, StarSystemsViewModel>();
            containerRegistry.RegisterForNavigation<PuzzlePage, PuzzleViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>();
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
