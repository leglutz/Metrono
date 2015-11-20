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

        private void OnLifeCycleMessage (LifeCycleMessage lifeCycleMessage)
        {
            switch(lifeCycleMessage.LifeCycleEvent)
            {
                case LifeCycleEvent.Show:
                    Show();
                    break;
                case LifeCycleEvent.Hide:
                    Hide ();
                    break;
                case LifeCycleEvent.Dispose:
                    Dispose ();
                    break;
            }
        }

        protected virtual void Show()
        {}

        protected virtual void Hide()
        {}

        protected virtual void Dispose()
        {}
    }
}

