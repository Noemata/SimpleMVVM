using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace SimpleMVVM.Messages
{
    public sealed class ConfirmMessage : AsyncRequestMessage<bool>
    {

        public ConfirmMessage(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
