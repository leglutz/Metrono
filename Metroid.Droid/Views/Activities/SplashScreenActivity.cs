using Android.App;
using Android.Content.PM;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using DiodeCompany.Metroid.Droid.Helpers;

namespace DiodeCompany.Metroid.Droid.Activities
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/MyTheme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreenActivity : MvxSplashScreenActivity
	{
		public SplashScreenActivity () : base (Resource.Layout.activity_splash_screen)
		{
		}

		protected override void OnCreate (Bundle bundle)
		{
            #if !DEBUG
            // Initialize Insights
            Xamarin.Insights.Initialize (XamarinInsights.ApiKey, this);
            // Initialize Google Analytics
            GoogleAnalyticsHelper.Instance.Initialize(ApplicationContext);
            #endif

			base.OnCreate (bundle);
		}
	}
}
