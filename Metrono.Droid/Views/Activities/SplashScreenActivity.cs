using Android.App;
using Android.Content.PM;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using DiodeCompany.Metrono.Droid.Helpers;

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
            // Initialize Google Analytics
            GoogleAnalyticsHelper.Instance.Initialize(Application);

			base.OnCreate (bundle);
		}
	}
}
