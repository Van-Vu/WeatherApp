using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WeatherApp.Model;

namespace WeatherApp.Data.Xml
{
    /// <summary>
    /// DataSource which read data from XML file then deserialize it to object
    /// </summary>
    public class XmlDataSource: IDataSource
    {
        private string _fileLocation;

        /// <summary>
        /// Constructor needs a file name
        /// </summary>
        /// <param name="fileLocation"></param>
        public XmlDataSource(string fileLocation)
        {
            _fileLocation = fileLocation;
        }

        /// <summary>
        /// Read xml content from a file and deserialize it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T BuildDataSource<T>()
        {
            var xmlString = ReadXmlFileContent();
            return DeserializeToObject<T>(xmlString);
        }

        private string ReadXmlFileContent()
        {
            var doc = new XmlDocument();
            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), _fileLocation);
            doc.Load(fileName);

            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    doc.WriteTo(xmlTextWriter);
                }
                return stringWriter.ToString();
            }            
        }

        private T DeserializeToObject<T>(string xml)
        {
            var xs = new XmlSerializer(typeof(T));
            var obj = xs.Deserialize(new StringReader(xml));

            return (T)obj;
        }
    }
}
