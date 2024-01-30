// Config to store params relevant to the model processing input data
// In the future I want this to be able to be configured by the user and act as a filter for incoming data.
// I want to send this as a package to the backend in order to modify which data is sent
// eg i want only images with a 80% confidence 

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

