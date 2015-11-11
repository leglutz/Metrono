using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.Services;
using DiodeTeam.Metroid.Droid.Services;

namespace DiodeTeam.Metroid.Droid
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

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}