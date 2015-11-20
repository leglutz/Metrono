﻿using System.Collections.Generic;
using System.Linq;
using DiodeTeam.Metroid.Core.Models;
using DiodeTeam.Metroid.Core.Resources;
using DiodeTeam.Metroid.Core.Services;

namespace DiodeTeam.Metroid.Core.ViewModels
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

