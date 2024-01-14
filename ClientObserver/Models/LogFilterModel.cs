namespace ClientObserver.Models
{
    public class LogFilterModel
    {
        private ServerConfig _config;
        public List<string> SelectedLabels;
        public double ConfidenceThreshold;

        public LogFilterModel(ServerConfig config)
        {
            _config = config;
            SelectedLabels = _config.SelectedLabels;
            ConfidenceThreshold = _config.ConfidenceThreshold;
            // More filter criteria... 
        }

    }
}