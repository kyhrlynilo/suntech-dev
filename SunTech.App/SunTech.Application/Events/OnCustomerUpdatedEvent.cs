using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Events
{
    public class OnCustomerUpdatedEvent: CustomerEvent
    {
        public string id { get; set; }
        public OnCustomerUpdatedEvent(string id, string firstName, string lastName, DateTimeOffset birthday, string email)
        {
            this.id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Email = email;
        }
    }
}
