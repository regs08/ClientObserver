using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientObserver.Models;
using ClientObserver.Services;

namespace ClientObserver.ViewModels
{
    public class LogViewModel : INotifyPropertyChanged
    {
        private LogService _logService;
        public ObservableCollection<string> DisplayLogs { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LogViewModel(LogService logService)
        {
            _logService = logService;
            DisplayLogs = new ObservableCollection<string>(_logService.GetDisplayLogs());

            // Subscribe to LogUpdated event
            _logService.LogUpdated += OnLogUpdated;
        }

        private void OnLogUpdated(object sender, System.EventArgs e)
        {
            // Update DisplayLogs collection
            DisplayLogs.Clear();
            foreach (var log in _logService.GetDisplayLogs())
            {
                DisplayLogs.Add(log);
            }

            // Notify the UI that the DisplayLogs collection has been updated
            OnPropertyChanged(nameof(DisplayLogs));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Add other methods to interact with the log service if necessary
    }
}



/*
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientObserver.Services;

namespace ClientObserver.ViewModels
{
    public class LogViewModel : INotifyPropertyChanged
    {
        private LogService _logService;
        public ObservableCollection<string> DisplayLogs { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LogViewModel(LogService logService)
        {
            _logService = logService;
            DisplayLogs = new ObservableCollection<string>();

            // Subscribe to the LogUpdated event
            _logService.LogUpdated += LogService_LogUpdated;
            LoadLogs();
        }

        private void LogService_LogUpdated(object sender, EventArgs e)
        {
            // Ensure UI updates happen on the main thread
            Device.BeginInvokeOnMainThread(() =>
            {
                LoadLogs();
            });
        }

        private void LoadLogs()
        {
            Console.Write($"display logs: {DisplayLogs}");
            DisplayLogs.Clear();
            var parsedLogs = _logService.ParseLogs();
            Console.Write($"Length of parsed logs {parsedLogs.Count}");
            foreach (var log in parsedLogs)
            {
                Console.Write($"Current log entry: {log}");
                DisplayLogs.Add(log);
            }

            // Notify the UI that DisplayLogs has been updated
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayLogs)));
        }
    }
}
*/ 