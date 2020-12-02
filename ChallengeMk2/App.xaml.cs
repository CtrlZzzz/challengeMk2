using Xamarin.Forms;
using ChallengeMk2.Services;
using ChallengeMk2.DataBase;
using ChallengeMk2.Views;
using ChallengeMk2.ViewModels;
using Prism;
using Prism.Ioc;

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

            Device.SetFlags(new string[] { "CarouselView_Experimental", "Brush_Experimental" });

            await NavigationService.NavigateAsync("MainTabbedPage?selectedTab=AboutPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedViewModel>();
            containerRegistry.RegisterForNavigation<StarSystemsPage, StarSystemsViewModel>();
            containerRegistry.RegisterForNavigation<SystemDetailCarouselPage, SystemDetailCarouselViewModel>();
            containerRegistry.RegisterForNavigation<PuzzlePage, PuzzleViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>();
            containerRegistry.RegisterForNavigation<ChatLoginPage, ChatLoginPageViewModel>();

            containerRegistry.RegisterSingleton<ILocalDataService, SQLiteDataService>();    
            containerRegistry.RegisterSingleton<IWebDataService, ApiDataService>();
            containerRegistry.RegisterSingleton<IMapperService, SystemDbMapperService>();
            containerRegistry.RegisterSingleton<IStarSystemService, StarSystemService>();
            containerRegistry.RegisterSingleton<IPuzzleService, PuzzleService>();
        }
    }
}