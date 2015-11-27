using System;
using System.Collections.ObjectModel;
using Android.Animation;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore.Droid;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeCompany.Metroid.Core.Models;
using DiodeCompany.Metroid.Core.Services;
using DiodeCompany.Metroid.Core.ViewModels;
using DiodeCompany.Metroid.Droid.Views.Activities;

namespace DiodeCompany.Metroid.Droid.Views.Fragments
{
    public class MetronomeFragment : MvxFragment<MetronomeViewModel>, View.IOnTouchListener
    {
        private readonly Settings _settings;
        private readonly Vibrator _vibrator;

        private ObjectAnimator _backgroundColorAnimator;
        private GridView _gridView;

        public MetronomeFragment(ISettingsService settingsService, IMvxAndroidGlobals globals)
        {
            _settings = settingsService.Settings;
            _vibrator = (Vibrator)globals.ApplicationContext.GetSystemService(Android.Content.Context.VibratorService);
        }

        public override void OnViewModelSet ()
        {
            base.OnViewModelSet ();

            ViewModel.Metronome.BeatStarted += OnBeatStarted;
            ViewModel.Metronome.BeatFinished += OnBeatFinished;
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
            _gridView = view.FindViewById<GridView>(Resource.Id.grid_view);
            // Do it without MvvmCross, because there's is a problem with MvvmCross adapter and MvxGridView (the first item is not displayed)
            _gridView.Adapter = new BeatAdapter (Activity, _gridView, ViewModel.MeasureViewModel.Measure.BeatList);
            _gridView.SetOnTouchListener (this);

            // Measure fragment
            ChildFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.measure_frame, new MeasureFragment { ViewModel = ViewModel.MeasureViewModel })
                .Commit ();
            
            return view;
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

        private void OnBeatStarted (object sender, Beat beat)
        {
            // Beat
            Activity.RunOnUiThread (() => {
                var beatView = _gridView.GetChildAt (beat.Number - 1);
                if(beatView != null)
                {
                    ((CardView)beatView).Alpha = 1f;
                    ((CardView)beatView).CardElevation = 2;
                }
            });
        
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

        private void OnBeatFinished (object sender, Beat beat)
        {
            // Beat
            Activity.RunOnUiThread (() => {
                var beatView = _gridView.GetChildAt (beat.Number - 1);
                if(beatView != null)
                {
                    ((CardView)beatView).Alpha = 0.5f;
                    ((CardView)beatView).CardElevation = 0;
                }
            });
        }

        class BeatAdapter : BaseAdapter<Beat>
        {
            private readonly Context _context;
            private readonly GridView _gridView;
            private readonly ObservableCollection<Beat> _beatList;

            public override int Count
            {
                get { return _beatList.Count; }
            }

            public override Beat this[int position]
            {
                get{ return _beatList [position]; }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public BeatAdapter(Context context, GridView gridView, ObservableCollection<Beat> beatList)
            {
                _context = context;
                _gridView = gridView;
                _beatList = beatList;
                _beatList.CollectionChanged += (sender, e) => NotifyDataSetChanged ();
            }

            public override View GetView (int position, View convertView, ViewGroup parent)
            {
                var inflater = LayoutInflater.FromContext (_context);
                var view = inflater.Inflate (Resource.Layout.template_metronome_beat_item, parent, false);

                // Image resource
                var beatImageView = view.FindViewById<ImageView> (Resource.Id.beat_image_view);
                var imageResource = Resource.Drawable.quarter;
                switch (this [position].Value)
                {
                    case 1:
                        imageResource = Resource.Drawable.whole;
                        break;
                    case 2:
                        imageResource = Resource.Drawable.half;
                        break;
                    case 4:
                        imageResource = Resource.Drawable.quarter;
                        break;
                    case 8:
                        imageResource = Resource.Drawable.eighth;
                        break;
                    case 16:
                        imageResource = Resource.Drawable.sixteenth;
                        break;
                    case 32:
                        imageResource = Resource.Drawable.thirty_second;
                        break;
                }
                beatImageView.SetImageResource (imageResource);

                // Beat number
                var beatNumberTextView = view.FindViewById<TextView> (Resource.Id.beat_number_text_view);
                beatNumberTextView.Text = this [position].Number.ToString();

                // Layout size
                const double numberOfColumns = 5.0;
                const double numberOfRows = 4.0;

                var width = _gridView.Width - _gridView.PaddingLeft - _gridView.PaddingRight;
                var gridViewParent = (View)_gridView.Parent;
                var height = gridViewParent.Height - gridViewParent.PaddingTop - gridViewParent.PaddingBottom;

                var itemWidth = width / numberOfColumns - _gridView.HorizontalSpacing;
                var itemHeight = height / numberOfRows - _gridView.VerticalSpacing;

                var size = (int)Math.Min (itemWidth, itemHeight);

                view.LayoutParameters = new GridView.LayoutParams (size, size);

                // Initial values for card view
                ((CardView)view).Alpha = 0.5f;
                ((CardView)view).CardElevation = 0;

                return view;
            }
        }
    }
}