using Xamarin.Forms;
using Foundation;
using UIKit;
using Prism;
using Prism.Ioc;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Microsoft.Identity.Client;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

namespace ChallengeMk2.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            //FFImageLoading.Svg.Forms
            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);

            //App Center
            Distribute.DontCheckForUpdatesInDebug();
            AppCenter.Start("ba2f4158-a705-4035-9b95-ff7a15e60efb",
                             typeof(Analytics), typeof(Crashes), typeof(Distribute));

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        //MSAL
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
            return base.OpenUrl(app, url, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
