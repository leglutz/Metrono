using Android.Support.V4.App;
using DiodeCompany.Metrono.Droid.Views.Fragments;

namespace DiodeCompany.Metrono.Droid.Views.Adapters
{
    public class ShowcaseFragmentAdapter : FragmentPagerAdapter
    {
        private static readonly int[] _showcaseImageResourceArray =
        {
            Resource.Drawable.tutorial_measure,
            Resource.Drawable.tutorial_metronome
        };

        public override int Count
        {
            get { return _showcaseImageResourceArray.Length; }
        }

        public ShowcaseFragmentAdapter(FragmentManager fragmentManager) : base(fragmentManager) 
        {
        }

        public override Fragment GetItem(int position)
        {
            return ShowcaseFragment.NewInstance(_showcaseImageResourceArray[position % Count], position + 1 == Count);
        }
    }
}

