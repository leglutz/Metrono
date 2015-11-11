using System;
using DiodeTeam.Metroid.Core.Resources;
using Cheesebaron.MvxPlugins.Settings.Interfaces;

namespace DiodeTeam.Metroid.Core.Models
{
    public class Settings
    {
        private readonly ISettings _settings;

        public Settings(ISettings settings)
        {
            _settings = settings;
        }

        public bool IsBlinking
        {
            get { return _settings.GetValue<bool>("IsBlinking", true); }
            set { _settings.AddOrUpdateValue<bool>("IsBlinking", value); }
        }

        public bool PlayFirstBeat
        {
            get { return _settings.GetValue<bool>("PlayFirstBeat", true); }
            set { _settings.AddOrUpdateValue<bool> ("PlayFirstBeat", value); }
        }

        public ClickKind FirstBeatClick
        {
            get { return _settings.GetValue<ClickKind>("FirstBeatClick", ClickKind.Bell);; }
            set { _settings.AddOrUpdateValue<ClickKind> ("FirstBeatClick", value); }
        }

        public bool PlayCompoundBeats
        {
            get { return _settings.GetValue<bool>("PlayCompoundBeats", true); }
            set { _settings.AddOrUpdateValue<bool> ("PlayCompoundBeats", value); }
        }

        public ClickKind CompoundBeatClick
        {
            get { return _settings.GetValue<ClickKind>("CompoundBeatClick", ClickKind.RimshotHi);; }
            set { _settings.AddOrUpdateValue<ClickKind> ("CompoundBeatClick", value); }
        }

        public bool PlayOtherBeats
        {
            get { return _settings.GetValue<bool>("PlayOtherBeats", true); }
            set { _settings.AddOrUpdateValue<bool> ("PlayOtherBeats", value); }
        }

        public ClickKind OtherBeatClick
        {
            get { return _settings.GetValue<ClickKind>("OtherBeatsClick", ClickKind.TickLo);; }
            set { _settings.AddOrUpdateValue<ClickKind> ("OtherBeatsClick", value); }
        }
    }
}

