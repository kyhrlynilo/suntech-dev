using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.Domain.Customer
{
    public class CustomerSummary
    {
        public string id { get; set; }
        public int CustomerCount { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
