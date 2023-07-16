using SunTech.Application.Customers.Queries;
using SunTech.Application.CustomersSummary.Queries;
using SunTech.Application.Events;
using System;

namespace SunTech.Application.EventHandlers
{
    public interface ICustomerEventBroker
    {
        event EventHandler<CustomerEvent> OnCustomerEvents;

        void Handle(GetCustomerCountQuery getCustomerQuery);
        void Handle(GetCustomersQuery getCustomerQuery);
        void Handle(GetCustomerQuery getCustomerQuery);
        void Handle(OnCustomerCreatedEvent createdEvent);
        void Handle(OnCustomerDeletedEvent deletedEvent);
        void Handle(OnCustomerUpdatedEvent updatedEvent);
        
    }
}