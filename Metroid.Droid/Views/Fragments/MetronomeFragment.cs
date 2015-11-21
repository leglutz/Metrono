using Android.Animation;
using Android.OS;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeTeam.Metroid.Core.Models;
using DiodeTeam.Metroid.Core.Services;
using DiodeTeam.Metroid.Core.ViewModels;
using DiodeTeam.Metroid.Droid.Views.Activities;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Android.Content;

namespace DiodeTeam.Metroid.Droid.Views.Fragments
{
    public class MetronomeFragment : MvxFragment<MetronomeViewModel>, View.IOnTouchListener
    {
        private readonly Settings _settings;
        private readonly Vibrator _vibrator;

        private ObjectAnimator _backgroundColorAnimator;

        public MetronomeFragment(ISettingsService settingsService, IMvxAndroidGlobals globals)
        {
            _settings = settingsService.Settings;
            _vibrator = globals.ApplicationContext.GetSystemService(Android.Content.Context.VibratorService) as Vibrator;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_metronome, null);

            HasOptionsMenu = true;

            // Beats layout background animation
            var beatsLayout = view.FindViewById<View>(Resource.Id.beats_layout);
            _backgroundColorAnimator = ObjectAnimator.OfObject (beatsLayout, "backgroundColor", new ArgbEvaluator (), _settings.BlinkColor, 0);
            beatsLayout.SetOnTouchListener (this);

            // GridView
            var gridView = view.FindViewById<MvxGridView>(Resource.Id.grid_view);
            gridView.Adapter = new CustomAdapter (Activity, (MvxAndroidBindingContext)BindingContext);
            gridView.SetOnTouchListener (this);

            // Measure fragment
            ChildFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.measure_frame, new MeasureFragment { ViewModel = ViewModel.MeasureViewModel })
                .Commit ();
            
            return view;
        }

        public class CustomAdapter : MvxAdapter
        {
            public CustomAdapter(Context context, IMvxAndroidBindingContext bindingContext)
                : base(context, bindingContext)
            {
            }

            protected override View GetBindableView(View convertView, object source, int templateId)
            {
                var beat = (Beat)source;
                Mvx.Trace (beat.Number.ToString ());

                return base.GetBindableView(convertView, source, templateId);
            }
        }

        public bool OnTouch (View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                ViewModel.StartStopCommand.Execute();
            }

            return true;
        }

        public override void OnPrepareOptionsMenu (IMenu menu)
        {
            menu.FindItem (Resource.Id.settings_menu).SetVisible(true);
            ((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled (false);
            ((MainActivity)Activity).SupportActionBar.Title = GetString(Resource.String.app_name);

            base.OnPrepareOptionsMenu (menu);
        }

        public override void OnResume ()
        {
            base.OnResume ();

            ViewModel.Metronome.BeatStarted += OnBeatStarted;
        }

        public override void OnPause ()
        {
            ViewModel.Metronome.BeatStarted -= OnBeatStarted;

            base.OnPause ();
        }

        private void OnBeatStarted (object sender, Beat beat)
        {
            // Blink
            if (_settings.Blink)
            {
                Activity.RunOnUiThread (() => {
                    _backgroundColorAnimator.SetObjectValues(_settings.BlinkColor, 0);
                    _backgroundColorAnimator.SetDuration ((long)(beat.Duration * 1000));
                    _backgroundColorAnimator.Start ();
                });
            }

            // Vibration
            if (_settings.Vibration)
            {
                _vibrator.Vibrate ((long)(beat.Duration / 4.0 * 1000));
            }
        }
    }
}