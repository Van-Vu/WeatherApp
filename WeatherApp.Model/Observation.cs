using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WeatherApp.Model
{
    [Serializable]
    public class Observation
    {
        [XmlElement(ElementName = "Station")]
        public string StationName { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Temperature { get; set; }
    }
}
