using Android.Support.V4.App;
using DiodeCompany.Metrono.Droid.Views.Fragments;

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
    }
}

