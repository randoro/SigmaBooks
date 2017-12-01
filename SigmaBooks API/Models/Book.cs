using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SigmaBooks_API.Models
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string author { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string genre { get; set; }
        [DataMember]
        public float price { get; set; }
        [DataMember]
        public DateTime publish_date { get; set; }
        [DataMember]
        public string description { get; set; }
    }
}