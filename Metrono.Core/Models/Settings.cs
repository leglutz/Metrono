using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metrono.Core.Resources;

namespace DiodeCompany.Metrono.Core.Models
{
    public class Settings : MvxNotifyPropertyChanged
    {
        private readonly ISettings _settings;

        public bool FirstLaunch
        {
            get { return _settings.GetValue<bool>("FirstLaunch", true); }
            set 
            { 
                _settings.AddOrUpdateValue<bool> ("FirstLaunch", value); 
                RaisePropertyChanged (() => FirstLaunch);
            }
        }

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

        public bool PlayClick
        {
            get { return _settings.GetValue<bool>("PlayClick", true); }
            set 
            {
                _settings.AddOrUpdateValue<bool> ("PlayClick", value); 
                RaisePropertyChanged (() => PlayClick);
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

        public bool PlayFirstBeat
        {
            get { return _settings.GetValue<bool>("PlayFirstBeat", true); }
            set 
            {
                _settings.AddOrUpdateValue<bool> ("PlayFirstBeat", value); 
                RaisePropertyChanged (() => PlayFirstBeat);
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

        public bool PlayLastBeat
        {
            get { return _settings.GetValue<bool>("PlayLastBeat", false); }
            set 
            {
                _settings.AddOrUpdateValue<bool> ("PlayLastBeat", value); 
                RaisePropertyChanged (() => PlayLastBeat);
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

        public bool PlayAccentedBeats
        {
            get { return _settings.GetValue<bool>("PlayAccentedBeats", true); }
            set 
            {
                _settings.AddOrUpdateValue<bool> ("PlayAccentedBeats", value); 
                RaisePropertyChanged (() => PlayAccentedBeats);
            }
        }

        public ClickKind AccentedBeatClick
        {
            get { return _settings.GetValue<ClickKind>("AccentedBeatClick", ClickKind.Bell); }
            set 
            {
                _settings.AddOrUpdateValue<ClickKind> ("AccentedBeatClick", value); 
                RaisePropertyChanged (() => AccentedBeatClick);
            }
        }

        public Settings(ISettings settings)
        {
            _settings = settings;
        }
    }
}

