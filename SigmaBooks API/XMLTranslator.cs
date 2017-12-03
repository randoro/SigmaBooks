using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace SigmaBooks_API
{
    /// <summary>
    ///     Generic Translator class for all XML files.
    /// </summary>
    public class XMLTranslator
    {
        /// <summary>
        ///     Serialize value and write it to an XML file at fullpath location.
        /// </summary>
        /// <typeparam name="T">Type of object serialized.</typeparam>
        /// <param name="fullPath">Path and filename to create the new XML file.</param>
        /// <param name="value">Value to serialize.</param>
        /// <returns>returns true if success.</returns>
        public static bool Serialize<T>(string fullPath, T value)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                TextWriter writer = new StreamWriter(fullPath);
                serializer.UnknownNode += UnknownNode;
                serializer.UnknownAttribute += UnknownAttribute;

                serializer.Serialize(writer, value);
                writer.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Deserialize file from fullpath and create object T from it.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="fullPath">Path and filename of the XML file being deserialized.</param>
        /// <param name="success">out bool for success.</param>
        /// <returns>Returns the created object of type T or null if failed.</returns>
        public static T Deserialize<T>(string fullPath, out bool success) where T : class
        {
            try
            {
                var deserializer = new XmlSerializer(typeof(T));
                TextReader reader = new StreamReader(fullPath);
                deserializer.UnknownNode += UnknownNode;
                deserializer.UnknownAttribute += UnknownAttribute;

                var obj = deserializer.Deserialize(reader);
                reader.Close();
                var xmlData = (T) obj;
                success = true;
                return xmlData;
            }
            catch (Exception)
            {
                success = false;
                return null;
            }
        }

        /// <summary>
        ///     Event raised when unknown XML node is found in file/objects.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Debug.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        /// <summary>
        ///     Event raised when unknown XML attribute is found in file/objects.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            var attr = e.Attr;
            Debug.WriteLine("Unknown attribute " +
                            attr.Name + "='" + attr.Value + "'");
        }
    }
}