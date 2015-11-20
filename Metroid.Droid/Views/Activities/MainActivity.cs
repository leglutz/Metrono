using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Support.AppCompat;
using DiodeTeam.Metroid.Core.Helpers;
using DiodeTeam.Metroid.Core.ViewModels;
using DiodeTeam.Metroid.Droid.Views.Fragments;
using MvvmCross.Plugins.Messenger;

namespace DiodeTeam.Metroid.Droid.Views.Activities
{
    [Activity (Label = "@string/app_name", Theme = "@style/MyTheme", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        private readonly IMvxMessenger _messenger;

        private AdView _adView;
        private SettingsFragment _settingsFragment;

        public MainActivity ()
        {
            _messenger = Mvx.Resolve<IMvxMessenger>();
        }

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.activity_main);

            // TODO delete
            var metrics = Resources.DisplayMetrics;
            var contentView = FindViewById<View> (Window.IdAndroidContent);
            contentView.ViewTreeObserver.GlobalLayout += (sender, e) => {
                var height = (int) ((contentView.Height)/Resources.DisplayMetrics.Density);
                var width = (int) ((contentView.Width)/Resources.DisplayMetrics.Density);
                var sizeText = FindViewById<Android.Widget.TextView> (Resource.Id.size_text);
                sizeText.Text = height + " x " + width + " " + Resources.DisplayMetrics.DensityDpi.ToString();
            };

            // Toolbar will now take on default actionbar characteristics
            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (toolbar);

            // AdView
            var adRequest = new AdRequest.Builder ()
                .AddTestDevice (AdRequest.DeviceIdEmulator)
                .AddTestDevice("8AB3CB50EEA11C715DC6E0F15ADC7CEC") // Samsung Galaxy S2
                .AddTestDevice("A538B166F286F87A47B2D600E0968262") // Motorola Droid Razr
                .AddTestDevice("DA017FBAC24CCDAAF97AD0B634BCB4A0") // Motorola Moto X
                .AddTestDevice("031CCE28BE1E56DE518153AF164C4CEF") // Sony Xperia Z
                .AddTestDevice("BEA4D61AAD98987FF358CE1A34E60381") // HTC One X
                .AddTestDevice("357B0383DFBCA45B3FF9CD0B86138DF5") // Samsung Galaxy S5
                .AddTestDevice("507F28A4B85875B7134D08CD5B7FD2AC") // Samsung Galaxy S6
                .AddTestDevice("E2492DDF275420924197F1222E8F6B3C") // Motorola Nexus 6
                //.AddTestDevice("3109107719D5CFB27B6793173D75B4D0") // Sony Xperia Z Compact
                .Build ();
            _adView = FindViewById<AdView> (Resource.Id.ad_view);
            _adView.LoadAd(adRequest);

            // Metronome fragment
            var metronomeFragment = Mvx.IocConstruct<MetronomeFragment>();
            metronomeFragment.ViewModel = ViewModel.MetronomeViewModel; 
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.content_frame, metronomeFragment)
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
            _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Hide));
            _adView.Pause ();

            base.OnPause ();
        }

        protected override void OnResume ()
        {
            base.OnResume ();

            _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Show));
            _adView.Resume ();
        }

        protected override void OnDestroy ()
        {
            _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Dispose));
            _adView.Destroy ();

            base.OnDestroy ();
        }
    }
}