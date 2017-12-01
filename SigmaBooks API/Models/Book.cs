using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SigmaBooks_API.Models
{
    public class Book
    {
        public string id { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public decimal price { get; set; }
        public DateTime publish_date { get; set; }
        public string description { get; set; }
    }
}