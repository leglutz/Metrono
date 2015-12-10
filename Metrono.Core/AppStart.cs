using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metrono.Core.ViewModels;

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
        }
    }
}

