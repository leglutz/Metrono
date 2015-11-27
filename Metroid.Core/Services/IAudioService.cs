using System.Threading.Tasks;

namespace DiodeCompany.Metroid.Core.Services
{
    public interface IAudioService
    {
        int SamplingRate { get; }
        int MinBufferSize { get; }
        bool IsPlaying { get; }
        void StartPlaying ();
        void StopPlaying ();
        Task PlayAsync (byte[] sound);
    }
}

