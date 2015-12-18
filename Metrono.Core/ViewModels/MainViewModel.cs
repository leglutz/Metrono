using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MetronomeViewModel MetronomeViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }

        public IMvxCommand TutorialCommand { get; private set; }

        public MainViewModel ()
        {
            MetronomeViewModel = Mvx.IocConstruct<MetronomeViewModel> ();
            SettingsViewModel = Mvx.IocConstruct<SettingsViewModel> ();

            TutorialCommand = new MvxCommand (() => ShowViewModel<TutorialViewModel>());
        }
    }
}

