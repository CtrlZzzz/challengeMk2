using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
using Prism;
using Prism.Ioc;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Android.Views;
using Microsoft.Identity.Client;
using Android.Content;

namespace ChallengeMk2.Droid
{
    [Activity(Label = "ChallengeMk2", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //Show content behind status bar
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutFullscreen | (StatusBarVisibility) SystemUiFlags.LayoutStable;

            //FFImageLoading.Svg.Forms
            CachedImageRenderer.Init(true);
            var ignore = typeof(SvgCachedImage);

            //App Center
            Distribute.SetEnabledForDebuggableBuild(true);
            Distribute.UpdateTrack = UpdateTrack.Private;
            AppCenter.Start("ac728f87-3efb-410c-a98b-ff612f259ff8",
                            typeof(Analytics), typeof(Crashes), typeof(Distribute));

            LoadApplication(new App(new AndroidInitializer()));

            //MSAL
            App.UIParent = this;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}