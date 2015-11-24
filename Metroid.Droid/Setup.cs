using System.Collections.Generic;
using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metroid.Core.Services;
using DiodeCompany.Metroid.Droid.Services;

namespace DiodeCompany.Metroid.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
		
        protected override void InitializeLastChance ()
        {
            base.InitializeLastChance ();

            Mvx.RegisterSingleton<IAudioService>(new AudioService ());
        }

        protected override IDictionary<string, string> ViewNamespaceAbbreviations
        {
            get
            {
                var toReturn = base.ViewNamespaceAbbreviations;
                toReturn["Controls"] = "DiodeCompany.Metroid.Droid.Controls";
                return toReturn;
            }
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}