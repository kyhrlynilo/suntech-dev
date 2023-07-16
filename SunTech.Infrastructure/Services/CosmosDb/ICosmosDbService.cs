using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SunTech.Infrastructure.Services.CosmosDb
{
    public interface ICosmosDbService
    {
        Task<T> DeleteItem<T>(string id, string databaseName, string containerName);
        Task<T> GetItem<T>(string id, string databaseName, string containerName);
        Task<IEnumerable<T>> GetItems<T>(string databaseName, string containerName, string query = "SELECT * FROM T");
        Task<T> UpsertItem<T>(T item, string databaseName, string containerName, bool isAdd);
    }
}