using SunTech.Application.EventHandlers;
using SunTech.Application.Events;
using SunTech.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Customers.Queries
{
    public class GetCustomerQuery: CustomerEvent
    {
        public string id { get; set; }
        private readonly ICustomerEventBroker _ceBroker;

        public GetCustomerQuery(ICustomerEventBroker ceBroker, string id)
        {
            this.id = id;
            _ceBroker = ceBroker;

            _ceBroker.Handle(this);
        }
    }
}
