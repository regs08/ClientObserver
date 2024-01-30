using System;

// Define a namespace for the ClientObserver.Models.MessageEvents
namespace ClientObserver.Models.MessageEvents
{
    // Declare a public enum named 'MessageType'
    // Enums are distinct types consisting of a set of named constants called the enumerator list.
    public enum MessageType
    {
        Image, // Represents a message type for images
        Log,   // Represents a message type for logs
        Text   // Represents a message type for text
    }
}
