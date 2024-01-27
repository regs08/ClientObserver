// Mqtt message event class used when receiving logs from an mqtt client.

namespace ClientObserver.Models.MessageEvents
{
    public class LogMessageEventArgs : MessageEventArgs<string>
    {
        public LogMessageEventArgs(string jsonArrayString) : base(jsonArrayString)
        {
        }
    }
}



