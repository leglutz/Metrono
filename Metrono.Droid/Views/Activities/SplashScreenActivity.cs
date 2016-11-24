using Android.App;
using Android.Content.PM;
using Android.OS;
using DiodeCompany.Metrono.Droid.Helpers;
using MvvmCross.Droid.Views;

namespace DiodeCompany.Metrono.Droid.Activities
{
    [Activity (MainLauncher = true, Theme = "@style/splash_screen_theme", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreenActivity : MvxSplashScreenActivity
	{
		public SplashScreenActivity () : base (Resource.Layout.activity_splash_screen)
		{
		}

		protected override void OnCreate (Bundle bundle)
		{
            // Initialize Xamarin Insights
            XamarinInsightsHelper.Instance.Initialize(Application);

			base.OnCreate (bundle);
		}
	}
}
