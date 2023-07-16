using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Events
{
    public class OnCustomerCreatedEvent: CustomerEvent
    {
        public OnCustomerCreatedEvent(string firstName, string lastName, DateTimeOffset birthday, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Email = email;
        }
        
    }
}
