using SunTech.Application.Customers.Queries;
using SunTech.Application.CustomersSummary.Queries;
using SunTech.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.EventHandlers
{
    public class CustomerEventBroker : ICustomerEventBroker
    {
        public event EventHandler<CustomerEvent> OnCustomerEvents;

        public void Handle(OnCustomerCreatedEvent createdEvent)
        {
            if (OnCustomerEvents != null)
                OnCustomerEvents(this, createdEvent);
        }

        public void Handle(OnCustomerUpdatedEvent updatedEvent)
        {
            if (OnCustomerEvents != null)
                OnCustomerEvents(this, updatedEvent);
        }

        public void Handle(OnCustomerDeletedEvent deletedEvent)
        {
            if (OnCustomerEvents != null)
                OnCustomerEvents(this, deletedEvent);
        }

        public void Handle(GetCustomerQuery getCustomerQuery)
        {
            if (OnCustomerEvents != null)
                OnCustomerEvents(this, getCustomerQuery);
        }

        public void Handle(GetCustomersQuery getCustomersQuery)
        {
            if (OnCustomerEvents != null)
                OnCustomerEvents(this, getCustomersQuery);
        }

        public void Handle(GetCustomerCountQuery getCustomersCount)
        {
            if (OnCustomerEvents != null)
                OnCustomerEvents(this, getCustomersCount);
        }
    }
}
