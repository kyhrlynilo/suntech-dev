using SunTech.Application.EventHandlers;
using SunTech.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Customers.Commands
{
    public class CreateCustomerCommand
    {
        private readonly ICustomerEventBroker _ceHandler;

        public CreateCustomerCommand(ICustomerEventBroker customerEventHandler,
                string firstName,
                string lastName,
                DateTimeOffset birthday,
                string email
            )
        {
            _ceHandler = customerEventHandler;

            _ceHandler.Handle(new Events.OnCustomerCreatedEvent(firstName, lastName, birthday, email));
        }
        
    }
}
