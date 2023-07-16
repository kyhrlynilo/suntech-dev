using SunTech.Domain.Customer;
using SunTech.Infrastructure.Services.CosmosDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SunTech.Application.Customers.Queries
{
    public class GetCustomerQueryHandler 

    {
        private readonly ICosmosDbService _cdbService;
        public GetCustomerQueryHandler(ICosmosDbService cosmosDbService)
        {
            _cdbService = cosmosDbService;
        }

        public async Task<Customer> Handle(string id, string dbName, string containerName)
        {
            var customer = await _cdbService.GetItem<Customer>(id, dbName, containerName);

            return customer;
        }
    }
}
