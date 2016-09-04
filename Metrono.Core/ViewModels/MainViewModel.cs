using MvvmCross.Core.ViewModels;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IMvxCommand TutorialCommand { get; private set; }

        public MainViewModel ()
        {
            TutorialCommand = new MvxCommand (() => ShowViewModel<TutorialViewModel>());
        }
    }
}

