using SunTech.Application.EventHandlers;
using SunTech.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Customers.Commands
{
    public class DeleteCustomerCommand
    {
        private readonly ICustomerEventBroker _ceHandler;
        public DeleteCustomerCommand(ICustomerEventBroker customerEventHandler, string id)
        {
            _ceHandler = customerEventHandler;

            _ceHandler.Handle(new Events.OnCustomerDeletedEvent(id));
        }
    }
}
