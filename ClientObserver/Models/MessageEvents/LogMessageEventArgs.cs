using System;

namespace ClientObserver.Models.MessageEvents // Replace with your actual namespace
{
    public class LogMessageEventArgs : EventArgs
    {
        // Property to hold the JSON array string of log entries
        public string JsonArrayString { get; private set; }

        // Constructor
        public LogMessageEventArgs(string jsonArrayString)
        {
            JsonArrayString = jsonArrayString;
        }
    }
}


