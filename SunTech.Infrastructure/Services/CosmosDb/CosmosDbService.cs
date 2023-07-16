
using Microsoft.Azure.Cosmos;
using SunTech.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SunTech.Infrastructure.Services.CosmosDb
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosClient _client;

        public CosmosDbService(string accountUri, string primaryKey)
        {
            _client = new CosmosClient(accountUri, primaryKey);
        }

        public async Task<T> UpsertItem<T>(T item, string databaseName, string containerName, bool isAdd)
        {
            var container = _client.GetContainer(databaseName, containerName);
            var result = await container.UpsertItemAsync<T>(item);

            return result;
        }

        public async Task<T> DeleteItem<T>(string id, string databaseName, string containerName)
        {
            var container = _client.GetContainer(databaseName, containerName);
            var result = await container.DeleteItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));

            return result;
        }

        public async Task<IEnumerable<T>> GetItems<T>(string databaseName, string containerName, string query = "SELECT * FROM T")
        {
            QueryDefinition queryDefinition = new QueryDefinition(query);
            var container = _client.GetContainer(databaseName, containerName);

            FeedIterator<T> queryResultSetIterator = container.GetItemQueryIterator<T>(queryDefinition);
            List<T> items = new List<T>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (T item in currentResultSet)
                {
                    items.Add(item);
                }
            }

            return items;
        }

        public async Task<T> GetItem<T>(string id, string databaseName, string containerName)
        {
            var container = _client.GetContainer(databaseName, containerName);
            ItemResponse<T> response = await container.ReadItemAsync<T>(id, new PartitionKey(id.ToString()));

            var customer = response.Resource;

            return customer;
        }
    }
}
