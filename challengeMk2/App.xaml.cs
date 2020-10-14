using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using ChallengeMk2.Services;
using ChallengeMk2.DataBase;

namespace ChallengeMk2
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            ConfigureServices();

            MainPage = new AppShell();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
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
