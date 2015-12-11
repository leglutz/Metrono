using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metrono.Core.Messages;
using DiodeCompany.Metrono.Core.Models;
using DiodeCompany.Metrono.Core.Services;
using MvvmCross.Plugins.Messenger;
using Plugin.Vibrate;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public class MetronomeViewModel : ViewModelBase
    {
        private readonly Settings _settings;
        private readonly MvxSubscriptionToken _metronomeMessageSubscriptionToken;

        public MeasureViewModel MeasureViewModel { get; private set; }
        public Metronome Metronome { get; set; }

        public IMvxCommand SettingsCommand { get; private set; }
        public IMvxCommand StartStopCommand { get; private set; }

        public MetronomeViewModel (ISettingsService settingsService, IMvxMessenger messenger)
        {
            _settings = settingsService.Settings;
            _metronomeMessageSubscriptionToken = messenger.SubscribeOnThreadPoolThread<MetronomeMessage> (OnMetronomeMessage);

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

        private void OnMetronomeMessage (MetronomeMessage metronomeMessage)
        {
            switch (metronomeMessage.MetronomeEvent)
            {
                case MetronomeEvent.BeatStarted:
                    {
                        // Vibration
                        if (_settings.Vibration)
                        {
                            CrossVibrate.Current.Vibration ((int)(metronomeMessage.Beat.Duration / 3.0 * 1000));
                        }
                    }
                    break;
            }
        }
    }
}

