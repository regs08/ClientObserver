using System;
using ClientObserver.Models.Server.Instance;
namespace ClientObserver.Services.Server.Framework.Base
{
	public class DeviceServices
	{
		public ServerInstance Device; 
		public DeviceServices(ServerInstance device)
		{
			Device = device;

		}

	}
}

