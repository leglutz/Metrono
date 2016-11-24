using Android.Animation;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using DiodeCompany.Metrono.Core.Models;
using DiodeCompany.Metrono.Core.Services;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Views.Activities;
using DiodeCompany.Metrono.Droid.Views.Adapters;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;
using System;

namespace DiodeCompany.Metrono.Droid.Views.Fragments
{
    [Register("diodecompany.metrono.droid.views.fragments.MetronomeFragment")]
    public class MetronomeFragment : MvxFragment<MetronomeViewModel>, View.IOnClickListener, AdapterView.IOnTouchListener, GestureDetector.IOnGestureListener
    {
        private Settings _settings;
        private ObjectAnimator _backgroundColorAnimator;
        private GridView _gridView;
        private GestureDetector _gestureDetector;

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel = Mvx.Resolve<MetronomeViewModel>();
            _settings = Mvx.Resolve<ISettingsService>().Settings;

            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_metronome, null);

            HasOptionsMenu = true;

            // Beats layout background animation
            var beatsLayout = view.FindViewById<View>(Resource.Id.beats_layout);
            beatsLayout.SetOnClickListener (this);
            _backgroundColorAnimator = ObjectAnimator.OfObject (beatsLayout, "backgroundColor", new ArgbEvaluator(), _settings.FlashColor, ContextCompat.GetColor(Context, Resource.Color.background));
            _backgroundColorAnimator.SetAutoCancel(true);

            // GridView
            _gridView = view.FindViewById<GridView>(Resource.Id.grid_view);
            // Do it without MvvmCross, because there's is a problem with MvvmCross adapter and MvxGridView (the first item is not displayed)
            _gridView.Adapter = new BeatAdapter (Activity, _gridView, ViewModel.MeasureViewModel.Measure.BeatList);
            _gridView.SetOnTouchListener (this);

            // Measure fragment
            var measureFragment = new MeasureFragment();
            ChildFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.measure_frame, measureFragment)
                .Commit ();

            // Gesture detector
            _gestureDetector = new GestureDetector(Context, this);

            return view;
        }

        public override void OnStart ()
        {
            base.OnStart ();

            ViewModel.Metronome.BeatStarted += OnBeatStarted;
            ViewModel.Metronome.BeatFinished += OnBeatFinished;
        }

        public override void OnStop ()
        {
            base.OnStop ();

            ViewModel.Metronome.BeatStarted -= OnBeatStarted;
            ViewModel.Metronome.BeatFinished -= OnBeatFinished;
        }

        public void OnClick (View view)
        {
            StartStopMetronome ();
        }

        public bool OnDown (MotionEvent e)
        {
            return false;
        }

        public bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            const int SwipeThreshold = 50;
            const int SwipeVelocityThreshold = 50;

            var diffY = e2.GetY() - e1.GetY();
            var diffX = e2.GetX() - e1.GetX();
            if (Math.Abs(diffX) > Math.Abs(diffY)) 
            {
                if (Math.Abs(diffX) > SwipeThreshold && Math.Abs(velocityX) > SwipeVelocityThreshold) 
                {
                    if (diffX > 0) 
                    {
                        OnSwipeRight(e1);
                    }
                    else 
                    {
                        OnSwipeLeft(e1);
                    }
                }
            } 
            else if (Math.Abs(diffY) > SwipeThreshold && Math.Abs(velocityY) > SwipeVelocityThreshold) 
            {
                if (diffY > 0) 
                {
                    OnSwipeBottom(e1);
                }
                else 
                {
                    OnSwipeTop(e1);
                }
            }

            return true;
        }

        public void OnSwipeRight(MotionEvent e) 
        {
        }

        public void OnSwipeLeft(MotionEvent e) 
        {
            var position = _gridView.PointToPosition ((int)e.GetX (), (int)e.GetY ());
            if(position > -1 && position < ViewModel.MeasureViewModel.Measure.BeatList.Count)
            {
                var beat = ViewModel.MeasureViewModel.Measure.BeatList [position];
                beat.Status = BeatStatus.Normal;
            }
            ((BeatAdapter)_gridView.Adapter).NotifyDataSetChanged ();
        }

        public void OnSwipeTop(MotionEvent e) 
        {
            var position = _gridView.PointToPosition ((int)e.GetX (), (int)e.GetY ());
            if(position > -1 && position < ViewModel.MeasureViewModel.Measure.BeatList.Count)
            {
                var beat = ViewModel.MeasureViewModel.Measure.BeatList [position];
                beat.Status = beat.Status == BeatStatus.Accented ? BeatStatus.Normal : BeatStatus.Accented;
            }
            ((BeatAdapter)_gridView.Adapter).NotifyDataSetChanged ();
        }

        public void OnSwipeBottom(MotionEvent e) 
        {
            var position = _gridView.PointToPosition ((int)e.GetX (), (int)e.GetY ());
            if(position > -1 && position < ViewModel.MeasureViewModel.Measure.BeatList.Count)
            {
                var beat = ViewModel.MeasureViewModel.Measure.BeatList [position];
                beat.Status = beat.Status == BeatStatus.Mutated ? BeatStatus.Normal : BeatStatus.Mutated;
            }
            ((BeatAdapter)_gridView.Adapter).NotifyDataSetChanged ();
        }

        public void OnLongPress (MotionEvent e)
        {
        }

        public bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return false;
        }

        public void OnShowPress (MotionEvent e)
        {
        }

        public bool OnSingleTapUp (MotionEvent e)
        {
            StartStopMetronome ();

            return true;
        }

        public bool OnTouch (View view, MotionEvent motionEvent)
        {
            _gestureDetector.OnTouchEvent (motionEvent);

            return false;
        }

        public override void OnPrepareOptionsMenu (IMenu menu)
        {

            base.OnPrepareOptionsMenu(menu);
            menu.FindItem (Resource.Id.menu_settings).SetVisible(true);
            ((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled (false);
            ((MainActivity)Activity).SupportActionBar.Title = GetString(Resource.String.app_name);
        }

        private void OnBeatStarted(object sender, Beat beat)
        {
            Activity.RunOnUiThread(() =>
            {
                // Beat
                var beatView = _gridView.GetChildAt(beat.Number - 1);
                if (beatView != null)
                {
                    beatView.Alpha = 1;
                }

                // Flash
                if (_settings.Flash)
                {
                    _backgroundColorAnimator.SetObjectValues(_settings.FlashColor, ContextCompat.GetColor(Context, Resource.Color.background));
                    _backgroundColorAnimator.SetDuration((long)(beat.Duration * 1000));
                    _backgroundColorAnimator.Start();
                }
            });
        }

        private void OnBeatFinished(object sender, Beat beat)
        {
            Activity.RunOnUiThread(() =>
            {
                // Beat
                var beatView = _gridView.GetChildAt(beat.Number - 1);
                if (beatView != null)
                {
                    beatView.Alpha = 0.5f;
                }
            });
        }

        private void StartStopMetronome()
        {
            ViewModel.StartStopCommand.Execute();
        }
    }
}