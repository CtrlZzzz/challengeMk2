using Xamarin.Forms;
using ChallengeMk2.Services;
using ChallengeMk2.DataBase;

namespace ChallengeMk2
{
    public partial class App : Application
    {
        //public static ILocalDataService Database { get; set; }

        public App()
        {
            InitializeComponent();

            ConfigureServices();

            MainPage = new AppShell();
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