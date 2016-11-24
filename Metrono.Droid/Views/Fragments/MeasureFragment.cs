using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DiodeCompany.Metrono.Core.Services;
using DiodeCompany.Metrono.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;

namespace DiodeCompany.Metrono.Droid.Views.Fragments
{
    [Register("diodecompany.metrono.droid.views.fragments.MeasureFragment")]
    public class MeasureFragment : MvxFragment<MeasureViewModel>
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel = Mvx.Resolve<MeasureViewModel>();

            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_measure, null);

            var settings = Mvx.Resolve<ISettingsService>().Settings;

            // Set the max value of the seek bar manually due to a binding problem with MvvmCross and Java
            // http://stackoverflow.com/questions/28030458/mvvmcross-binding-android-eventhandler
            // https://github.com/MvvmCross/MvvmCross/issues/884
            var seekBar = view.FindViewById<SeekBar> (Resource.Id.seek_bar);
            seekBar.Max = ViewModel.Measure.MaxTempo;
            // Also set the progress once, due to an initialization problem
            seekBar.Progress = settings.LastMeasure.Tempo;

            return view;
        }
    }
}
