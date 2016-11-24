using DiodeCompany.Metrono.Core.Messages;
using DiodeCompany.Metrono.Core.Models;
using DiodeCompany.Metrono.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public class MetronomeViewModel : ViewModelBase
    {
        private readonly Settings _settings;
        
        public MeasureViewModel MeasureViewModel { get; private set; }
        public Metronome Metronome { get; set; }

        public IMvxCommand SettingsCommand { get; private set; }
        public IMvxCommand StartStopCommand { get; private set; }

        public MetronomeViewModel (MeasureViewModel measureViewModel, ISettingsService settingsService, IMvxMessenger messenger)
        {
            _settings = settingsService.Settings;

            MeasureViewModel = measureViewModel;
            Metronome = new Metronome();

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

