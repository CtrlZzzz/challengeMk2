using Xamarin.Forms;
using ChallengeMk2.Services;
using ChallengeMk2.DataBase;
using ChallengeMk2.Views;
using ChallengeMk2.ViewModels;
using Prism;
using Prism.Ioc;
using Microsoft.Identity.Client;
using ChallengeMk2.MSAL;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

[assembly: ExportFont("fa-brands-400.ttf", Alias = "fab")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "fas")]
[assembly: ExportFont("fa-regular-400.ttf", Alias = "far")]
namespace ChallengeMk2
{
    public partial class App
    {
        public static object UIParent { get; set; } = null;

        //IAuthenticationService authenticationService;

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }


        protected override void OnStart()
        {
            base.OnStart();

            //App Center
            Distribute.UpdateTrack = UpdateTrack.Private;
            AppCenter.Start("ios=ba2f4158-a705-4035-9b95-ff7a15e60efb;" +
                            "android=ac728f87-3efb-410c-a98b-ff612f259ff8",
                            typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();

            Device.SetFlags(new string[] { "CarouselView_Experimental", "Brush_Experimental", "Shapes_Experimental" });

            //MSAL
            //authenticationService.InitializeAuthenticationService();

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
            containerRegistry.RegisterForNavigation<ChatMainPage, ChatMainPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatPublicPage, ChatPublicPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatRoomPage, ChatRoomPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatPrivatePage, ChatPrivatePageViewModel>();
            containerRegistry.RegisterForNavigation<ChatAddContactPage, ChatAddContactPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatAddRoomPage, ChatAddRoomPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatCreateAccountPage, ChatCreateAccountPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatLoginMsalPage, ChatLoginMsalPageViewModel>();

            containerRegistry.RegisterSingleton<ILocalDataService, SQLiteDataService>();    
            containerRegistry.RegisterSingleton<IWebDataService, ApiDataService>();
            containerRegistry.RegisterSingleton<IMapperService, SystemDbMapperService>();
            containerRegistry.RegisterSingleton<IStarSystemService, StarSystemService>();
            containerRegistry.RegisterSingleton<IPuzzleService, PuzzleService>();
            containerRegistry.RegisterSingleton<IChatService, ChatService>();
            containerRegistry.RegisterSingleton<IAuthenticationService, Aadb2cMsalService>();
        }
    }
}