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
using System.Xml.Serialization;
using System.Xml.XPath;

namespace SigmaBooks_API
{
    public class XMLTranslator
    {
        public static bool Serialize<T>(string fullPath, T value)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                TextWriter writer = new StreamWriter(fullPath);
                serializer.Serialize(writer, value);
                writer.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static T Deserialize<T>(string fullPath, out bool success) where T : class
        {
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(T));
                TextReader reader = new StreamReader(fullPath);
                object obj = deserializer.Deserialize(reader);
                reader.Close();
                T xmlData = (T)obj;
                success = true;
                return xmlData;
            }
            catch(Exception)
            {
                success = false;
                return null;
            }
        }
        

        

        public IEnumerable<Book> TestAll()
        {
            // Creates an instance of the XmlSerializer class;  
            // specifies the type of object to be deserialized.  
           
            // If the XML document has been altered with unknown   
            // nodes or attributes, handles them with the   
            // UnknownNode and UnknownAttribute events.  
            //serializer.UnknownNode += new
            //XmlNodeEventHandler(serializer_UnknownNode);
            //serializer.UnknownAttribute += new
            //XmlAttributeEventHandler(serializer_UnknownAttribute);

            string filename = HttpContext.Current.Server.MapPath("~/App_Data/books.xml");
            //string filename = @"C:\\books.xml";
            //string filename = "/books.xml";
            bool success = false;

            BookCatalog XmlData = Deserialize<BookCatalog>(filename, out success);

            //string clone = HttpContext.Current.Server.MapPath("~/App_Data/clone.xml");

            //bool anothersuccess = Serialize<BookCatalog>(clone, XmlData);





            //XmlSerializer deserializer = new XmlSerializer(typeof(BookCatalog));
            //TextReader reader = new StreamReader(filename);
            //object obj = deserializer.Deserialize(reader);
            //BookCatalog XmlData = (BookCatalog)obj;
            //reader.Close();






            // A FileStream is needed to read the XML document.  

            //FileStream fs = new FileStream(filename, FileMode.Open);
            //// Declares an object variable of the type to be deserialized.  
            //List<Book> po;
            //// Uses the Deserialize method to restore the object's state   
            //// with data from the XML document. */  
            //po = (List<Book>)serializer.Deserialize(fs);
            // Reads the order date.  
            //Console.WriteLine("OrderDate: " + po.OrderDate);

            // Reads the shipping address.  
            //Address shipTo = po.ShipTo;
            //ReadAddress(shipTo, "Ship To:");
            // Reads the list of ordered items.  
            //OrderedItem[] items = po.OrderedItems;
            //Console.WriteLine("Items to be shipped:");
            //foreach (OrderedItem oi in items)
            //{
            //    Console.WriteLine("\t" +
            //    oi.ItemName + "\t" +
            //    oi.Description + "\t" +
            //    oi.UnitPrice + "\t" +
            //    oi.Quantity + "\t" +
            //    oi.LineTotal);
            //}
            // Reads the subtotal, shipping cost, and total cost.  
            //Console.WriteLine(
            //"\n\t\t\t\t\t Subtotal\t" + po.SubTotal +
            //"\n\t\t\t\t\t Shipping\t" + po.ShipCost +
            //"\n\t\t\t\t\t Total\t\t" + po.TotalCost
            //);
            return XmlData.bookList;
        }

        public List<Book> TestAllWorking()
        {
            //System.Diagnostics.Debug.WriteLine("Hello");
            List<Book> employees = new List<Book>();
            try
            {
                XDocument doc = XDocument.Load("/books.xml");
                //System.Diagnostics.Debug.WriteLine("Root:"+doc.Root.ToString());


                foreach (XElement element in doc.Root.Elements())//.Elements("book"))//doc.XPathSelectElements("/catalog/book"))//doc.Descendants("catalog").Descendants("book"))
                {
                    //System.Diagnostics.Debug.WriteLine("Starting on element " + element.Name);
                    //System.Diagnostics.Debug.WriteLine(element.Attribute("id").Value);
                    Book employee = new Book();
                    employee.id = element.Attribute("id").Value;
                    employee.author = element.Element("author").Value;
                    employee.title = element.Element("title").Value;
                    employee.genre = element.Element("genre").Value;
                    System.Diagnostics.Debug.WriteLine("Before Price");
                    float outPrice = 0.0f;
                    float.TryParse(element.Element("price").Value, NumberStyles.Any, CultureInfo.InvariantCulture, out outPrice);
                    //System.Diagnostics.Debug.WriteLine("Price: " + element.Element("price").Value);
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