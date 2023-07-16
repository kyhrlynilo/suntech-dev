using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Events
{
    public class OnCustomerDeletedEvent: CustomerEvent
    {
        public string id { get; set; }
        public OnCustomerDeletedEvent(string id)
        {
            this.id = id;
        }
    }
}
