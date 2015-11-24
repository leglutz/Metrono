using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeCompany.Metroid.Core.ViewModels;

namespace DiodeCompany.Metroid.Droid.Views.Fragments
{
    public class MeasureFragment : MvxFragment<MeasureViewModel>
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            return this.BindingInflate (Resource.Layout.fragment_measure, null);
        }
    }
}
