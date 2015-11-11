using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.Models;
using DiodeTeam.Metroid.Core.Services;

namespace DiodeTeam.Metroid.Core.ViewModels
{
    public class MetronomeViewModel : MvxViewModel
    {
        public MeasureViewModel MeasureViewModel { get; private set; }
        public Metronome Metronome { get; set; }

        public IMvxCommand SettingsCommand { get; private set; }
        public IMvxCommand StopCommand { get; private set; }
        public IMvxCommand PlayCommand { get; private set; }

        public MetronomeViewModel ()
        {
            MeasureViewModel = Mvx.IocConstruct<MeasureViewModel> ();
            Metronome = Mvx.IocConstruct<Metronome> ();

            SettingsCommand = new MvxCommand (() => ShowViewModel<SettingsViewModel> ());
            StopCommand = new MvxCommand (() => Stop ());
            PlayCommand = new MvxCommand (() => Play ());
        }

        private void Stop ()
        {
            if (Metronome.IsPlaying)
            {
                Metronome.Stop ();
            }
        }

        private void Play ()
        {
            if (!Metronome.IsPlaying)
            {
                Metronome.Play (MeasureViewModel.Measure, true);
            }
        }
    }
}

