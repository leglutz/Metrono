using System;
using System.Collections.ObjectModel;
using Android.Content;
using Android.Support.V7.Widget;
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

