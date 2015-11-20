using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.Models;

namespace DiodeTeam.Metroid.Core.ViewModels
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

        protected override void Hide()
        {
            Metronome.Stop ();
        }
    }
}

