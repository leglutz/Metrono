﻿using System;
using System.Threading.Tasks;
using Android.Media;
using DiodeTeam.Metroid.Core.Services;

namespace DiodeTeam.Metroid.Droid.Services
{
    /// <summary>
    /// Audio service.
    /// Thanks Ryan
    /// https://code.google.com/p/music-practice-tools/
    /// </summary>
    public class AudioService : IAudioService
    {
        private readonly AudioTrack _audioTrack;

        public int SamplingRate
        {
            get { return 44100; }
        }

        public bool IsPlaying
        {
            get { return _audioTrack.PlayState == PlayState.Playing; }
        }

        public AudioService ()
        {
            var minPlayBuffSizeInBytes = AudioTrack.GetMinBufferSize (SamplingRate, ChannelOut.Mono, Encoding.Pcm16bit);
            _audioTrack = new AudioTrack (Stream.Music, SamplingRate, ChannelOut.Mono, Encoding.Pcm16bit, minPlayBuffSizeInBytes, AudioTrackMode.Stream);
        }

        public void StartPlaying ()
        {
            _audioTrack.Play ();
        }

        public void StopPlaying ()
        {
            _audioTrack.Stop ();
        }

        public async Task PlayAsync (byte[] sound)
        {
            await _audioTrack.WriteAsync (sound, 0, sound.Length).ConfigureAwait (false);
        }
    }
}
    