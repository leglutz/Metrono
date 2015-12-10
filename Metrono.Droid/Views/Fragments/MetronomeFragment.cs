using Android.Animation;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeCompany.Metrono.Core.Messages;
using DiodeCompany.Metrono.Core.Models;
using DiodeCompany.Metrono.Core.Services;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Helpers;
using DiodeCompany.Metrono.Droid.Views.Activities;
using DiodeCompany.Metrono.Droid.Views.Adapters;
using MvvmCross.Plugins.Messenger;

namespace DiodeCompany.Metrono.Droid.Views.Fragments
{
    public class MetronomeFragment : MvxFragment<MetronomeViewModel>, View.IOnTouchListener
    {
        private readonly Settings _settings;
        private readonly MvxSubscriptionToken _metronomeMessageSubscriptionToken;

        private ObjectAnimator _backgroundColorAnimator;
        private GridView _gridView;

        public MetronomeFragment()
        {
            _settings = Mvx.Resolve<ISettingsService>().Settings;
            _metronomeMessageSubscriptionToken = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<MetronomeMessage> (OnMetronomeMessage);

            RetainInstance = true;
        }
            
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_metronome, null);

            HasOptionsMenu = true;

            // Beats layout background animation
            var beatsLayout = view.FindViewById<View>(Resource.Id.beats_layout);
            _backgroundColorAnimator = ObjectAnimator.OfObject (beatsLayout, "backgroundColor", new ArgbEvaluator (), _settings.FlashColor, 0);
            beatsLayout.SetOnTouchListener (this);

            // GridView
            _gridView = view.FindViewById<GridView>(Resource.Id.grid_view);
            // Do it without MvvmCross, because there's is a problem with MvvmCross adapter and MvxGridView (the first item is not displayed)
            _gridView.Adapter = new BeatAdapter (Activity, _gridView, ViewModel.MeasureViewModel.Measure.BeatList);
            _gridView.SetOnTouchListener (this);

            // Measure fragment
            var measureFragment = new MeasureFragment()  { ViewModel = ViewModel.MeasureViewModel };
            ChildFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.measure_frame, measureFragment)
                .Commit ();

            return view;
        }

        public bool OnTouch (View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                ViewModel.StartStopCommand.Execute();
                if(ViewModel.Metronome.IsPlaying)
                {
                    GoogleAnalyticsHelper.Instance.TrackEvent ("Metronome", "Start");
                }
                else
                {
                    GoogleAnalyticsHelper.Instance.TrackEvent ("Metronome", "Stop");
                }
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

        private void OnMetronomeMessage (MetronomeMessage metronomeMessage)
        {
            switch(metronomeMessage.MetronomeEvent)
            {
                case MetronomeEvent.BeatStarted:
                    {
                        // Beat
                        var beat = metronomeMessage.Beat;
                        var beatView = _gridView.GetChildAt (beat.Number - 1);
                        if (beatView != null)
                        {
                            ((CardView)beatView).Alpha = 1f;
                            ((CardView)beatView).CardElevation = 2;
                        }

                        // Flash
                        if (_settings.Flash)
                        {
                            _backgroundColorAnimator.SetObjectValues (_settings.FlashColor, 0);
                            _backgroundColorAnimator.SetDuration ((long)(beat.Duration * 1000));
                            _backgroundColorAnimator.Start ();
                        }
                    }
                    break;
                case MetronomeEvent.BeatFinished:
                    {
                        // Beat
                        var beat = metronomeMessage.Beat;
                        var beatView = _gridView.GetChildAt (beat.Number - 1);
                        if(beatView != null)
                        {
                            ((CardView)beatView).Alpha = 0.5f;
                            ((CardView)beatView).CardElevation = 0;
                        }
                    }
                    break;
            }
        }
    }
}