using Xamarin.Forms;
using ChallengeMk2.Services;
using ChallengeMk2.DataBase;
using ChallengeMk2.Views;
using ChallengeMk2.ViewModels;
using DryIoc;
using Prism;
using Prism.Ioc;
using Prism.DryIoc;
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

            //await NavigationService.NavigateAsync("MainTabbedPage/StarSystemsPage");
            await NavigationService.NavigateAsync("MainTabbedPage?selectedTab=AboutPage");
            //await NavigationService.NavigateAsync("/NavigationPage/MainTabbedPage?selectedTab=StarSystemsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedViewModel>();
            containerRegistry.RegisterForNavigation<StarSystemsPage, StarSystemsViewModel>();
            containerRegistry.RegisterForNavigation<SystemDetailCarouselPage, SystemDetailCarouselViewModel>();
            containerRegistry.RegisterForNavigation<PuzzlePage, PuzzleViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>();

            containerRegistry.RegisterSingleton<ILocalDataService, SQLiteDataService>();
            containerRegistry.RegisterSingleton<IWebDataService, ApiDataService>();
            containerRegistry.RegisterSingleton<IMapperService, SystemDbMapperService>();
            containerRegistry.RegisterSingleton<IStarSystemService, StarSystemService>();
            containerRegistry.RegisterSingleton<IPuzzleService, PuzzleService>();

            //debug
            containerRegistry.RegisterSingleton<MockDataStore>();
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
