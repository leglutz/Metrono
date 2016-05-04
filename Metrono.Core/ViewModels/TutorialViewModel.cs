using MvvmCross.Core.ViewModels;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public class TutorialViewModel : ViewModelBase
    {
        public IMvxCommand OkCommand { get; private set; }

        public TutorialViewModel ()
        {
            OkCommand = new MvxCommand (() => Close(this));
        }
    }
}

