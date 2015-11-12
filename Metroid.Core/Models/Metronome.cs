using System;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.Resources;
using DiodeTeam.Metroid.Core.Services;

namespace DiodeTeam.Metroid.Core.Models
{
    public class Metronome : MvxNotifyPropertyChanged
    {
        public event EventHandler<Measure> MeasureStarted;
        public event EventHandler<Measure> MeasureFinished;
        public event EventHandler<Beat> BeatStarted;
        public event EventHandler<Beat> BeatFinished;

        private readonly IAudioService _audioService;
        private readonly Settings _settings;

        private bool _isPlaying;
        public bool IsPlaying
        { 
            get { return _isPlaying; }
            private set { SetProperty (ref _isPlaying, value); }
        }

        private bool _isPaused;
        public bool IsPaused
        { 
            get { return _isPaused; }
            private set { SetProperty (ref _isPaused, value); }
        }

        public Metronome (IAudioService audioService, ISettingsService settingsService)
        {
            _audioService = audioService;
            _settings = settingsService.Settings;

            IsPlaying = false;
            IsPaused = false;
        }

        public void Play (Measure measure, bool loop = false)
        {
            if (IsPaused || !IsPlaying)
            {
                _audioService.StartPlaying ();

                IsPlaying = true;
                IsPaused = false;

                Task.Run (async () => await PlayMeasureAsync (measure, loop).ConfigureAwait (false)).ContinueWith (x => Stop ());
            }
        }

        public void Pause ()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                IsPaused = true;

                _audioService.StopPlaying ();
            }
        }

        public void Resume ()
        {
            if (IsPaused)
            {
                _audioService.StartPlaying ();

                IsPlaying = true;
                IsPaused = false;
            }
        }

        public void Stop ()
        {
            if (IsPlaying || IsPaused)
            {
                IsPlaying = false;
                IsPaused = false;

                _audioService.StopPlaying ();
            }
        }

        private async Task PlayMeasureAsync (Measure measure, bool loop = false)
        {
            var currentBeatIndex = 0;
            measure.IsPlaying = true;
            FireMeasureStarted (measure);

            do
            {
                // Play the current beat
                var currentBeat = measure.BeatList [currentBeatIndex];
                await PlayBeatAsync (currentBeat).ConfigureAwait (false);

                currentBeatIndex++;
                if (currentBeatIndex >= measure.BeatList.Count && loop)
                {
                    // End of the measure, loop from start
                    currentBeatIndex = 0;
                }

                // Check if is paused and pause if needed
                await PauseAysnc ();
            }
            while (IsPlaying && currentBeatIndex < measure.BeatList.Count);

            measure.IsPlaying = false;
            FireMeasureFinished (measure);
        }

        private async Task PlayBeatAsync (Beat beat)
        {
            beat.IsPlaying = true;
            FireBeatStarted (beat);

            var beatSound = GetBeatSound (beat);
            await _audioService.PlayAsync (beatSound).ConfigureAwait (false);

            // Divided by two due to the ratio 16 bit PCM / wav
            var samplesElapsed = beatSound.Length / 2;
            var samplesPerBeat = GetSamplesPerBeat (beat);

            while (IsPlaying && samplesElapsed < samplesPerBeat)
            {
                var samplesLeft = samplesPerBeat - samplesElapsed;

                // Rest for a full write chunk or until the next click needs to play, whichever is less.
                var emptyChunk = Math.Min (samplesLeft, _audioService.SamplingRate / 2);
                await _audioService.PlayAsync (Get16BitPcm (new double[emptyChunk])).ConfigureAwait (false);

                samplesElapsed += emptyChunk;

                // In case the tempo changed
                samplesPerBeat = GetSamplesPerBeat (beat);

                // Check if is paused and pause if needed
                await PauseAysnc ();
            }

            beat.IsPlaying = false;
            FireBeatFinished (beat);
        }

        private byte[] GetBeatSound(Beat beat)
        {
            var beatSound = new byte[0];
            if (beat.IsFirst && _settings.AccentuateFirstBeat)
            {
                beatSound = ResourcesHelper.ClickSoundMap [_settings.FirstBeatClick];
            }
            else if (beat.IsLast && _settings.AccentuateLastBeat)
            {
                beatSound = ResourcesHelper.ClickSoundMap [_settings.LastBeatClick];
            }
            else if(beat.IsCompound && _settings.AccentuateCompoundBeats)
            {
                beatSound = ResourcesHelper.ClickSoundMap [_settings.CompoundBeatClick];
            }
            else
            { 
                beatSound = ResourcesHelper.ClickSoundMap [_settings.BeatClick];
            }

            return beatSound;
        }

        private async Task PauseAysnc ()
        {
            while (IsPaused)
            {
                await Task.Delay (50).ConfigureAwait (false);
            }
        }

        private int GetSamplesPerBeat (Beat beat)
        {
            return beat.Duration < int.MaxValue ? (int)(_audioService.SamplingRate * beat.Duration) : int.MaxValue;
        }

        private byte[] Get16BitPcm (double[] samples)
        {
            byte[] generatedSound = new byte[2 * samples.Length];
            int index = 0;
            foreach (var sample in samples)
            {
                // Scale to maximum amplitude
                short maxSample = (short)((sample * short.MaxValue));
                // In 16 bit wav PCM, first byte is the low order byte
                generatedSound [index++] = (byte)(maxSample & 0x00ff);
                generatedSound [index++] = (byte)((uint)(maxSample & 0xff00) >> 8);

            }
            return generatedSound;
        }

        private void FireMeasureStarted (Measure measure)
        {
            var measureStarted = MeasureStarted;
            if (measureStarted != null)
            {
                measureStarted.BeginInvoke (this, measure, null, null);
            }
        }

        private void FireMeasureFinished (Measure measure)
        {
            var measureFinished = MeasureFinished;
            if (measureFinished != null)
            {
                measureFinished.BeginInvoke (this, measure, null, null);
            }
        }

        private void FireBeatStarted (Beat beat)
        {
            var beatStarted = BeatStarted;
            if (beatStarted != null)
            {
                beatStarted.BeginInvoke (this, beat, null, null);
            }
        }

        private void FireBeatFinished (Beat beat)
        {
            var beatFinished = BeatFinished;
            if (beatFinished != null)
            {
                beatFinished.BeginInvoke (this, beat, null, null);
            }
        }
    }
}

