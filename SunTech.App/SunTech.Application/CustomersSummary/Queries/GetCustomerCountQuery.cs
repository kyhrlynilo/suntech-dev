using SunTech.Application.EventHandlers;
using SunTech.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.CustomersSummary.Queries
{
    public class CustomersCount
    {
        public DateTimeOffset AsOf { get; set; }
        public int Count { get; set; }
    }
    public class GetCustomerCountQuery: CustomerEvent
    {
        private readonly ICustomerEventBroker _ceBroker;

        public GetCustomerCountQuery(ICustomerEventBroker ceBroker)
        {
            _ceBroker = ceBroker;
            _ceBroker.Handle(this);
        }
    }
}
