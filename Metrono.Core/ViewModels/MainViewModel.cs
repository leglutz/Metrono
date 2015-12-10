using Cirrious.CrossCore;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MetronomeViewModel MetronomeViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }

        public MainViewModel ()
        {
            MetronomeViewModel = Mvx.IocConstruct<MetronomeViewModel> ();
            SettingsViewModel = Mvx.IocConstruct<SettingsViewModel> ();
        }
    }
}

