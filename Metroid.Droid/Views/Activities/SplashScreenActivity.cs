using Android.App;
using Android.Content.PM;
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
            // Initialize Insights
            Xamarin.Insights.Initialize (XamarinInsights.ApiKey, this);
			
			base.OnCreate (bundle);
		}
	}
}
