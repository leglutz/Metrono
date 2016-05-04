using DiodeCompany.Metrono.Core.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace DiodeCompany.Metrono.Core.ViewModels
{
    public abstract class ViewModelBase : MvxViewModel
    {
        private readonly MvxSubscriptionToken _lifeCycleMessageSubscriptionToken;

        protected ViewModelBase ()
        {
            _lifeCycleMessageSubscriptionToken = Mvx.Resolve<IMvxMessenger> ().Subscribe<LifeCycleMessage> (OnLifeCycleMessage);
        }

        protected virtual void OnLifeCycleMessage (LifeCycleMessage lifeCycleMessage)
        {
        }
    }
}

