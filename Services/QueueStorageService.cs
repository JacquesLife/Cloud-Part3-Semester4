using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class QueueStorageService
    {
        private readonly QueueClient _queueClient;

        // Constructor that initializes the QueueClient
        public QueueStorageService(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName);
        }

        // Method to send a message to the queue
        public async Task SendMessageAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
        }

        // Method to receive a message from the queue
        public async Task<QueueMessage> ReceiveMessageAsync()
        {
            var messages = await _queueClient.ReceiveMessagesAsync();
            return messages.Value.FirstOrDefault();
        }

        // Method to delete a message from the queue
        public async Task DeleteMessageAsync(string messageId, string popReceipt)
        {
            await _queueClient.DeleteMessageAsync(messageId, popReceipt);
        }

        // Method to view all messages in the queue
        public async Task<IEnumerable<QueueMessage>> ViewMessagesAsync()
        {
            var messages = await _queueClient.ReceiveMessagesAsync(maxMessages: 32); // Maximum number of messages to retrieve
            return messages.Value;
        }
    }
}