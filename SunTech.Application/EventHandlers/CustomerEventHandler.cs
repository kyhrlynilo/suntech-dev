using SunTech.Application.Customers.Commands;
using SunTech.Application.Customers.Queries;
using SunTech.Application.CustomersSummary.Commands;
using SunTech.Application.CustomersSummary.Queries;
using SunTech.Application.Events;
using SunTech.Domain.Customer;
using SunTech.Infrastructure.Services.CosmosDb;
using SunTech.Infrastructure.Services.CustomHttpClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.EventHandlers
{
    public class CustomerEventHandler : ICustomerEventHandler
    {
        private readonly ICustomerEventBroker _ceBroker;
        private readonly ICosmosDbService _cdbService;
        private readonly ICustomHttpClientService _httpClient;
        private readonly string _dbName;
        private readonly string _customerContainerName;
        private readonly string _customerSummaryContainerName;

        public Customer customer;
        public IEnumerable<Customer> customers;
        public CustomersCount Count;

        public CustomerEventHandler(ICustomerEventBroker ceBroker, ICosmosDbService cdbService, ICustomHttpClientService httpClient, string dbName, string containerName, string customerSummaryContainerName)
        {
            _ceBroker = ceBroker;
            _cdbService = cdbService;
            _httpClient = httpClient;
            _dbName = dbName;
            _customerContainerName = containerName;
            _customerSummaryContainerName = customerSummaryContainerName;
        }

        public void Subscribe(ICustomerEventBroker customerEventBroker)
        {
            _ceBroker.OnCustomerEvents += ProcessEvents;
        }

        public Customer GetCustomer()
        {
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return customers;
        }

        public CustomersCount GetCount()
        {
            return Count;
        }

        private void ProcessEvents(object sender, CustomerEvent e)
        {
            if (e != null)
            {
                if (e.GetType() == typeof(OnCustomerCreatedEvent))
                {
                    var data = new CreateCustomerCommandHandler(_cdbService, _dbName, _customerContainerName).Handle((OnCustomerCreatedEvent)e);

                    _httpClient.SendMessageToEventGrid("Customer", "Created", data);

                    new CalculateCustomerSummaryCommandHandler(_cdbService).Handle(_dbName, _customerContainerName, _customerSummaryContainerName);
                }

                if (e.GetType() == typeof(OnCustomerUpdatedEvent))
                {
                    var data = new UpdateCustomerCommandHandler(_cdbService, _dbName, _customerContainerName).Handle((OnCustomerUpdatedEvent)e);

                    _httpClient.SendMessageToEventGrid("Customer", "Updated", data);
                }

                if (e.GetType() == typeof(OnCustomerDeletedEvent))
                {
                    var data = new DeleteCustomerCommandHandler(_cdbService, _dbName, _customerContainerName).Handle((OnCustomerDeletedEvent)e);

                    _httpClient.SendMessageToEventGrid("Customer", "Updated", data);

                    new CalculateCustomerSummaryCommandHandler(_cdbService).Handle(_dbName, _customerContainerName, _customerSummaryContainerName);
                }

                if (e.GetType() == typeof(GetCustomerQuery))
                {
                    var input = (GetCustomerQuery)e;
                    var result = new GetCustomerQueryHandler(_cdbService).Handle(input.id, _dbName, _customerContainerName).GetAwaiter().GetResult();
                    customer = result;
                }

                if (e.GetType() == typeof(GetCustomersQuery))
                {
                    var results = new GetCustomersQueryHandler(_cdbService).Handle(_dbName, _customerContainerName).GetAwaiter().GetResult();
                    customers = results;
                }

                if (e.GetType() == typeof(GetCustomerCountQuery))
                {
                    var count = new GetCustomerCountQueryHandler(_cdbService).Handle(_dbName, _customerSummaryContainerName).GetAwaiter().GetResult();
                    Count = count;
                }
            }
        }
    }
}
