using Cirrious.CrossCore;
using DiodeCompany.Metrono.Core.Models;

namespace DiodeCompany.Metrono.Core.Services
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

