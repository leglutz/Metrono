using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeTeam.Metroid.Core.Models;
using DiodeTeam.Metroid.Core.Services;
using DiodeTeam.Metroid.Core.ViewModels;
using DiodeTeam.Metroid.Droid.Views.Activities;

namespace DiodeTeam.Metroid.Droid.Views.Fragments
{
    public class MetronomeFragment : MvxFragment<MetronomeViewModel>
    {
        private readonly Settings _settings;

        private View _beatsCard;

        public MetronomeFragment(ISettingsService settingsService)
        {
            _settings = settingsService.Settings;

            RetainInstance = true;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_metronome, null);

            HasOptionsMenu = true;

            // Get the beats card (for animation purpose)
            _beatsCard = view.FindViewById<View>(Resource.Id.beats_card);

            // Measure fragment
            ChildFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.measure_frame, new MeasureFragment { ViewModel = ViewModel.MeasureViewModel })
                .Commit ();
            
            return view;
        }

        public override void OnPrepareOptionsMenu (IMenu menu)
        {
            menu.FindItem (Resource.Id.settings_menu).SetVisible(true);
            ((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled (false);
            ((MainActivity)Activity).SupportActionBar.Title = "Metroid";

            base.OnPrepareOptionsMenu (menu);
        }

        public override void OnResume ()
        {
            base.OnResume ();

            ViewModel.Metronome.BeatStarted += OnBeatStarted;
        }

        public override void OnPause ()
        {
            base.OnPause ();

            ViewModel.Metronome.BeatStarted -= OnBeatStarted;
        }

        private void OnBeatStarted (object sender, Beat beat)
        {
        }
    }
}