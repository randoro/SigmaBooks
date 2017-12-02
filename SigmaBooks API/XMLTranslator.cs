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
            // Creates an instance of the XmlSerializer class;  
            // specifies the type of object to be deserialized.  

            // If the XML document has been altered with unknown   
            // nodes or attributes, handles them with the   
            // UnknownNode and UnknownAttribute events.  
            //serializer.UnknownNode += new
            //XmlNodeEventHandler(serializer_UnknownNode);
            //serializer.UnknownAttribute += new
            //XmlAttributeEventHandler(serializer_UnknownAttribute);

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
    }
}