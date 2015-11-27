using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metroid.Core.Helpers;
using DiodeCompany.Metroid.Core.Models;

namespace DiodeCompany.Metroid.Core.ViewModels
{
    public class MetronomeViewModel : ViewModelBase
    {
        public MeasureViewModel MeasureViewModel { get; private set; }
        public Metronome Metronome { get; set; }

        public IMvxCommand SettingsCommand { get; private set; }
        public IMvxCommand StartStopCommand { get; private set; }

        public MetronomeViewModel ()
        {
            MeasureViewModel = Mvx.IocConstruct<MeasureViewModel> ();
            Metronome = Mvx.IocConstruct<Metronome> ();

            SettingsCommand = new MvxCommand (() => ShowViewModel<SettingsViewModel> ());
            StartStopCommand = new MvxCommand (DoStartStopCommand);
        }

        private void DoStartStopCommand ()
        {
            if(Metronome.IsPlaying)
            {
                Metronome.Stop();
            }
            else
            {
                Metronome.Play (MeasureViewModel.Measure, true);
            }    
        }

        protected override void OnLifeCycleMessage (LifeCycleMessage lifeCycleMessage)
        {
            switch(lifeCycleMessage.LifeCycleEvent)
            {
                case LifeCycleEvent.Stop:
                case LifeCycleEvent.Destroy:
                    Metronome.Stop ();
                    break;
            }
        }
    }
}

