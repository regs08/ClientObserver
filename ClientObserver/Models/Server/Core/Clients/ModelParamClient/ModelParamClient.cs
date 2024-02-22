using System;
using ClientObserver.Models.Server.Core.Configs;
using ClientObserver.Services.Server.Core.Clients;

namespace ClientObserver.Models.Server.Core.Clients
{
    public class ModelParamClient : BaseClientModel
    {
        public List<string> SelectedLabels { get; set; }

        /// <summary>
        /// List of available labels for selection.
        /// </summary>
        public List<string> AvailableLabels { get; set; }

        /// <summary>
        /// Confidence threshold for processing data.
        /// </summary>
        public double ConfidenceThreshold { get; set; }

        public ModelParamClient(ModelParamConfig config) : base(config, "ModelParamClient")
        {
            Config = config;
            SetClientService(new ModelParamService(this));
            InitializeWithConfig();

        }
    }
}



