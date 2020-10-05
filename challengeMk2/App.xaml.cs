using Xamarin.Forms;
using ChallengeMk2.Services;
using ChallengeMk2.DataBase;

namespace ChallengeMk2
{
    public partial class App : Application
    {
        public static ILocalDataService Database { get; set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IPuzzleService, PuzzleService>();

            //Debug
            DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
