using System;
namespace ClientObserver.Models.Configs
{
	public class ModelParamConfig
	{
        public List<string> SelectedLabels { get; set; }
        public List<string> AvailableLabels { get; set; }
        public double ConfidenceThreshold { get; set; }

        // Method to check for null properties
        public string NullProperties()
        {
            if (SelectedLabels == null)
            {
                return nameof(SelectedLabels);
            }
            if (AvailableLabels == null)
            {
                return nameof(AvailableLabels);
            }
            // No need to check ConfidenceThreshold as it's a value type and cannot be null

            return null; // or return String.Empty if you prefer
        }
    }

}

