using Cirrious.CrossCore;
using DiodeTeam.Metroid.Core.Models;

namespace DiodeTeam.Metroid.Core.Services
{
    public class SettingsService : ISettingsService
    {
        public Settings Settings{ get; private set; }

        public SettingsService ()
        {
            Settings = Mvx.IocConstruct<Settings>();
        }
    }
}

