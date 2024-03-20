using System;
using ClientObserver.Models.Interfaces.Messaging;
using CommunityToolkit.Mvvm.Messaging;

namespace ClientObserver.Services.App.Messaging
{
    public class MessagingService : IMessagingService
    {
        public void Register<TRecipient, TMessage>(TRecipient recipient, Action<TRecipient, TMessage> action)
            where TRecipient : class where TMessage : class
        {
            WeakReferenceMessenger.Default.Register<TRecipient, TMessage>(recipient, (recipient, message) => action(recipient, message));
        }

        public void Send<TMessage>(TMessage message) where TMessage : class
        {
            WeakReferenceMessenger.Default.Send(message);
        }

        public void UnregisterAll(object recipient)
        {
            WeakReferenceMessenger.Default.UnregisterAll(recipient);
        }

    }
}
