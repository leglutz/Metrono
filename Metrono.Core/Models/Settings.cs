using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metroid.Core.Resources;

namespace DiodeCompany.Metroid.Core.Models
{
    public class Settings : MvxNotifyPropertyChanged
    {
        private readonly ISettings _settings;

        public int LastTempo
        {
            get { return _settings.GetValue<int>("LastTempo", 120); }
            set 
            { 
                _settings.AddOrUpdateValue<int> ("LastTempo", value); 
                RaisePropertyChanged (() => LastTempo);
            }
        }

        public int LastTimeSignatureNumerator
        {
            get { return _settings.GetValue<int>("LastTimeSignatureNumerator", 4); }
            set 
            { 
                _settings.AddOrUpdateValue<int> ("LastTimeSignatureNumerator", value); 
                RaisePropertyChanged (() => LastTimeSignatureNumerator);
            }
        }

        public int LastTimeSignatureDenominator
        {
            get { return _settings.GetValue<int>("LastTimeSignatureDenominator", 4); }
            set 
            { 
                _settings.AddOrUpdateValue<int> ("LastTimeSignatureDenominator", value); 
                RaisePropertyChanged (() => LastTimeSignatureDenominator);
            }
        }

        public ClickKind BeatClick
        {
            get { return _settings.GetValue<ClickKind>("BeatClick", ClickKind.BeepLo); }
            set 
            { 
                _settings.AddOrUpdateValue<ClickKind> ("BeatClick", value); 
                RaisePropertyChanged (() => BeatClick);
            }
        }

        public bool Flash
        {
            get { return _settings.GetValue<bool>("Flash", true); }
            set 
            { 
                _settings.AddOrUpdateValue<bool>("Flash", value); 
                RaisePropertyChanged (() => Flash);
            }
        }

        public int FlashColor
        {
            get { return _settings.GetValue<int>("FlashColor", MvxColors.OrangeRed.ARGB); }
            set 
            { 
                _settings.AddOrUpdateValue<int>("FlashColor", value); 
                RaisePropertyChanged (() => FlashColor);
            }
        }

        public bool Vibration
        {
            get { return _settings.GetValue<bool>("Vibration", false); }
            set 
            { 
                _settings.AddOrUpdateValue<bool>("Vibration", value); 
                RaisePropertyChanged (() => Vibration);
            }
        }

        public bool AccentuateFirstBeat
        {
            get { return _settings.GetValue<bool>("AccentuateFirstBeat", true); }
            set 
            {
                _settings.AddOrUpdateValue<bool> ("AccentuateFirstBeat", value); 
                RaisePropertyChanged (() => AccentuateFirstBeat);
            }
        }

        public ClickKind FirstBeatClick
        {
            get { return _settings.GetValue<ClickKind>("FirstBeatClick", ClickKind.BeepHi); }
            set 
            {
                _settings.AddOrUpdateValue<ClickKind> ("FirstBeatClick", value); 
                RaisePropertyChanged (() => FirstBeatClick);
            }
        }

        public bool AccentuateLastBeat
        {
            get { return _settings.GetValue<bool>("AccentuateLastBeat", false); }
            set 
            {
                _settings.AddOrUpdateValue<bool> ("AccentuateLastBeat", value); 
                RaisePropertyChanged (() => AccentuateLastBeat);
            }
        }

        public ClickKind LastBeatClick
        {
            get { return _settings.GetValue<ClickKind>("LastBeatClick", ClickKind.Tambourine); }
            set 
            {
                _settings.AddOrUpdateValue<ClickKind> ("LastBeatClick", value); 
                RaisePropertyChanged (() => LastBeatClick);
            }
        }

        public bool AccentuateCompoundBeats
        {
            get { return _settings.GetValue<bool>("AccentuateCompoundBeats", true); }
            set 
            {
                _settings.AddOrUpdateValue<bool> ("AccentuateCompoundBeats", value); 
                RaisePropertyChanged (() => AccentuateCompoundBeats);
            }
        }

        public ClickKind CompoundBeatClick
        {
            get { return _settings.GetValue<ClickKind>("CompoundBeatClick", ClickKind.Bell); }
            set 
            {
                _settings.AddOrUpdateValue<ClickKind> ("CompoundBeatClick", value); 
                RaisePropertyChanged (() => CompoundBeatClick);
            }
        }

        public Settings(ISettings settings)
        {
            _settings = settings;
        }
    }
}

