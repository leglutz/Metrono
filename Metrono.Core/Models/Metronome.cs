using System;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metroid.Core.Resources;
using DiodeCompany.Metroid.Core.Services;
using MvvmCross.Plugins.Messenger;
using DiodeCompany.Metroid.Core.Messages;

namespace DiodeCompany.Metroid.Core.Models
{
    public class Metronome : MvxNotifyPropertyChanged
    {
        private readonly IAudioService _audioService;
        private readonly Settings _settings;
        private readonly IMvxMessenger _messenger;

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

        public Metronome (IAudioService audioService, ISettingsService settingsService, IMvxMessenger messenger)
        {
            _audioService = audioService;
            _settings = settingsService.Settings;
            _messenger = messenger;

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

            _messenger.Publish<MetronomeMessage> (new MetronomeMessage (this, MetronomeEvent.MeasureStarted, measure));   

            while (IsPlaying && currentBeatIndex < measure.BeatList.Count)
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
                await PauseAysnc ().ConfigureAwait (false);
            }

            measure.IsPlaying = false;

            _messenger.Publish<MetronomeMessage> (new MetronomeMessage (this, MetronomeEvent.MeasureFinished, measure));   
        }

        private async Task PlayBeatAsync (Beat beat)
        {
            beat.IsPlaying = true;
            // Play the beat first (sound takes more time than light)
            var beatSound = GetBeatSound (beat);
            await _audioService.PlayAsync (beatSound).ConfigureAwait (false);

            _messenger.Publish<MetronomeMessage> (new MetronomeMessage (this, MetronomeEvent.BeatStarted, beat: beat));   

            // Divided by two due to the ratio 16 bit PCM / wav
            var samplesElapsed = beatSound.Length / 2;
            var samplesPerBeat = GetSamplesPerBeat (beat);

            while (IsPlaying && samplesElapsed < samplesPerBeat)
            {
                var samplesLeft = samplesPerBeat - samplesElapsed;

                // Rest for a full write chunk or until the next click needs to play, whichever is less.
                var numberOfEmptyChunk = Math.Min (samplesLeft, _audioService.MinBufferSize);
                await _audioService.PlayAsync (new byte[2 * numberOfEmptyChunk]).ConfigureAwait (false);

                samplesElapsed += numberOfEmptyChunk;

                // In case the tempo changed
                samplesPerBeat = GetSamplesPerBeat (beat);

                // Check if is paused and pause if needed
                await PauseAysnc ().ConfigureAwait (false);
            }

            beat.IsPlaying = false;

            _messenger.Publish<MetronomeMessage> (new MetronomeMessage (this, MetronomeEvent.BeatFinished, beat: beat));   
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
    }
}

