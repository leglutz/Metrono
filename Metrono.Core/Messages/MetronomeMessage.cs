using MvvmCross.Plugins.Messenger;
using DiodeCompany.Metrono.Core.Models;

namespace DiodeCompany.Metrono.Core.Messages
{
    public enum MetronomeEvent
    {
        MeasureStarted,
        MeasureFinished,
        BeatStarted,
        BeatFinished
    }

    public class MetronomeMessage : MvxMessage
    {
        public MetronomeEvent MetronomeEvent { get; private set;}
        public Measure Measure { get; private set; }
        public Beat Beat { get; private set; }

        public MetronomeMessage (object sender, MetronomeEvent metronomeEvent, Measure measure = null, Beat beat = null) : base(sender)
        {
            MetronomeEvent = metronomeEvent;
            Measure = measure;
            Beat = beat;
        }
    }
}

