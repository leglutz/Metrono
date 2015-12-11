using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metrono.Core.Messages;
using DiodeCompany.Metrono.Core.Models;
using DiodeCompany.Metrono.Core.Resources;
using DiodeCompany.Metrono.Core.Services;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public class MeasureViewModel : ViewModelBase
    {
        private readonly Settings _settings;

        public Measure Measure { get; private set; }

        public List<int> TempoList { get; private set; }
        public List<int> TimeSignatureNumeratorList { get; private set; }
        public List<int> TimeSignatureDenominatorList { get; private set; }

        public IMvxCommand TempoPlus1Command { get; private set; }
        public IMvxCommand TempoMinus1Command { get; private set; }
        public IMvxCommand TapCommand { get; private set; }

        public MeasureViewModel (ISettingsService settingsService)
        {
            _settings = settingsService.Settings;

            Measure = new Measure ();
           
            TempoList = new List<int> (Enumerable.Range (Measure.MinTempo, Measure.MaxTempo + 1));
            TimeSignatureNumeratorList = new List<int> (Enumerable.Range (1, 20));
            TimeSignatureDenominatorList = new List<int> (ResourcesHelper.NoteImageSourceMap.Keys);

            TempoPlus1Command = new MvxCommand (() => Measure.Tempo += 1);
            TempoMinus1Command = new MvxCommand (() => Measure.Tempo -= 1);
            TapCommand = new MvxCommand (() => Measure.TapTempo ());
        }

        protected override void OnLifeCycleMessage (LifeCycleMessage lifeCycleMessage)
        {
            switch(lifeCycleMessage.LifeCycleEvent)
            {
                case LifeCycleEvent.Start:
                    // Restore values
                    Measure.TimeSignatureNumerator = _settings.LastTimeSignatureNumerator;
                    Measure.TimeSignatureDenominator = _settings.LastTimeSignatureDenominator;
                    Measure.Tempo = _settings.LastTempo;
                    break;
                case LifeCycleEvent.Stop:
                case LifeCycleEvent.Destroy:
                    // Store values
                    _settings.LastTempo = Measure.Tempo;
                    _settings.LastTimeSignatureNumerator = Measure.TimeSignatureNumerator;
                    _settings.LastTimeSignatureDenominator = Measure.TimeSignatureDenominator;
                    break;
            }
        }
    }
}

