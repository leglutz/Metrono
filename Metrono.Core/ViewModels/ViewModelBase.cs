using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metrono.Core.Messages;
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

