using DiodeCompany.Metrono.Core.Models;
using DiodeCompany.Metrono.Core.Resources;
using DiodeCompany.Metrono.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public Settings Settings { get; private set;}
        public List<ClickKind> ClickKindList { get; private set; }

        public SettingsViewModel (ISettingsService settingsService)
        {
            Settings = settingsService.Settings;
            ClickKindList = ResourcesHelper.ClickSoundMap.Keys.ToList ();
        }
    }
}

