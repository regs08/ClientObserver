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
        private readonly object _logsLock = new object();

        public event EventHandler LogUpdated;

        public LogService(MqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;
            _mqttClientService.LogReceived += OnMqttLogReceived;
        }
        private void OnMqttLogReceived(object sender, LogMessageEventArgs e)
        {
            var logEntries = ProcessLogEntries(e.Data);

            lock (_logsLock)
            {
                foreach (var logEntry in logEntries)
                {
                    _logs.Add(logEntry);

                    while (_logs.Count > 10)
                    {
                        _logs.RemoveAt(0);
                    }
                }
            }

            LogUpdated?.Invoke(this, EventArgs.Empty);
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

            lock (_logsLock)
            {
                foreach (var logEntry in _logs)
                {
                    displayLogs.Add(logEntry.FormattedLog);
                }
            }

            return displayLogs;
        }

    }
}
