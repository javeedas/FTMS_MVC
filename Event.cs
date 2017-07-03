using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTMS.Models
{
    public class Event
    {
        public int eId { get; set; }
        public string eName { get; set; }
        public DateTime eDate { get; set; }
        public Decimal eBudget { get; set; }
        public string eStatus { get; set; }
    }
}