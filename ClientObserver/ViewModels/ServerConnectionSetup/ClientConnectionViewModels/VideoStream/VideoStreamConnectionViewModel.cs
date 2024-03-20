using System;
using ClientObserver.Models.Server.Core.Clients;

namespace ClientObserver.ViewModels.ServerConnectionSetup.ClientConnectionViewModels
{
	public class VideoStreamConnectionViewModel: BaseConnectionViewModel
	{
		public VideoStreamClient VideoStreamClient => ClientModel as VideoStreamClient;

		public VideoStreamConnectionViewModel(VideoStreamClient clientModel) : base(clientModel)
		{
		}
	}
}

