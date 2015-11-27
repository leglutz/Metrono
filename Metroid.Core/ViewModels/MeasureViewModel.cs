using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metroid.Core.Helpers;
using DiodeCompany.Metroid.Core.Models;
using DiodeCompany.Metroid.Core.Resources;
using DiodeCompany.Metroid.Core.Services;

namespace DiodeCompany.Metroid.Core.ViewModels
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

            // Create a new measure
            Measure = new Measure (_settingsService.Settings.LastTempo, 
                                   _settingsService.Settings.LastTimeSignatureNumerator, 
                                   _settingsService.Settings.LastTimeSignatureDenominator);
           
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
                case LifeCycleEvent.Stop:
                case LifeCycleEvent.Destroy:
                    _settingsService.Settings.LastTempo = Measure.Tempo;
                    _settingsService.Settings.LastTimeSignatureNumerator = Measure.TimeSignatureNumerator;
                    _settingsService.Settings.LastTimeSignatureDenominator = Measure.TimeSignatureDenominator;
                    break;
            }
        }
    }
}

