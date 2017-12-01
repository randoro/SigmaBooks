using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SigmaBooks_API.Models
{
    [XmlRoot("catalog")]
    public class BookCatalog
    {
        [XmlElement("book")]
        public List<Book> bookList = new List<Book>();
    }
}