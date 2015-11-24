using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.Media;
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
    public class MainActivity : MvxAppCompatActivity<MainViewModel>, AudioManager.IOnAudioFocusChangeListener
    {
        private readonly IMvxMessenger _messenger;

        private AdView _adView;
        private SettingsFragment _settingsFragment;
        private PowerManager _powerManager;

        public MainActivity ()
        {
           _messenger = Mvx.Resolve<IMvxMessenger>();
        }

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.activity_main);

            // Toolbar will now take on default actionbar characteristics
            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (toolbar);

            // Metronome fragment
            var metronomeFragment = Mvx.IocConstruct<MetronomeFragment>();
            metronomeFragment.ViewModel = ViewModel.MetronomeViewModel; 
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.content_frame, metronomeFragment)
                .Commit ();
           
            // Settings fragment
            _settingsFragment =  Mvx.IocConstruct<SettingsFragment>();
            _settingsFragment.ViewModel = ViewModel.SettingsViewModel; 

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
                .AddTestDevice("2AEBA62B2AF8435AC4C3FD4ACA680610") // Samsung Galaxy Mega 6.3
                .AddTestDevice("0051822137F6B272857CBD7CE46DB438") // Sony Xperia Z Ultra HSPA
                //.AddTestDevice("3109107719D5CFB27B6793173D75B4D0") // Sony Xperia Z Compact
                .Build ();
            _adView = FindViewById<AdView> (Resource.Id.ad_view);
            _adView.LoadAd(adRequest);

            // AudioManager
            var audioManager = (AudioManager) GetSystemService(Android.Content.Context.AudioService);
            audioManager.RequestAudioFocus(this, Stream.Music, AudioFocus.Gain);

            // PowerManager
            _powerManager = (PowerManager) GetSystemService(Android.Content.Context.PowerService);

            // Keep the screen always on
            Window.AddFlags (WindowManagerFlags.KeepScreenOn);
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
            // Don't hide if locked/sleep only
            var isSceenAwake = (Build.VERSION.SdkInt < BuildVersionCodes.KitkatWatch ? _powerManager.IsScreenOn : _powerManager.IsInteractive);
            if(isSceenAwake)
            {
                _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Hide));   
            }
            _adView.Pause ();

            base.OnPause ();
        }

        protected override void OnResume ()
        {
            base.OnResume ();

            var isSceenAwake = (Build.VERSION.SdkInt < BuildVersionCodes.KitkatWatch ? _powerManager.IsScreenOn : _powerManager.IsInteractive);
            if(isSceenAwake)
            {
            }

            _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Show));
            _adView.Resume ();
        }

        protected override void OnDestroy ()
        {
            _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Dispose));
            _adView.Destroy ();

            base.OnDestroy ();
        }

        public void OnAudioFocusChange (AudioFocus focusChange)
        {
            if(focusChange == AudioFocus.LossTransient)
            {
                _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Hide));   
            }
        }
    }
}