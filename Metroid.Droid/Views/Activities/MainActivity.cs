using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Support.AppCompat;
using DiodeTeam.Metroid.Core.ViewModels;
using DiodeTeam.Metroid.Droid.Views.Fragments;

namespace DiodeTeam.Metroid.Droid.Views.Activities
{
    [Activity (Label = "@string/app_name", Theme = "@style/MyTheme", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxFragmentCompatActivity<MainViewModel>
    {
        private AdView _adView;
        private MetronomeFragment _metronomeFragment;
        private SettingsFragment _settingsFragment;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.activity_main);

            // Toolbar will now take on default actionbar characteristics
            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (toolbar);

            // AdView
            var adRequest = new AdRequest.Builder ()
                .AddTestDevice (AdRequest.DeviceIdEmulator)
                .AddTestDevice("A4345C113A901171A1CC2471BDAFF7DB")
                .AddTestDevice("9FBB55A713C3B6A69EECC6E4AEB2101C")
                .Build ();
            _adView = FindViewById<AdView> (Resource.Id.ad_view);
            _adView.LoadAd(adRequest);

            // Metronome fragment
            _metronomeFragment = Mvx.IocConstruct<MetronomeFragment>();
            _metronomeFragment.ViewModel = ViewModel.MetronomeViewModel; 
            SupportFragmentManager.BeginTransaction ()
                .Add (Resource.Id.content_frame, _metronomeFragment)
                .Commit ();
           
            // Settings fragment
            _settingsFragment =  Mvx.IocConstruct<SettingsFragment>();
            _settingsFragment.ViewModel = ViewModel.SettingsViewModel; 
        }

        public override bool OnCreateOptionsMenu (IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);

            return base.OnCreateOptionsMenu (menu);
        }

        public override bool OnOptionsItemSelected (IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.settings_menu:
                    // Settings fragment
                    SupportFragmentManager.BeginTransaction ()
                        .SetCustomAnimations(Resource.Animation.abc_slide_in_bottom, 0, 0, Resource.Animation.abc_slide_out_bottom)
                        .Add (Resource.Id.content_frame, _settingsFragment)
                        .AddToBackStack(null)
                        .Commit ();
                    break;
                case Android.Resource.Id.Home:
                    // Metronome fragment
                    SupportFragmentManager.PopBackStack();
                    break;
            }

            return base.OnOptionsItemSelected (item);
        }

        protected override void OnPause ()
        {
            ViewModel.MetronomeViewModel.Metronome.Stop();
            _adView.Pause ();

            base.OnPause ();
        }

        protected override void OnResume ()
        {
            base.OnResume ();

            _adView.Resume ();
        }

        protected override void OnDestroy ()
        {
            _adView.Destroy ();

            base.OnDestroy ();
        }
    }
}