using SunTech.Application.EventHandlers;
using SunTech.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Customers.Commands
{
    public class UpdateCustomerCommand
    {
        private readonly ICustomerEventBroker _ceHandler;
        public UpdateCustomerCommand(ICustomerEventBroker customerEventHandler,
                string id,
                string firstName,
                string lastName,
                DateTimeOffset birthday,
                string email
            )
        {
            _ceHandler = customerEventHandler;

            _ceHandler.Handle(new Events.OnCustomerUpdatedEvent(id, firstName, lastName, birthday, email));
        }
    }
}
