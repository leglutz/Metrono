/*
 * Thanks to Cheesebaron and JakeWharton
 * https://github.com/Cheesebaron/ViewPagerIndicator/
 * https://github.com/JakeWharton/ViewPagerIndicator
 * https://developer.xamarin.com/samples/monodroid/ViewPagerIndicator/
 */

using Android.Support.V4.View;

namespace DiodeCompany.Metrono.Droid.Controls.ViewPagerIndicator
{
    public interface IPageIndicator : ViewPager.IOnPageChangeListener
    {
        void SetViewPager(ViewPager view);
        void SetViewPager(ViewPager view, int initialPosition);
        int CurrentItem { get; set; }
        void SetOnPageChangeListener(ViewPager.IOnPageChangeListener listener);
        void NotifyDataSetChanged();
    }
}

