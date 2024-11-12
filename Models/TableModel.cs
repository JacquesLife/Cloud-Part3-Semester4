using Azure;
using Azure.Data.Tables;

namespace WebApplication1.Models
{
    public class TableEntityModel : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

        // Constructor for initializing the PartitionKey and RowKey
        public TableEntityModel(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        // Parameterless constructor for Table SDK
        public TableEntityModel() { }
    }
}
