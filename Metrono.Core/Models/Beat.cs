using System.ComponentModel;
using Cirrious.MvvmCross.ViewModels;

namespace DiodeCompany.Metrono.Core.Models
{
    public class Beat : MvxNotifyPropertyChanged
    {
        private int _number;
        public int Number
        { 
            get { return _number; }
            set
            {
                if (SetProperty (ref _number, value))
                {
                    UpdateDuration ();
                }
            }
        }

        private int _value;
        public int Value
        { 
            get { return _value; }
            set
            {
                if (SetProperty (ref _value, value))
                {
                    UpdateDuration ();
                }
            }
        }

        private int _tempo;
        public int Tempo
        { 
            get { return _tempo; }
            set
            {
                if (SetProperty (ref _tempo, value))
                {
                    UpdateDuration ();
                }
            }
        }

        private int _dots;
        public int Dots
        { 
            get { return _dots; }
            set
            {
                if (SetProperty (ref _dots, value))
                {
                    UpdateDuration ();
                }
            }
        }

        private int _tupletNumerator;
        public int TupletNumerator
        { 
            get { return _tupletNumerator; }
            set
            {
                if (SetProperty (ref _tupletNumerator, value))
                {
                    UpdateDuration ();
                }
            }
        }

        private int _tupletDenominator;
        public int TupletDenominator
        { 
            get { return _tupletDenominator; }
            set
            {
                if (SetProperty (ref _tupletDenominator, value))
                {
                    UpdateDuration ();
                }
            }
        }

        private bool _isFirst;
        public bool IsFirst
        { 
            get { return _isFirst; }
            set { SetProperty (ref _isFirst, value); }
        }

        private bool _isLast;
        public bool IsLast
        { 
            get { return _isLast; }
            set { SetProperty (ref _isLast, value); }
        }

        private bool _isCompound;
        public bool IsCompound
        { 
            get { return _isCompound; }
            set { SetProperty (ref _isCompound, value); }
        }

        private bool _isPlaying;
        public bool IsPlaying
        { 
            get { return _isPlaying; }
            set { SetProperty (ref _isPlaying, value); }
        }

        public double Duration { get; private set; }

        public Beat (int number = 1, int value = 4, int tempo = 120)
        {
            _number = number;
            _value = value;
            _tempo = tempo;
            _dots = 0;
            _tupletNumerator = 0;
            _tupletDenominator = 0;
            _isFirst = false;
            _isLast = false;
            _isCompound = false;
            _isPlaying = false;

            UpdateDuration ();
        }

        private void UpdateDuration ()
        {
            if (Tempo > 0)
            {
                var duration = (60.0 / Tempo) * (4.0 / Value);
                if (Dots == 2)
                {
                    duration += ((duration / 4.0) * 3.0);
                }
                else if (Dots == 1)
                {
                    duration += (duration / 2.0);
                }

                if (TupletDenominator > 0 && TupletNumerator >= 0)
                {
                    duration *= TupletNumerator / (double)TupletDenominator;
                }

                Duration = duration;
            }
            else
            {
                Duration = int.MaxValue;
            }
        }
    }
}
    