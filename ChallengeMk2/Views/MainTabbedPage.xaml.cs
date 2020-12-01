using Xamarin.Forms;
using Plugin.SharedTransitions;
using System.Diagnostics;

namespace ChallengeMk2.Views
{
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
        }

        void SharedTransitionNavigationPage_OnTransitionStarted(object sender, SharedTransitionEventArgs e)
        {
            Debug.WriteLine($"From event: Transition started - {e.PageFrom}|{e.PageTo}|{e.NavOperation}");
        }

        void SharedTransitionNavigationPage_OnTransitionEnded(object sender, SharedTransitionEventArgs e)
        {
            Debug.WriteLine($"From event: Transition ended - {e.PageFrom}|{e.PageTo}|{e.NavOperation}");
        }

        void SharedTransitionNavigationPage_OnTransitionCancelled(object sender, SharedTransitionEventArgs e)
        {
            Debug.WriteLine($"From event: Transition cancelled - {e.PageFrom}|{e.PageTo}|{e.NavOperation}");
        }
    }
}
