using System;
using ClientObserver.Models.Interfaces;

namespace ClientObserver.Models.Server.Framework
{
	public abstract class ServerFrameworkEntity: IIdentifiableModel
	{

		public string Name { get; set; }
		public ServerFrameworkEntity()
		{
		}
        /// <summary>
        /// Sets the server name, ensuring it is not null or whitespace.
        /// </summary>
        /// <param name="name">The name of the server.</param>
        public void SetServerName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Server name cannot be null or whitespace.", nameof(name));

            Name = name;
        }
    }
}

