using System.Threading.Tasks;
using Android.Media;
using DiodeCompany.Metrono.Core.Services;

namespace DiodeCompany.Metrono.Droid.Services
{
    /// <summary>
    /// Audio service.
    /// Thanks Ryan
    /// https://code.google.com/p/music-practice-tools/
    /// </summary>
    public class AudioService : IAudioService
    {
        private AudioTrack _audioTrack;

        public int SamplingRate
        {
            get { return 44100; }
        }

        public int MinBufferSize { get; private set;}

        public bool IsPlaying
        {
            get { return _audioTrack.PlayState == PlayState.Playing; }
        }

        public AudioService ()
        {
            MinBufferSize = AudioTrack.GetMinBufferSize (SamplingRate, ChannelOut.Mono, Encoding.Pcm16bit);
        }

        public void StartPlaying ()
        {
            _audioTrack = new AudioTrack (Stream.Music, SamplingRate, ChannelOut.Mono, Encoding.Pcm16bit, 2*MinBufferSize, AudioTrackMode.Stream);
            _audioTrack.Play ();
        }

        public void StopPlaying ()
        {
            _audioTrack.Stop ();
            _audioTrack.Release ();
        }

        public async Task PlayAsync (byte[] sound)
        {
            await _audioTrack.WriteAsync (sound, 0, sound.Length).ConfigureAwait(false);
        }
    }
}
    