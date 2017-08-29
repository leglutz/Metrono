using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace DiodeCompany.Metrono.Droid.Activities
{
    [Activity (MainLauncher = true, Theme = "@style/splash_screen_theme", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreenActivity : MvxSplashScreenActivity
	{
		public SplashScreenActivity () : base (Resource.Layout.activity_splash_screen)
		{
		}
	}
}
