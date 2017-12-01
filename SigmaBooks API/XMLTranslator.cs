using SigmaBooks_API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SigmaBooks_API
{
    public class XMLTranslator
    {
        string Serialize<T>(MediaTypeFormatter formatter, T value)
        {
            // Create a dummy HTTP Content.
            Stream stream = new MemoryStream();
            var content = new StreamContent(stream);
            /// Serialize the object.
            formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();
            // Read the serialized string.
            stream.Position = 0;
            return content.ReadAsStringAsync().Result;
        }

        T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
        {
            // Write the serialized string to a memory stream.
            Stream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            // Deserialize to an object of type T
            return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
        }

        // Example of use
        Book TestSerialization(Book book)
        {

            var xml = new XmlMediaTypeFormatter();
            string str = Serialize(xml, book);

            //var json = new JsonMediaTypeFormatter();
            //str = Serialize(json, book);

            // Round trip
            Book person2 = Deserialize<Book>(xml, str);
            return person2;
        }

        //public Book[] TestAll()
        //{
        //    Book[] products = new Book[3];
        //    products[0] = TestSerialization(new Book { id = "test", author = "J.K Rowlings", title = "Harry potter", genre = "Magic", price = 10, publish_date = DateTime.Now, description = "a book" });
        //    products[1] = TestSerialization(new Book { id = "test2", author = "J.K Rowlings", title = "Harry potter 2", genre = "Magic", price = 10, publish_date = DateTime.Now, description = "a book" });
        //    products[2] = TestSerialization(new Book { id = "test3", author = "J.K Rowlings", title = "Harry potter 3", genre = "Magic", price = 10, publish_date = DateTime.Now, description = "a book" });
        //    return products;
        //}

        public List<Book> TestAll()
        {
            System.Diagnostics.Debug.WriteLine("Hello");
            List <Book> employees = new List<Book>();
            try
            {
                XDocument doc = XDocument.Load("/books.xml");
                System.Diagnostics.Debug.WriteLine("Root:"+doc.Root.ToString());


                foreach (XElement element in doc.Root.Elements())//.Elements("book"))//doc.XPathSelectElements("/catalog/book"))//doc.Descendants("catalog").Descendants("book"))
                {
                    System.Diagnostics.Debug.WriteLine("Starting on element " + element.Name);
                    System.Diagnostics.Debug.WriteLine(element.Attribute("id").Value);
                    Book employee = new Book();
                    employee.id = element.Attribute("id").Value;
                    employee.author = element.Element("author").Value;
                    employee.title = element.Element("title").Value;
                    employee.genre = element.Element("genre").Value;
                    System.Diagnostics.Debug.WriteLine("Before Price");
                    float outPrice = 0.0f;
                    float.TryParse(element.Element("price").Value, NumberStyles.Any, CultureInfo.InvariantCulture, out outPrice);
                    System.Diagnostics.Debug.WriteLine("Price: " + element.Element("price").Value);
                    employee.price = outPrice;
                    employee.publish_date = DateTime.Now;
                    employee.description = element.Element("description").Value;
                    employees.Add(employee);
                    
                }
                // Process the file
            }
            catch (FileNotFoundException e)
            {
                // File does not exist - handle the error
                System.Diagnostics.Debug.WriteLine("Error: " + e.FileName);
            }
            
            return employees;
        }

        //void LoadFile()
        //{
        //    var xml = new XmlMediaTypeFormatter();

        //    XmlDocument xmldoc = new XmlDocument();
        //    XmlNodeList xmlnode;
        //    int i = 0;
        //    string str = null;
        //    FileStream fs = new FileStream("c:\\books.xml", FileMode.Open, FileAccess.Read);
        //    xmldoc.Load(fs);
        //    xmlnode = xmldoc.GetElementsByTagName("book");
        //    for (i = 0; i <= xmlnode.Count - 1; i++)
        //    {
        //        xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
        //        str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
        //        MessageBox.Show(str);
        //    }
        //}
    }
}