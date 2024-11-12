using Azure.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class TableStorageService
    {
        private readonly TableClient _tableClient;

        // Constructor that initializes the TableClient
        public TableStorageService(string connectionString, string tableName)
        {
            _tableClient = new TableClient(connectionString, tableName);
        }

        // Method to query all entities in the table
        public async Task<IEnumerable<TableEntityModel>> QueryEntitiesAsync()
        {
            // Query all entities in the table asynchronously
            var entities = _tableClient.Query<TableEntityModel>();
            return await Task.FromResult(entities);
        }

        // Method to add an entity to the table
        public async Task AddEntityAsync(TableEntityModel entity)
        {
            // Add a new entity to the table asynchronously
            await _tableClient.AddEntityAsync(entity);
        }
    }
}
