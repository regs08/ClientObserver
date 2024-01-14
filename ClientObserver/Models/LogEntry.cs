using System;
using Newtonsoft.Json.Linq;

namespace ClientObserver.Models
{
    public class LogEntry
    {
        public string Entry { get; private set; }
        public string Event { get; private set; }
        public string Label { get; private set; }
        public float Confidence { get; private set; }
        public string Filename { get; private set; }
        public DateTime Timestamp { get; private set; }
        public int Xmin { get; private set; }
        public int Ymin { get; private set; }
        public int Xmax { get; private set; }
        public int Ymax { get; private set; }

        public string FormattedLog
        {
            get
            {
                return FormatLogMessage(this);
            }
        }

        public LogEntry(string jsonString)
        {
            JObject json = JObject.Parse(jsonString);

            Entry = json["entry"].ToString();
            Event = json["event"].ToString();
            Label = json["label"].ToString();
            Confidence = float.Parse(json["confidence"].ToString());
            Filename = json["filename"].ToString();
            Timestamp = DateTime.ParseExact(json["timestamp"].ToString(), "yyyyMMdd_HHmmss", null);
            Xmin = int.Parse(json["xmin"].ToString());
            Ymin = int.Parse(json["ymin"].ToString());
            Xmax = int.Parse(json["xmax"].ToString());
            Ymax = int.Parse(json["ymax"].ToString());
        }



        private string FormatLogMessage(LogEntry logEntry)
        {
            // Build a formatted log message based on the log entry properties
            return $"Event: {logEntry.Event}, Label: {logEntry.Label}, Confidence: {logEntry.Confidence}, TimeStamp: {logEntry.Timestamp}";
        }
    }
}
