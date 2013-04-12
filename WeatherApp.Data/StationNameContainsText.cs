using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Model;

namespace WeatherApp.Data
{
    /// <summary>
    /// Specification to get weather data which has station name contains the search text
    /// </summary>
    public class StationNameContainsText : Specification<Observation>
    {
        public StationNameContainsText(string searchText) : base(w => w.StationName.IndexOf(searchText,StringComparison.InvariantCultureIgnoreCase) >= 0) 
        {}
    }
}
