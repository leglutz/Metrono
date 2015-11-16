using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Droid.Views;

namespace DiodeTeam.Metroid.Droid.Activities
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/MyTheme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : MvxSplashScreenActivity
    {
        public SplashScreenActivity() : base(Resource.Layout.activity_splash_screen)
        {
        }
    }
}