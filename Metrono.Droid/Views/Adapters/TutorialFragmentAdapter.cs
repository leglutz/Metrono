using Android.Support.V4.App;
using DiodeCompany.Metrono.Droid.Controls.ViewPagerIndicator;
using DiodeCompany.Metrono.Droid.Views.Fragments;
using Android.Content.Res;

namespace DiodeCompany.Metrono.Droid.Views.Adapters
{
    public class TutorialFragmentAdapter : FragmentPagerAdapter
    {
        private static readonly int[] _imageResourceArray =
        {
            Resource.Drawable.tutorial_measure,
            Resource.Drawable.tutorial_metronome
        };

        public override int Count
        {
            get { return _imageResourceArray.Length; }
        }

        public TutorialFragmentAdapter(FragmentManager fragmentManager) : base(fragmentManager) 
        {
        }

        public override Fragment GetItem(int position)
        {
            return TutorialFragment.NewInstance(_imageResourceArray[position % Count], position + 1 == Count);
        }

        public string GetTitle (int position)
        {
            return Resource.String.tutorial + " /" + (position % Count) + 1;
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(Android.App.Application.Context.GetString(Resource.String.tutorial) + " " + ((position % Count) + 1) + "/" + Count);
        }
    }
}

