using System;
namespace ClientObserver.Models.MessageEvents
{
    public class ImageMessageEventArgs : EventArgs
    {
        // Property to hold the image data
        public byte[] ImageData { get; private set; }

        public ImageMessageEventArgs(byte[] imageData)
        {
            ImageData = imageData;
        }
    }
}

