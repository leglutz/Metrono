using Android.Content;
using DiodeCompany.Metrono.Core.Services;
using DiodeCompany.Metrono.Droid.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using System.Collections.Generic;

namespace DiodeCompany.Metrono.Droid
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
                toReturn["Controls"] = "DiodeCompany.Metrono.Droid.Controls";
                return toReturn;
            }
        }
    }
}