using SunTech.Application.EventHandlers;
using SunTech.Application.Events;
using SunTech.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Customers.Queries
{
    public class GetCustomersQuery : CustomerEvent
    {
        private readonly ICustomerEventBroker _ceBroker;

        public GetCustomersQuery(ICustomerEventBroker ceBroker)
        {
            _ceBroker = ceBroker;
            _ceBroker.Handle(this);
        }
    }
}
