using System.Collections.ObjectModel;
using System.ComponentModel;
using ClientObserver.Services;

namespace ClientObserver.ViewModels
{
    public class LogViewModel : INotifyPropertyChanged
    {
        // Reference to the LogService
        private LogService _logService;

        // Collection for storing and displaying logs in the UI
        public ObservableCollection<string> DisplayLogs { get; private set; }

        // Event to notify when a property value changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor that initializes the ViewModel with the LogService
        public LogViewModel(LogService logService)
        {
            _logService = logService;
            InitializeDisplayLogs();
        }

        // Initializes the DisplayLogs collection and subscribes to log updates
        private void InitializeDisplayLogs()
        {
            var logs = _logService.GetDisplayLogs();
            if (logs != null)
            {
                // Initializes DisplayLogs with existing logs from the service
                DisplayLogs = new ObservableCollection<string>(logs);
            }
            else
            {
                // Handles the scenario where no initial logs are available
                DisplayLogs = new ObservableCollection<string>();
                // Optionally, add a placeholder or log a message
            }

            // Subscribes to the LogUpdated event to receive updates
            _logService.LogUpdated += OnLogUpdated;
        }

        // Handler for the LogUpdated event
        private void OnLogUpdated(object sender, System.EventArgs e)
        {
            var updatedLogs = _logService.GetDisplayLogs();
            if (updatedLogs != null)
            {
                // Clears the existing logs and adds the updated ones
                DisplayLogs.Clear();
                foreach (var log in updatedLogs)
                {
                    DisplayLogs.Add(log);
                }
            }
            else
            {
                // Handles the case where updated logs are null or empty
                DisplayLogs.Clear();
                // Optionally, add a placeholder or log a message
            }

            // Notify the UI that the DisplayLogs collection has been updated
            OnPropertyChanged(nameof(DisplayLogs));
        }

        // Method to invoke the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
