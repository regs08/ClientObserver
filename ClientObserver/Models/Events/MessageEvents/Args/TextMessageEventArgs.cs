namespace ClientObserver.Models.MessageEvents
{
    public class TextMessageEventArgs : MessageEventArgs<string>
    {
        public TextMessageEventArgs(string text) : base(text)
        {
        }
    }
}
