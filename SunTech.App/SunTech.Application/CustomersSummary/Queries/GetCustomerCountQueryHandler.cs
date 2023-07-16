using SunTech.Infrastructure.Services.CosmosDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using model = SunTech.Domain.Customer;
using System.Linq;

namespace SunTech.Application.CustomersSummary.Queries
{
    public class GetCustomerCountQueryHandler
    {
        ICosmosDbService _cdbService;

        public GetCustomerCountQueryHandler(ICosmosDbService cosmosDbService)
        {
            _cdbService = cosmosDbService;
        }

        public async Task<CustomersCount> Handle(string dbName, string containerName)
        {
            var query = "SELECT TOP 1 * FROM T ORDER BY T.Timestamp DESC";
            var latest_summary = await _cdbService.GetItems<model.CustomerSummary>(dbName, containerName, query);

            CustomersCount result;

            if (latest_summary.FirstOrDefault() != null)
            {
                result = new CustomersCount()
                {
                    AsOf = latest_summary.FirstOrDefault().Timestamp,
                    Count = latest_summary.FirstOrDefault().CustomerCount
                };
            }
            else
            {
                result = new CustomersCount()
                {
                    AsOf = DateTimeOffset.UtcNow,
                    Count = 0
                };
            }

            return result;
        }
    }
}
