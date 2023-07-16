using System;
using System.Collections.Generic;
using System.Text;

namespace SunTech.API.Listener.Models
{
    public class CustomerEventGridMessage
    {
        public string Subject { get; set; }
        public string Verb { get; set; }
        public dynamic Data { get; set; }
    }
}
