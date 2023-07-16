using SunTech.Application.CustomersSummary.Queries;
using SunTech.Domain.Customer;
using System.Collections.Generic;

namespace SunTech.Application.EventHandlers
{
    public interface ICustomerEventHandler
    {
        CustomersCount GetCount();
        Customer GetCustomer();
        IEnumerable<Customer> GetCustomers();
        void Subscribe(ICustomerEventBroker customerEventBroker);
    }
}