using System.Collections.Generic;
using System.Xml.Serialization;

namespace SigmaBooks_API.Models
{
    /// <summary>
    ///     XML-link Object Catalog.
    /// </summary>
    [XmlRoot("catalog")]
    public class BookCatalog
    {
        /// <summary>
        ///     Equivilent to ArrayOfBooks tag in generated XML.
        /// </summary>
        [XmlElement("book")] public List<Book> bookList = new List<Book>();
    }
}