﻿using System;
using MvvmCross.Plugins.Messenger;

namespace DiodeTeam.Metroid.Core.Helpers
{
    public enum LifeCycleEvent
    {
        Show,
        Hide,
        Dispose
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
