using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzFunctionReferenceSolution.Models
{
    public class BusinessEvent
    {
        public string? Topic { get; set; }
        public string? Subject { get; set; }
        public string? EventType { get; set; }
        public DateTimeOffset EventTime { get; set; }
        public IDictionary<string, object>? Data { get; set; }
    }
}
