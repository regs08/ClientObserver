using System;
namespace ClientObserver.Models.Interfaces.Messaging
{
    
        public interface IMessagingService
        {
            // Register a recipient for a specific message type with an action to execute.
            void Register<TRecipient, TMessage>(TRecipient recipient, Action<TRecipient, TMessage> action)
                where TRecipient : class
                where TMessage : class;

            // Send a message to all registered recipients.
            void Send<TMessage>(TMessage message) where TMessage : class;

            // Unregister all messages for a specific recipient.
            void UnregisterAll(object recipient);
        }
    


}

