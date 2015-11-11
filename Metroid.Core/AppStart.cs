using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.ViewModels;

namespace DiodeTeam.Metroid.Core
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
        }
    }
}

