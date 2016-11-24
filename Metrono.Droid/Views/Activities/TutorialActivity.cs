using Android.App;
using Android.OS;
using Android.Support.V4.View;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Controls.ViewPagerIndicator;
using DiodeCompany.Metrono.Droid.Views.Adapters;
using MvvmCross.Droid.Support.V4;

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
        }
	}
}
