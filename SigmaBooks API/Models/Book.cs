using System;
using System.Xml.Serialization;

namespace SigmaBooks_API.Models
{
    /// <summary>
    ///     XML-link Object Book.
    /// </summary>
    public class Book
    {
        /// <summary>
        ///     Property XML linked to book's id attribute.
        /// </summary>
        [XmlAttribute("id")]
        public string id { get; set; }

        /// <summary>
        ///     Property XML linked to book's author element.
        /// </summary>
        [XmlElement("author")]
        public string author { get; set; }

        /// <summary>
        ///     Property XML linked to book's title element.
        /// </summary>
        [XmlElement("title")]
        public string title { get; set; }

        /// <summary>
        ///     Property XML linked to book's genre element.
        /// </summary>
        [XmlElement("genre")]
        public string genre { get; set; }

        /// <summary>
        ///     Property XML linked to book's price element.
        /// </summary>
        [XmlElement("price")]
        public float price { get; set; }

        /// <summary>
        ///     Secret DateTime Object that also includes time.
        /// </summary>
        [XmlIgnore]
        public DateTime secret_publish_date { get; set; }

        /// <summary>
        ///     Dummy Property XML linked to book's publish_date element. This is used to prevent writing/reading a 00:00:00 time
        ///     on XML values with no timestamp.
        /// </summary>
        [XmlElement("publish_date")]
        public string publish_date
        {
            get { return secret_publish_date.ToString("yyyy-MM-dd"); }
            set { secret_publish_date = DateTime.Parse(value); }
        }

        /// <summary>
        ///     Property XML linked to book's description element.
        /// </summary>
        [XmlElement("description")]
        public string description { get; set; }
    }
}