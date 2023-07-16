﻿using SunTech.Application.Events;
using SunTech.Domain.Customer;
using SunTech.Infrastructure.Services.CosmosDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Customers.Commands
{
    public class UpdateCustomerCommandHandler
    {
        private readonly ICosmosDbService _cdbService;
        private readonly string _dbName;
        private readonly string _containerName;

        public UpdateCustomerCommandHandler(ICosmosDbService cdbService, string dbName, string containerName)
        {
            _cdbService = cdbService;
            _dbName = dbName;
            _containerName = containerName;
        }

        public Customer Handle(OnCustomerUpdatedEvent e)
        {
            var customer = new Customer()
            {
                BirthdayInEpoch = e.Birthday.ToUnixTimeSeconds(),
                Email = e.Email,
                FirstName = e.FirstName,
                id = e.id,
                LastName = e.LastName,
            };

            return _cdbService.UpsertItem<Customer>(customer, _dbName, _containerName, true).GetAwaiter().GetResult();
        }
    }
}
