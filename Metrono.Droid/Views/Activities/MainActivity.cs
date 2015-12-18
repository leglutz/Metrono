﻿using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.Media;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Support.AppCompat;
using DiodeCompany.Metrono.Core.Messages;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Helpers;
using DiodeCompany.Metrono.Droid.Views.Fragments;
using MvvmCross.Plugins.Messenger;

namespace DiodeCompany.Metrono.Droid.Views.Activities
{
    [Activity (Label = "@string/app_name", Theme = "@style/main_theme", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>, AudioManager.IOnAudioFocusChangeListener
    {
        private readonly IMvxMessenger _messenger;

        private SettingsFragment _settingsFragment;
        private AdView _adView;
        private AudioManager _audioManager;
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
            var metronomeFragment = new MetronomeFragment() { ViewModel = ViewModel.MetronomeViewModel };
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.content_frame, metronomeFragment)
                .Commit ();
           
            // Settings fragment
            _settingsFragment = new SettingsFragment() { ViewModel = ViewModel.SettingsViewModel };

            // AdView
            _adView = FindViewById<AdView> (Resource.Id.ad_view);
            #if !DEBUG
            var adRequest = new AdRequest.Builder ()
                .AddTestDevice (AdRequest.DeviceIdEmulator)
                .Build ();
            // Initialize Insights
            _adView.LoadAd(adRequest);
            #endif

            // AudioManager
            _audioManager = (AudioManager) GetSystemService(Android.Content.Context.AudioService);
            _audioManager.RequestAudioFocus(this, Stream.Music, AudioFocus.Gain);

            // PowerManager
            _powerManager = (PowerManager) GetSystemService(Android.Content.Context.PowerService);

            // Keep the screen always on
            Window.AddFlags (WindowManagerFlags.KeepScreenOn);

            // We are on main page
            GoogleAnalyticsHelper.Instance.TrackPage("Main");
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
                    GoogleAnalyticsHelper.Instance.TrackPage("Settings");
                    break;
                case Android.Resource.Id.Home:
                    // Metronome fragment
                    SupportFragmentManager.PopBackStack();
                    GoogleAnalyticsHelper.Instance.TrackPage("Main");
                    break;
            }

            return base.OnOptionsItemSelected (item);
        }

        protected override void OnPause ()
        {
            if(IsScreenAwake())
            {
                _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Stop));   
            }
            else
            {
                _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Lock));   
            }
            _adView.Pause ();

            base.OnPause ();
        }

        protected override void OnResume ()
        {
            base.OnResume ();

            _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Start));
            _adView.Resume ();
        }

        protected override void OnDestroy ()
        {
            _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Destroy));
            _adView.Destroy ();

            base.OnDestroy ();
        }

        public void OnAudioFocusChange (AudioFocus focusChange)
        {
            if(focusChange == AudioFocus.LossTransient)
            {
                _messenger.Publish<LifeCycleMessage> (new LifeCycleMessage (this, LifeCycleEvent.Stop));   
            }
        }

        private bool IsScreenAwake()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.KitkatWatch)
            {
                return _powerManager.IsInteractive;
            }
            else 
            {
                // Disable the obsolete warning message
                #pragma warning disable 618
                return _powerManager.IsScreenOn;
                #pragma warning restore 618
            }
        }
    }
}