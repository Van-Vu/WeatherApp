using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Model;

namespace WeatherApp.Data
{
    /// <summary>
    /// Specification to get weather data recorded before a date
    /// </summary>
    public class WeatherDataRecordedToDate: Specification<Observation>
    {
        public WeatherDataRecordedToDate(DateTime toDate):
            base(w =>w.DateTime <= toDate)
        {}
    }
}
