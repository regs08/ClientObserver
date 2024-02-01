using System;
using System.Collections.Generic; // Required for List<>
using ClientObserver.Models.Configs; // Required if BaseConfig is in a different namespace

namespace ClientObserver.Models.Configs
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
    }
}
