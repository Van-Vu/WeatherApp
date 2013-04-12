using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Model;

namespace WeatherApp.Data
{
    /// <summary>
    /// Specification to get weather data recorded after a date
    /// </summary>
    public class WeatherDataRecordedFromDate: Specification<Observation>
    {
        public WeatherDataRecordedFromDate(DateTime fromDate):
            base(w => w.DateTime >= fromDate)
        {            
        }
    }
}
