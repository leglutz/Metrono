using Cirrious.CrossCore;
using DiodeCompany.Metroid.Core.Models;

namespace DiodeCompany.Metroid.Core.Services
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

