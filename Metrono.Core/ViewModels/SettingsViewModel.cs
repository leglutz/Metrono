using System.Collections.Generic;
using System.Linq;
using DiodeCompany.Metroid.Core.Models;
using DiodeCompany.Metroid.Core.Resources;
using DiodeCompany.Metroid.Core.Services;

namespace DiodeCompany.Metroid.Core.ViewModels
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

