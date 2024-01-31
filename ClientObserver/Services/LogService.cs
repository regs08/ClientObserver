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
        // Reference to the MQTT client service
        private MqttClientService _mqttClientService;

        // List to store log entries
        private List<LogEntry> _logs = new List<LogEntry>();

        // Object used for locking to ensure thread safety
        private readonly object _logsLock = new object();

        // Event to notify when logs are updated
        public event EventHandler LogUpdated;

        // Constructor that subscribes to MQTT client service events
        public LogService(MqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;

            // Subscribing to LogReceived event from the MQTT client
            _mqttClientService.LogReceived += OnMqttLogReceived;
        }

        // Handles the LogReceived event from the MQTT client
        private void OnMqttLogReceived(object sender, LogMessageEventArgs e)
        {
            // Process the log entries received in the event args
            var logEntries = ProcessLogEntries(e.Data);

            // Thread-safe addition and maintenance of the log entries list
            lock (_logsLock)
            {
                foreach (var logEntry in logEntries)
                {
                    // Adds new log entry
                    _logs.Add(logEntry);

                    // Ensures the log list doesn't exceed a maximum size (e.g., 10 entries)
                    while (_logs.Count > 10)
                    {
                        // Removes the oldest log entry
                        _logs.RemoveAt(0);
                    }
                }
            }

            // Raises the LogUpdated event
            LogUpdated?.Invoke(this, EventArgs.Empty);
        }

        // Processes raw JSON array string into a list of LogEntry objects
        private List<LogEntry> ProcessLogEntries(string jsonArrayString)
        {
            var jsonArray = JArray.Parse(jsonArrayString);
            var logEntries = new List<LogEntry>();

            foreach (var jsonObject in jsonArray)
            {
                // Converts each JSON object into a LogEntry
                string jsonString = jsonObject.ToString();
                LogEntry logEntry = new LogEntry(jsonString);
                logEntries.Add(logEntry);
            }

            return logEntries;
        }

        // Retrieves a list of formatted logs for display purposes
        public List<string> GetDisplayLogs()
        {
            List<string> displayLogs = new();

            // Thread-safe reading of logs
            lock (_logsLock)
            {
                foreach (var logEntry in _logs)
                {
                    // Adds the formatted log string for each log entry
                    displayLogs.Add(logEntry.FormattedLog);
                }
            }

            return displayLogs;
        }
    }
}
