using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Cirrious.MvvmCross.Droid.Support.Fragging;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Controls.CirclePageIndicator;
using DiodeCompany.Metrono.Droid.Views.Adapters;

namespace DiodeCompany.Metrono.Droid.Activities
{
    [Activity (Theme = "@style/showcase_theme")]
    public class ShowcaseActivity : MvxFragmentActivity<ShowcaseViewModel>
	{
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.activity_showcase);

            // Pager
            var pager = FindViewById<ViewPager>(Resource.Id.pager);
            pager.Adapter = new ShowcaseFragmentAdapter(SupportFragmentManager);

            // Indicator
            var indicator = FindViewById<CirclePageIndicatorView>(Resource.Id.indicator);
            indicator.SetViewPager(pager);
        }
	}
}
