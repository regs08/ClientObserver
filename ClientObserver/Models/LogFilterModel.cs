// filter model for our logs used to filter out with user selected confidence
// labels etc ..

//todo implement this properly
using ClientObserver.Configs;

namespace ClientObserver.Models
{
    public class LogFilterModel
    {
        private ModelParamConfig _config;
        public List<string> SelectedLabels;
        public double ConfidenceThreshold;

        public LogFilterModel(ModelParamConfig config)
        {
            _config = config;
            SelectedLabels = _config.SelectedLabels;
            ConfidenceThreshold = _config.ConfidenceThreshold;
            // More filter criteria... 
        }

    }
}