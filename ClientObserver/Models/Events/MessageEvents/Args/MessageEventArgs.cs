using System;

namespace ClientObserver.Models.MessageEvents
{
    public class MessageEventArgs<TData> : EventArgs
    {
        public TData Data { get; private set; }

        public MessageEventArgs(TData data)
        {
            Data = data;
        }
    }
}
