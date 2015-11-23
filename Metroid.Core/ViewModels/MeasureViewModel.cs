using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.Models;
using DiodeTeam.Metroid.Core.Resources;
using DiodeTeam.Metroid.Core.Services;

namespace DiodeTeam.Metroid.Core.ViewModels
{
    public class MeasureViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;

        public Measure Measure { get; private set; }

        public List<int> TempoList { get; private set; }
        public List<int> TimeSignatureNumeratorList { get; private set; }
        public List<int> TimeSignatureDenominatorList { get; private set; }

        public IMvxCommand TempoPlus1Command { get; private set; }
        public IMvxCommand TempoMinus1Command { get; private set; }
        public IMvxCommand TapCommand { get; private set; }

        public MeasureViewModel (ISettingsService settingsService)
        {
            _settingsService = settingsService;

            Measure = new Measure ();
           
            TempoList = new List<int> (Enumerable.Range (Measure.MinTempo, Measure.MaxTempo + 1));
            TimeSignatureNumeratorList = new List<int> (Enumerable.Range (1, 20));
            TimeSignatureDenominatorList = new List<int> (ResourcesHelper.NoteImageSourceMap.Keys);

            TempoPlus1Command = new MvxCommand (() => Measure.Tempo += 1);
            TempoMinus1Command = new MvxCommand (() => Measure.Tempo -= 1);
            TapCommand = new MvxCommand (() => Measure.TapTempo ());
        }

        protected override void Show ()
        {
            Measure.Tempo = _settingsService.Settings.LastTempo;
            Measure.TimeSignatureNumerator = _settingsService.Settings.LastTimeSignatureNumerator;
            Measure.TimeSignatureDenominator = _settingsService.Settings.LastTimeSignatureDenominator;
        }

        protected override void Hide ()
        {
            _settingsService.Settings.LastTempo = Measure.Tempo;
            _settingsService.Settings.LastTimeSignatureNumerator = Measure.TimeSignatureNumerator;
            _settingsService.Settings.LastTimeSignatureDenominator = Measure.TimeSignatureDenominator;
        }
    }
}

