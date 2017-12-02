using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace SigmaBooks_API.Models
{
    /// <summary>
    /// XML-link Object Book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Property XML linked to book's id attribute.
        /// </summary>
        [XmlAttribute("id")]
        public string id { get; set; }

        /// <summary>
        /// Property XML linked to book's author element.
        /// </summary>
        [XmlElement("author")]
        public string author { get; set; }

        /// <summary>
        /// Property XML linked to book's title element.
        /// </summary>
        [XmlElement("title")]
        public string title { get; set; }

        /// <summary>
        /// Property XML linked to book's genre element.
        /// </summary>
        [XmlElement("genre")]
        public string genre { get; set; }

        /// <summary>
        /// Property XML linked to book's price element.
        /// </summary>
        [XmlElement("price")]
        public float price { get; set; }

        /// <summary>
        /// Property XML linked to book's publish_date element.
        /// </summary>
        [XmlElement("publish_date")]
        public DateTime publish_date { get; set; }

        /// <summary>
        /// Property XML linked to book's description element.
        /// </summary>
        [XmlElement("description")]
        public string description { get; set; }
    }
}