using Android.App;
using Android.Content.PM;
using Android.Gms.Analytics;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

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
            var googleAnalytics = GoogleAnalytics.GetInstance(ApplicationContext);
            googleAnalytics.SetLocalDispatchPeriod(60);
            var tracker = googleAnalytics.NewTracker("UA-70767993-1");
            tracker.EnableExceptionReporting(true);
            tracker.EnableAutoActivityTracking(true);
            #endif

			base.OnCreate (bundle);
		}
	}
}
