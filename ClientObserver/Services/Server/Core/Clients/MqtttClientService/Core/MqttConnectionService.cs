using System;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using ClientObserver.Models.Server.Core.Clients;
using ClientObserver.Services.Server.Core.Base;

namespace ClientObserver.Services.Server.Core.Clients.MqttClientService
{
    /// <summary>
    /// Provides services for managing network interactions of an MQTT client, including connecting to the broker,
    /// subscribing to topics, and disconnecting.
    /// </summary>
	public class MqttConnectionService : BaseClientConnectionService
    {
        public IMqttClient mqttNetworkClient;
        private MqttClientModel mqttClientParameters;

        /// <summary>
        /// Initializes a new instance of the MqttNetworkService with specified MQTT client model parameters.
        /// </summary>
        /// <param name="clientModel">The model containing MQTT client parameters.</param>
        public MqttConnectionService(MqttClientModel clientModel) : base(clientModel)
        {
            var factory = new MqttFactory();
            mqttNetworkClient = factory.CreateMqttClient();
            mqttClientParameters = clientModel;
        }
        /// <summary>
        /// Asynchronously connects to the MQTT broker using the configured client parameters.
        /// </summary>
        public override async Task<bool> InitializeConnection()
        {
            Console.WriteLine("Connecting to MQTT...");
            if (mqttNetworkClient.IsConnected)
            {
                Console.WriteLine("Client already connected.");
                return false;
            }

            var options = new MqttClientOptionsBuilder()
                .WithClientId(mqttClientParameters.ClientId)
                .WithTcpServer(mqttClientParameters.BrokerAddress, mqttClientParameters.BrokerPort)
                .WithCleanSession(mqttClientParameters.CleanSession)
                .Build();

            var cancellationTokenSource = new CancellationTokenSource();

            try
            {
                var connectTask = mqttNetworkClient.ConnectAsync(options, cancellationTokenSource.Token);

                if (await Task.WhenAny(connectTask, Task.Delay(1000, cancellationTokenSource.Token)) == connectTask)
                {
                    // Cancel the delay task if the connection task completed first
                    cancellationTokenSource.Cancel();
                    // If the task is this one, the connection was successful before the timeout.
                    Console.WriteLine("Connected successfully.");
                    return true;
                }
                else
                {
                    // If the delay task completed first, the connection attempt took too long.
                    Console.WriteLine("Connection attempt timed out.");
                }
            }
            catch (MQTTnet.Exceptions.MqttCommunicationException ex)
            {
                Console.WriteLine($"Failed to connect: {ex.Message}");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Operation was cancelled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                if (!mqttNetworkClient.IsConnected)
                {
                    try
                    {
                        // Attempt to cancel the connection attempt if it's still ongoing.
                        cancellationTokenSource.Cancel();
                    }
                    catch (Exception cancelEx)
                    {
                        Console.WriteLine($"Error cancelling the connection task: {cancelEx.Message}");
                    }
                }
            }

            return false;
        }

        public override async Task<bool> Authenticate()
        {
            return true;
        }
        public override async Task<bool> FinalizeConnection()
        {
            await SubscribeToConfiguredTopicsAsync();
            // todo figure out some logic to validate subsctibed topcis 
            return true; 
        }

        /// <summary>
        /// Asynchronously disconnects from the MQTT broker.
        /// </summary>
        public override async Task<bool> DisconnectFromClient()
        {
            await mqttNetworkClient.DisconnectAsync();
            if (mqttNetworkClient.IsConnected)
            {
                return false;
            }
            return true; 
        }

        /// <summary>
        /// Asynchronously subscribes to a specific topic.
        /// </summary>
        /// <param name="topic">The topic to subscribe to.</param>
        public async Task SubscribeAsync(string topic)
        {
            if (!mqttNetworkClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            await mqttNetworkClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }

        /// <summary>
        /// Asynchronously subscribes the MQTT client to all configured topics.
        /// </summary>
        public async Task SubscribeToConfiguredTopicsAsync()
        {
            if (!mqttNetworkClient.IsConnected)
            {
                throw new InvalidOperationException("MQTT client is not connected.");
            }

            foreach (var topic in mqttClientParameters.SubscriptionTopics.Topics)
            {
                await SubscribeAsync(topic);
                Console.WriteLine($"Subscribed to topic: {topic}");
            }
        }
    }
}
