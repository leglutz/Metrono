using MvvmCross.Plugins.Messenger;

namespace DiodeCompany.Metroid.Core.Messages
{
    public enum LifeCycleEvent
    {
        Start,
        Stop,
        Lock,
        Destroy
    }

    public class LifeCycleMessage : MvxMessage
    {
        public LifeCycleEvent LifeCycleEvent { get; private set;}

        public LifeCycleMessage (object sender, LifeCycleEvent lifeCycleEvent) : base(sender)
        {
            LifeCycleEvent = lifeCycleEvent;
        }
    }
}

