using System.Text;
using Newtonsoft.Json;

namespace ClientObserver.Models.Server.Core.Configs

{
    /// <summary>
    /// Config to store parameters relevant to the model processing input data.
    /// In the future, this can be configured by the user and act as a filter for incoming data.
    /// For example, to only process images with a certain confidence level.
    /// </summary>
    public class ModelParamConfig : BaseConfig
    {
        /// <summary>
        /// List of labels selected by the user.
        /// </summary>
        public List<string> SelectedLabels { get; set; }

        /// <summary>
        /// List of available labels for selection.
        /// </summary>
        public List<string> AvailableLabels { get; set; }

        /// <summary>
        /// Confidence threshold for processing data.
        /// </summary>
        public double ConfidenceThreshold { get; set; }

        /// <summary>
        /// Provides a formatted display string of the configuration's details.
        /// </summary>

        //Initialize constructoir with the given config name 
        public ModelParamConfig() : base("ModelParamConfig")
        {
        }
        /// <summary>
        /// Overrides the Validate method to check for null properties specific to ModelParamConfig.
        /// </summary>
        /// <returns>
        /// A string representing the name of the first property found to be null, or null if all properties are valid.
        /// </returns>
        public override string Validate()
        {
            // First, use the base class validation to check for null or empty string properties
            var baseValidationResult = base.Validate();
            if (!string.IsNullOrEmpty(baseValidationResult))
            {
                return baseValidationResult;
            }

            // Then, perform specific validations for ModelParamConfig
            if (SelectedLabels == null)
            {
                return nameof(SelectedLabels);
            }
            if (AvailableLabels == null)
            {
                return nameof(AvailableLabels);
            }

            return null; // All properties are valid
        }
        /// <summary>
        /// Formats the configuration details for display.
        /// </summary>
        /// <returns>A formatted string of the configuration details.</returns>
        protected override string FormatForDisplay()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Configuration Name: {Name}");
            sb.AppendLine($"Selected Labels: {string.Join(", ", SelectedLabels)}");
            sb.AppendLine($"Confidence Threshold: {ConfidenceThreshold}");

            // Optionally, use JsonConvert to pretty-print complex properties
            // Ensure you have a reference to Newtonsoft.Json
            if (AvailableLabels != null && AvailableLabels.Count > 0)
            {
                sb.AppendLine("Available Labels:");
                sb.AppendLine(JsonConvert.SerializeObject(AvailableLabels, Formatting.Indented));
            }

            return sb.ToString().TrimEnd(); // Removes the last newline for cleanliness
        }
    }
}
