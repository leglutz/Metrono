using DiodeCompany.Metrono.Core.Services;
using DiodeCompany.Metrono.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace DiodeCompany.Metrono.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        /// <summary>
        /// Start is called on startup of the app
        /// Hint contains information in case the app is started with extra parameters
        /// </summary>
        public void Start(object hint = null)
        {
            ShowViewModel<MainViewModel>();

            // If first launch, display the tutorial
            var settings = Mvx.Resolve<ISettingsService> ().Settings;
            if(settings.FirstLaunch)
            {
                ShowViewModel<TutorialViewModel> ();
                settings.FirstLaunch = false;
            }
        }
    }
}

