using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WeatherApp.Model
{
    [Serializable]
    public class Observations
    {
        [XmlElement(ElementName = "Observation")]
        public Observation[] Observation { get; set; }
    }
}
