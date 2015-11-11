using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Cirrious.MvvmCross.Droid.Support.AppCompat;
using DiodeTeam.Metroid.Core.ViewModels;
using DiodeTeam.Metroid.Droid.Views.Fragments;

namespace DiodeTeam.Metroid.Droid.Views.Activities
{
    [Activity (Label = "Metroid", Theme = "@style/MyTheme", LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxFragmentCompatActivity<MainViewModel>
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.activity_main);

            // Toolbar will now take on default actionbar characteristics
            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (toolbar);
            //SupportActionBar.SetDisplayHomeAsUpEnabled (true);

            // Metronome fragment
            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.content_frame, new MetronomeFragment { ViewModel = ViewModel.MetronomeViewModel })
                .Commit ();
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
                        .Add (Resource.Id.content_frame, new SettingsFragment { ViewModel = ViewModel.SettingsViewModel })
                        .AddToBackStack (null)
                        .Commit ();
                    break;
                case Android.Resource.Id.Home:
                    SupportFragmentManager.PopBackStack ();
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