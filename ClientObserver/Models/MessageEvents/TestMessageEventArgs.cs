using System;

namespace ClientObserver.Models.MessageEvents
{
    public class TextMessageEventArgs : EventArgs
    {
        // Property to hold the text data
        public string Text { get; private set; }

        public TextMessageEventArgs(string text)
        {
            Text = text;
        }
    }
}
