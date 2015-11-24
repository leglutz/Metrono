using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using DiodeTeam.Metroid.Core.Helpers;
using MvvmCross.Plugins.Messenger;

namespace DiodeTeam.Metroid.Core.ViewModels
{
    public abstract class ViewModelBase : MvxViewModel
    {
        private readonly MvxSubscriptionToken _subscriptionToken;

        protected ViewModelBase ()
        {
            _subscriptionToken = Mvx.Resolve<IMvxMessenger> ().Subscribe<LifeCycleMessage> (OnLifeCycleMessage);
        }

        protected virtual void OnLifeCycleMessage (LifeCycleMessage lifeCycleMessage)
        {
        }
    }
}

