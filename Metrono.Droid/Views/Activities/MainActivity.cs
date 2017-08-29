using Android.App;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using DiodeCompany.Metrono.Core.Messages;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Views.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace DiodeCompany.Metrono.Droid.Views.Activities
{
    [Activity (Label = "@string/app_name", Theme = "@style/main_theme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>, AudioManager.IOnAudioFocusChangeListener
    {
        private MetronomeFragment _metronomeFragment;
        private SettingsFragment _settingsFragment;
        
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);
            SetContentView (Resource.Layout.activity_main);

            // Toolbar will now take on default actionbar characteristics
            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (toolbar);

            // Metronome fragment
            _metronomeFragment = new MetronomeFragment();
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.content_frame, _metronomeFragment)
                .Commit ();
           
            // Settings fragment
            _settingsFragment = new SettingsFragment();

            // AudioManager
            var audioManager = GetSystemService(AudioService) as AudioManager;
            if (audioManager != null)
            {
                audioManager.RequestAudioFocus(this, Stream.Music, AudioFocus.Gain);
            }

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
                case Resource.Id.menu_settings:
                    // Settings fragment
                    SupportFragmentManager.BeginTransaction ()
                        .SetCustomAnimations(Resource.Animation.abc_slide_in_bottom, 0, 0, Resource.Animation.abc_slide_out_bottom)
                        .Add (Resource.Id.content_frame, _settingsFragment)
                        .AddToBackStack(null)
                        .Commit ();
                    return true;
                case Resource.Id.menu_tutorial:
                    // Tutorial activity
                    ViewModel.TutorialCommand.Execute();
                    return true;
            }

            return base.OnOptionsItemSelected (item);
        }

        protected override void OnPause ()
        {
            base.OnPause();

            var messenger = Mvx.Resolve<IMvxMessenger>();
            if(IsScreenAwake())
            {
                messenger.Publish(new LifeCycleMessage (this, LifeCycleEvent.Stop));
            }
            else
            {
                messenger.Publish(new LifeCycleMessage (this, LifeCycleEvent.Lock));
            }
        }

        protected override void OnResume ()
        {
            base.OnResume();

            Mvx.Resolve<IMvxMessenger>().Publish(new LifeCycleMessage (this, LifeCycleEvent.Start));
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();

            Mvx.Resolve<IMvxMessenger>().Publish(new LifeCycleMessage (this, LifeCycleEvent.Destroy));
        }

        public void OnAudioFocusChange (AudioFocus focusChange)
        {
            if(focusChange == AudioFocus.LossTransient)
            {
                Mvx.Resolve<IMvxMessenger>().Publish(new LifeCycleMessage (this, LifeCycleEvent.Stop));
            }
        }

        private bool IsScreenAwake()
        {
            var powerManager = GetSystemService(PowerService) as PowerManager;
            if (powerManager != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.KitkatWatch)
                {
                    return powerManager.IsInteractive;
                }
                else
                {
                    // Disable the obsolete warning message
#pragma warning disable 618
                    return powerManager.IsScreenOn;
#pragma warning restore 618
                }
            }
            return true;
        }
    }
}