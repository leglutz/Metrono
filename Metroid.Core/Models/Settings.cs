using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.Resources;

namespace DiodeTeam.Metroid.Core.Models
{
    public class Settings : MvxNotifyPropertyChanged
    {
        private readonly ISettings _settings;

        public bool Blink
        {
            get { return _settings.GetValue<bool>("Blink", true); }
            set 
            { 
                _settings.AddOrUpdateValue<bool>("Blink", value); 
                RaisePropertyChanged (() => Blink);
            }
        }

        public int BlinkColor
        {
            get { return _settings.GetValue<int>("BlinkColor", -9681153); }
            set 
            { 
                _settings.AddOrUpdateValue<int>("BlinkColor", value); 
                RaisePropertyChanged (() => BlinkColor);
            }
        }

        public ClickKind BeatClick
        {
            get { return _settings.GetValue<ClickKind>("BeatClick", ClickKind.TickLo);; }
            set 
            { 
                _settings.AddOrUpdateValue<ClickKind> ("BeatClick", value); 
                RaisePropertyChanged (() => BeatClick);
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
            get { return _settings.GetValue<ClickKind>("FirstBeatClick", ClickKind.Bell);; }
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
            get { return _settings.GetValue<ClickKind>("LastBeatClick", ClickKind.Bell);; }
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
            get { return _settings.GetValue<ClickKind>("CompoundBeatClick", ClickKind.RimshotHi);; }
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

