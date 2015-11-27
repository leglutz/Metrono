using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;

namespace DiodeCompany.Metroid.Core.Models
{
    public class Measure : MvxNotifyPropertyChanged
    {
        private readonly Queue<int> _tapTempoQueue;
        private DateTime _lastTap;

        public int MinTempo
        {
            get { return 0; }
        }

        public int MaxTempo
        {
            get { return 300; }
        }

        private int _number;
        public int Number
        { 
            get { return _number; }
            set { SetProperty (ref _number, value); }
        }

        private int _tempo;
        public int Tempo
        { 
            get { return _tempo; }
            set
            {
                if (value >= MinTempo && value <= MaxTempo)
                {
                    if (SetProperty (ref _tempo, value))
                    {
                        UpdateBeatsTempo ();
                    }
                }
            }
        }

        private int _timeSignatureNumerator;
        public int TimeSignatureNumerator
        { 
            get { return _timeSignatureNumerator; }
            set
            {
                if (SetProperty (ref _timeSignatureNumerator, value))
                {
                    UpdateBeatList ();
                }
            }
        }

        private int _timeSignatureDenominator;
        public int TimeSignatureDenominator
        { 
            get { return _timeSignatureDenominator; }
            set
            {
                if (SetProperty (ref _timeSignatureDenominator, value))
                {
                    UpdateBeatList ();
                }
            }
        }

        private ObservableCollection<Beat> _beatList;
        public ObservableCollection<Beat> BeatList
        { 
            get { return _beatList; }
            set { SetProperty (ref _beatList, value); }
        }

        private bool _isPlaying;
        public bool IsPlaying
        { 
            get { return _isPlaying; }
            set { SetProperty (ref _isPlaying, value); }
        }

        public Measure (int tempo = 120, int signatureNominator = 4, int signatureDenominator = 4, int number = 1, int repetitionNumber = 1)
        {
            _tapTempoQueue = new Queue<int> ();
            _lastTap = DateTime.Now;

            _number = number;
            _tempo = tempo;
            _timeSignatureNumerator = signatureNominator;
            _timeSignatureDenominator = signatureDenominator;
            _beatList = new ObservableCollection<Beat> ();
            _isPlaying = false;

            UpdateBeatList ();
        }

        public void TapTempo ()
        {
            // Reset the tap
            if (DateTime.Now - _lastTap > TimeSpan.FromSeconds (5))
            {
                _tapTempoQueue.Clear ();
                _lastTap = DateTime.Now;
            }
            else
            {
                var elapsedTime = DateTime.Now - _lastTap;
                _lastTap = DateTime.Now;

                if (_tapTempoQueue.Count >= 5)
                {
                    _tapTempoQueue.Dequeue ();
                }

                _tapTempoQueue.Enqueue (ConvertMillisecondsToTempo (elapsedTime.TotalMilliseconds));

                var tempoAverage = (int)_tapTempoQueue.Average ();
                Tempo = tempoAverage > MaxTempo ? MaxTempo : tempoAverage;
            }
        }

        private void UpdateBeatsTempo ()
        {
            foreach (var beat in BeatList)
            {
                beat.Tempo = Tempo;
            }
        }

        private void UpdateBeatList ()
        {
            BeatList.Clear ();
            for (int i = 1; i <= TimeSignatureNumerator; i++)
            {
                var beat = new Beat (i, TimeSignatureDenominator, Tempo);
                beat.IsFirst = i == 1;
                beat.IsLast = i == TimeSignatureNumerator;
                // Compound meters
                if(TimeSignatureNumerator > 4 && TimeSignatureNumerator % 3 == 0)
                {
                    beat.IsCompound = (i-1) % 3 == 0;
                }
                BeatList.Add (beat);
            }
        }

        private int ConvertMillisecondsToTempo (double milliseconds)
        {
            return (int)((60.0 / milliseconds) * 1000);
        }
    }
}

