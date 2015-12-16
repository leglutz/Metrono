using System;
using System.Collections.ObjectModel;
using Android.Content;
using Android.Views;
using Android.Widget;
using DiodeCompany.Metrono.Core.Models;

namespace DiodeCompany.Metrono.Droid.Views.Adapters
{
    public class BeatAdapter : BaseAdapter<Beat>
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
            }
            beatImageView.SetImageResource (imageResource);

            // Beat number
            var beatNumberTextView = view.FindViewById<TextView> (Resource.Id.beat_number_text_view);
            beatNumberTextView.Text = this [position].Number.ToString();

            // Beat status
            var beatStatusImageView = view.FindViewById<ImageView> (Resource.Id.beat_status_image_view);
            switch(this[position].Status)
            {
                case BeatStatus.Accented:
                    beatStatusImageView.SetImageResource (Resource.Drawable.accent);
                    break;
                case BeatStatus.Mutated:
                    beatStatusImageView.SetImageResource (Resource.Drawable.mute);
                    break;
                case BeatStatus.Normal:
                    beatStatusImageView.SetImageResource (0);
                    break;
            }

            // Layout size
            const double numberOfColumns = 5.0;
            const double numberOfRows = 4.0;

            var width = _gridView.Width - _gridView.PaddingLeft - _gridView.PaddingRight;
            var gridViewParent = (View)_gridView.Parent;
            var height = gridViewParent.Height - gridViewParent.PaddingTop - gridViewParent.PaddingBottom;

            var itemWidth = width / numberOfColumns - _gridView.HorizontalSpacing;
            var itemHeight = height / numberOfRows - _gridView.VerticalSpacing;

            // Set the new size
            var size = (int)Math.Min (itemWidth, itemHeight);
            view.LayoutParameters = new GridView.LayoutParams (size, size);

            // Initial alpha values for card view
            view.Alpha = 0.5f;

            return view;
        }
    }
}

