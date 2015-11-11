using System.Collections.Generic;
using System.Linq;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.Models;
using DiodeTeam.Metroid.Core.Resources;

namespace DiodeTeam.Metroid.Core.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        public Settings Settings { get; private set;}

        public List<ClickKind> ClickKindList { get; private set; }

        public SettingsViewModel ()
        {
            Settings = Mvx.IocConstruct<Settings> ();

            ClickKindList = ResourcesHelper.ClickSoundMap.Keys.ToList ();
        }
    }
}

