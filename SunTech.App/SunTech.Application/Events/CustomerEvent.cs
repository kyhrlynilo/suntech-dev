using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Application.Events
{
    public class CustomerEvent: EventArgs
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string Email { get; set; }
    }
}
