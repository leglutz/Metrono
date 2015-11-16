using Android.App;
using Android.Content.PM;
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
        private SettingsFragment _settingsFragment;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.activity_main);

            // TODO delete
            var metrics = Resources.DisplayMetrics;
            var height = (int) ((metrics.HeightPixels)/Resources.DisplayMetrics.Density);
            var width = (int) ((metrics.WidthPixels)/Resources.DisplayMetrics.Density);

            // Toolbar will now take on default actionbar characteristics
            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (toolbar);

            // Metronome fragment
            var metronomeFragment = Mvx.IocConstruct<MetronomeFragment>();
            metronomeFragment.ViewModel = ViewModel.MetronomeViewModel; 
            SupportFragmentManager.BeginTransaction ()
                .Add (Resource.Id.content_frame, metronomeFragment)
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
                    /*SupportFragmentManager.BeginTransaction ()
                        .SetCustomAnimations(Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_bottom)
                        .Replace (Resource.Id.content_frame, _metronomeFragment)
                        .Commit ();*/
                    break;
            }

            return base.OnOptionsItemSelected (item);
        }

        protected override void OnPause ()
        {
            base.OnPause ();

            ViewModel.MetronomeViewModel.Metronome.Stop();
        }
    }
}