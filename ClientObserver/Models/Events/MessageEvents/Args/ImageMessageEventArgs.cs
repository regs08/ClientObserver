// Mqtt message event class used when receiving images from an mqtt client 
namespace ClientObserver.Models.MessageEvents
{
    public class ImageMessageEventArgs : MessageEventArgs<byte[]>
    {
        public ImageMessageEventArgs(byte[] imageData) : base(imageData)
        {
        }
    }
}


