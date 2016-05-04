using Cheesebaron.MvxPlugins.Settings.Interfaces;
using DiodeCompany.Metrono.Core.Resources;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.UI;

namespace DiodeCompany.Metrono.Core.Models
{
    public class Settings : MvxNotifyPropertyChanged
    {
        private readonly ISettings _settings;
        private readonly IMvxJsonConverter _jsonConverter;

        public bool FirstLaunch
        {
            get { return _settings.GetValue<bool>("FirstLaunch", true); }
            set 
            { 
                _settings.AddOrUpdateValue<bool> ("FirstLaunch", value); 
                RaisePropertyChanged (() => FirstLaunch);
            }
        }

        public Measure LastMeasure
        {
            get 
            {
                var jsonMeasure = _settings.GetValue<string>("LastMeasure", null); 
                if (!string.IsNullOrEmpty (jsonMeasure))
                {
                    return _jsonConverter.DeserializeObject<Measure> (jsonMeasure);
                }

                return new Measure ();
            }
            set 
            { 
                _settings.AddOrUpdateValue<string> ("LastMeasure", _jsonConverter.SerializeObject (value)); 
                RaisePropertyChanged (() => LastMeasure);
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

        public Settings()
        {
            _settings = Mvx.Resolve<ISettings> ();
            _jsonConverter = Mvx.Resolve<IMvxJsonConverter> ();
        }
    }
}

