using DiodeCompany.Metrono.Core.Resources;
using DiodeCompany.Metrono.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System;
using System.Threading.Tasks;

namespace DiodeCompany.Metrono.Core.Models
{
    public class Metronome : MvxNotifyPropertyChanged
    {
        public event EventHandler<Beat> BeatStarted;
        public event EventHandler<Beat> BeatFinished;

        private readonly IAudioService _audioService;
        private readonly Settings _settings;
        private readonly IMvxMessenger _messenger;

        private readonly byte[] _defaultEmptyChunksArray;

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

        public Metronome ()
        {
            _audioService = Mvx.Resolve<IAudioService>();
            _settings = Mvx.Resolve<ISettingsService>().Settings;
            _messenger = Mvx.Resolve<IMvxMessenger>();

            _defaultEmptyChunksArray = new byte[_audioService.MinBufferSize];

            IsPlaying = false;
            IsPaused = false;
        }

        public void Play (Measure measure, bool loop = false)
        {
            if (IsPaused || !IsPlaying)
            {
                // Run the GC to clean everything
                GC.Collect ();
                
                _audioService.StartPlaying ();

                IsPlaying = true;
                IsPaused = false;

                Task.Run (async () => await PlayMeasureAsync (measure, loop).ConfigureAwait(false))
                    .ContinueWith (x => Stop ()).ConfigureAwait(false);
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
                // Run the GC to clean everything
                GC.Collect ();

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

            while (IsPlaying && currentBeatIndex < measure.BeatList.Count)
            {
                // Play the current beat
                var currentBeat = measure.BeatList [currentBeatIndex];
                await PlayBeatAsync (measure, currentBeat).ConfigureAwait(false);

                currentBeatIndex++;
                if (currentBeatIndex >= measure.BeatList.Count && loop)
                {
                    // End of the measure, loop from start
                    currentBeatIndex = 0;
                }

                // Check if is paused and pause if needed
                if (_isPaused)
                {
                    await PauseAysnc ().ConfigureAwait(false);
                }
            }

            measure.IsPlaying = false;
        }

        private async Task PlayBeatAsync (Measure measure, Beat beat)
        {
            beat.IsPlaying = true;

            // Play the beat first (sound takes more time than light)
            var beatSound = GetBeatSound (measure, beat);
            await _audioService.PlayAsync (beatSound, beatSound.Length).ConfigureAwait(false);

            // Raise BeatStarted event
            BeatStarted?.BeginInvoke(this, beat, null, null);

            // Divided by two due to the ratio 16 bit PCM / wav
            var samplesElapsed = beatSound.Length / 2;
            var samplesPerBeat = GetSamplesPerBeat (beat);
            while (IsPlaying && samplesElapsed < samplesPerBeat)
            {
                var samplesLeft = samplesPerBeat - samplesElapsed;

                // Rest for a full write chunk or until the next click needs to play, whichever is less.
                var emptyChunksSamples = _defaultEmptyChunksArray.Length;
                if (samplesLeft < _defaultEmptyChunksArray.Length)
                {
                    // Multiplied by two due to the ratio wav / 16 bit PCM
                    emptyChunksSamples = 2 * samplesLeft;
                }
                await _audioService.PlayAsync (_defaultEmptyChunksArray, emptyChunksSamples).ConfigureAwait(false);

                // Elasped samples
                samplesElapsed += emptyChunksSamples / 2;

                // In case the tempo changed
                samplesPerBeat = GetSamplesPerBeat (beat);

                // Check if is paused and pause if needed
                if (_isPaused)
                {
                    await PauseAysnc ().ConfigureAwait(false);
                }
            }

            beat.IsPlaying = false;

            // Raise BeatFinished event
            BeatFinished?.BeginInvoke(this, beat, null, null);
        }

        private byte[] GetBeatSound(Measure measure, Beat beat)
        {
            if (beat.Status == BeatStatus.Mutated)
            {
                return _defaultEmptyChunksArray;
            }

            if (beat.Status == BeatStatus.Accented && _settings.PlayAccentedBeats)
            {
                return ResourcesHelper.ClickSoundMap [_settings.AccentedBeatClick];
            }

            if (beat.Number == 1 && _settings.PlayFirstBeat)
            {
                return ResourcesHelper.ClickSoundMap [_settings.FirstBeatClick];
            }

            if (beat.Number == measure.BeatList.Count && _settings.PlayLastBeat)
            {
                return ResourcesHelper.ClickSoundMap [_settings.LastBeatClick];
            }

            if (_settings.PlayClick)
            { 
                return ResourcesHelper.ClickSoundMap [_settings.BeatClick];
            }

            return _defaultEmptyChunksArray;
        }

        private async Task PauseAysnc ()
        {
            while (IsPaused)
            {
                await Task.Delay (50).ConfigureAwait(false);
            }
        }

        private int GetSamplesPerBeat (Beat beat)
        {
            return beat.Duration < int.MaxValue ? (int)(_audioService.SamplingRate * beat.Duration) : int.MaxValue;
        }
    }
}

