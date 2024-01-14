using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ClientObserver.Models;
using ClientObserver.Services;
using ClientObserver.Models.MessageEvents;

namespace ClientObserver.Services
{
    public class LogService
    {
        private MqttClientService _mqttClientService;
        private List<LogEntry> _logs = new List<LogEntry>();

        public event EventHandler LogUpdated;

        public LogService(MqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
            _mqttClientService.LogReceived += OnMqttLogReceived;
        }

        private void OnMqttLogReceived(object sender, LogMessageEventArgs e)
        {
            var logEntries = ProcessLogEntries(e.JsonArrayString);
            foreach (var logEntry in logEntries)
            {
                _logs.Add(logEntry);

                // Ensure only the latest 10 logs are kept
                while (_logs.Count > 10)
                {
                    _logs.RemoveAt(0); // Removes the oldest log entry
                }
                // Notify subscribers
                LogUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        private List<LogEntry> ProcessLogEntries(string jsonArrayString)
        {
            var jsonArray = JArray.Parse(jsonArrayString);
            var logEntries = new List<LogEntry>();

            foreach (var jsonObject in jsonArray)
            {
                // Convert each JObject to a string before passing it to the LogEntry constructor
                string jsonString = jsonObject.ToString();
                LogEntry logEntry = new LogEntry(jsonString);
                logEntries.Add(logEntry);
            }

            return logEntries;
        }

        public List<string> GetDisplayLogs()
        {
            List<string> displayLogs = new();
            foreach (LogEntry logEntry in _logs)
            {
                displayLogs.Add(logEntry.FormattedLog);
            }
            return displayLogs;
        }
    }
}


/*using System;
using System.Collections.Generic;
using ClientObserver.Services;
using ClientObserver.Models;
using ClientObserver.Models.MessageEvents;
using Newtonsoft.Json.Linq;
// todo put a cap pn number of logs to be displayd
// have another buttojn that displays all logs? 
namespace ClientObserver.Services
{
    public class LogService
    {
        private MqttClientService _mqttClientService;
        private List<LogEntry> _logs;
        public List<LogEntry> Logs => _logs;
        public event EventHandler LogUpdated;
        private ServerConfig _serverConfig;
        private LogFilterModel _logFilterModel;
        private readonly object _logsLock = new object();

        public LogService(MqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
            _logs = new List<LogEntry>();
            _serverConfig = mqttClientService.Config;
            _logFilterModel = new LogFilterModel(_serverConfig); 
            _mqttClientService.LogReceived += OnMqttLogReceived;


        }


        private void OnMqttLogReceived(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine("log received");

            List<LogEntry> logEntries = ProcessLogEntries(e.JsonArrayString);
            Console.WriteLine($"Num logs received {logEntries.Count}");
            lock (_logsLock)
            {
                foreach (var logEntry in logEntries)
                {
                    // Add the new log entry
                    _logs.Add(logEntry);

                    // If the list exceeds 10 items, remove the oldest one
                    while (_logs.Count > 10)
                    {
                        _logs.RemoveAt(0);
                    }
                }
            }

            // Notify that the logs have been updated
            LogUpdated?.Invoke(this, EventArgs.Empty);
        }

        public List<string> ParseLogs()
        {
            // Uses our log filter model to filter and parse logs.
            List<string> parsedLogs = new List<string>();
            List<LogEntry> logsCopy;

            // Synchronize access to the _logs collection
            lock (_logsLock)
            {
                // Create a copy of the _logs collection for safe iteration
                logsCopy = new List<LogEntry>(_logs);
                //
                //Console.WriteLine($"Log entry:{logsCopy[0].Entry}"); 
            }

            // Iterate over the copy of the logs
            foreach (LogEntry logEntry in logsCopy)
            {
                if (logEntry != null &&
                    logEntry.Event == "detection" &&
                    _logFilterModel.SelectedLabels.Contains(logEntry.Label) &&
                    logEntry.Confidence >= _logFilterModel.ConfidenceThreshold)
                {
                    // Build a formatted log message
                    string logMessage = FormatLogMessage(logEntry);

                    // Add the formatted log to the list
                    parsedLogs.Add(logMessage);
                }
            }

            return parsedLogs;
        }


        public static List<LogEntry> ProcessLogEntries(string jsonArrayString)
        {
            // look into here and the log entry method i think were passing in a string when the
            // log entry model expects a json thing ask GPT and should be okay 
            List<LogEntry> logEntries = new List<LogEntry>();

            var jsonArray = JArray.Parse(jsonArrayString);
            foreach (var jsonObject in jsonArray)
            {
                LogEntry logEntry = new LogEntry(jsonObject.ToString());
                Console.WriteLine($"Log entry Event: {logEntry.Event}");
                logEntries.Add(logEntry);
            }

            return logEntries;
        }

        private string FormatLogMessage(LogEntry logEntry)
        {
            // Build a formatted log message based on the log entry properties
            return $"Event: {logEntry.Event}, Label: {logEntry.Label}, Confidence: {logEntry.Confidence}, TimeStamp: {logEntry.Timestamp}";
        }
    }
}
*/ 