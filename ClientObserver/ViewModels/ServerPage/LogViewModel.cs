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
            InitializeDisplayLogs();
        }

        private void InitializeDisplayLogs()
        {
            var logs = _logService.GetDisplayLogs();
            if (logs != null)
            {
                DisplayLogs = new ObservableCollection<string>(logs);
            }
            else
            {
                // Handle null or empty logs appropriately
                DisplayLogs = new ObservableCollection<string>();
                // Optionally, add a placeholder or log a message
            }

            // Subscribe to LogUpdated event
            _logService.LogUpdated += OnLogUpdated;
        }

        private void OnLogUpdated(object sender, System.EventArgs e)
        {
            var updatedLogs = _logService.GetDisplayLogs();
            if (updatedLogs != null)
            {
                DisplayLogs.Clear();
                foreach (var log in updatedLogs)
                {
                    DisplayLogs.Add(log);
                }
            }
            else
            {
                // Handle the case where updated logs are null or empty
                DisplayLogs.Clear();
                // Optionally, add a placeholder or log a message
            }

            OnPropertyChanged(nameof(DisplayLogs));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
