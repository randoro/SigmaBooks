using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace SigmaBooks_API.Models
{
    public class Book
    {
        [XmlAttribute("id")]
        public string id { get; set; }
        [XmlElement("author")]
        public string author { get; set; }
        [XmlElement("title")]
        public string title { get; set; }
        [XmlElement("genre")]
        public string genre { get; set; }
        [XmlElement("price")]
        public float price { get; set; }
        [XmlElement("publish_date")]
        public DateTime publish_date { get; set; }
        [XmlElement("description")]
        public string description { get; set; }
    }
}