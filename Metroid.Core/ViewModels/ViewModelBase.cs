using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using DiodeCompany.Metroid.Core.Helpers;
using MvvmCross.Plugins.Messenger;

namespace DiodeCompany.Metroid.Core.ViewModels
{
    public abstract class ViewModelBase : MvxViewModel
    {
        private readonly MvxSubscriptionToken _subscriptionToken;

        protected ViewModelBase ()
        {
            _subscriptionToken = Mvx.Resolve<IMvxMessenger> ().SubscribeOnThreadPoolThread<LifeCycleMessage> (OnLifeCycleMessage);
        }

        protected virtual void OnLifeCycleMessage (LifeCycleMessage lifeCycleMessage)
        {
        }
    }
}

