﻿using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace SimpleMVVM.Messages
{
    public enum ShellState
    {
        BusyOn,
        BusyOff,
    }

    public sealed class ShellStateMessage : AsyncRequestMessage<ShellState>
    {

        public ShellStateMessage(ShellState state)
        {
            State = state;
        }

        public ShellState State { get; }
    }
}
