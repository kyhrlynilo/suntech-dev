using SunTech.Application.Events;
using SunTech.Domain.Customer;
using SunTech.Infrastructure.Services.CosmosDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Customers.Commands
{
    public class DeleteCustomerCommandHandler
    {
        private readonly ICosmosDbService _cdbService;
        private readonly string _dbName;
        private readonly string _containerName;

        public DeleteCustomerCommandHandler(ICosmosDbService cdbService, string dbName, string containerName)
        {
            _cdbService = cdbService;
            _dbName = dbName;
            _containerName = containerName;
        }

        public Customer Handle(OnCustomerDeletedEvent e)
        {
            var customer =  _cdbService.GetItem<Customer>(e.id, _dbName, _containerName).GetAwaiter().GetResult();

            _cdbService.DeleteItem<Customer>(e.id, _dbName, _containerName).GetAwaiter().GetResult();

            return customer;
        }
    }
}
