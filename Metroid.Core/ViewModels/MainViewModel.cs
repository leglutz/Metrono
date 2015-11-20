using Cirrious.CrossCore;

namespace DiodeTeam.Metroid.Core.ViewModels
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

