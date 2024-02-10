using System;
namespace ClientObserver.Models.Servers
{
	public abstract class ServerManagerBase
	{

		public string ServerName { get; set; }
		public ServerManagerBase()
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

            ServerName = name;
        }
    }
}

