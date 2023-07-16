using domains = SunTech.Domain.Customer;
using SunTech.Infrastructure.Services.CosmosDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SunTech.Application.CustomersSummary.Commands
{
    public class CalculateCustomerSummaryCommandHandler
    {
        ICosmosDbService _cdbService;

        public CalculateCustomerSummaryCommandHandler(ICosmosDbService cosmosDbService)
        {
            _cdbService = cosmosDbService;
        }

        public void Handle(string dbName, string customerContainerName, string customerSummaryContainerName)
        {

            var customers = _cdbService.GetItems<domains.Customer>(dbName, customerContainerName).GetAwaiter().GetResult();
            var customersCount = customers.ToList().Count();

            domains.CustomerSummary summary = new domains.CustomerSummary()
            {
                CustomerCount = customersCount,
                id = Guid.NewGuid().ToString(),
                Timestamp = DateTimeOffset.UtcNow
            };

            _cdbService.UpsertItem<domains.CustomerSummary>(summary, dbName, customerSummaryContainerName, true).GetAwaiter().GetResult();
        }
    }
}
