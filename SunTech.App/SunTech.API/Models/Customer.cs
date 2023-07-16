using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.API.Models
{
    public class Customer
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string Email { get; set; }
    }
}
