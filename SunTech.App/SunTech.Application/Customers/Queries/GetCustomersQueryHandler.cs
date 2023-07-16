using SunTech.Domain.Customer;
using SunTech.Infrastructure.Services.CosmosDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SunTech.Application.Customers.Queries
{
    public class GetCustomersQueryHandler 
    {

        private readonly ICosmosDbService _cdbService;
        public GetCustomersQueryHandler(ICosmosDbService cosmosDbService)
        {
            _cdbService = cosmosDbService;
        }

        public async Task<IEnumerable<Customer>> Handle(string dbName, string containerName)
        {
            var result = await _cdbService.GetItems<Customer>(dbName, containerName);

            return result;
        }
    }
}
