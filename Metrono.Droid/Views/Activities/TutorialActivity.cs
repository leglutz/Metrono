using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Cirrious.MvvmCross.Droid.Support.Fragging;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Controls.ViewPagerIndicator;
using DiodeCompany.Metrono.Droid.Helpers;
using DiodeCompany.Metrono.Droid.Views.Adapters;

namespace DiodeCompany.Metrono.Droid.Activities
{
    [Activity (Theme = "@style/tutorial_theme")]
    public class TutorialActivity : MvxFragmentActivity<TutorialViewModel>
	{
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);
            SetContentView (Resource.Layout.activity_tutorial);

            // Pager
            var pager = FindViewById<ViewPager>(Resource.Id.pager);
            pager.Adapter = new TutorialFragmentAdapter(SupportFragmentManager);

            // Indicator
            var indicator = FindViewById<TitlePageIndicatorView>(Resource.Id.indicator);
            indicator.SetViewPager(pager);

            GoogleAnalyticsHelper.Instance.TrackPage("Tutorial");
        }
	}
}
