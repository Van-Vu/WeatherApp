using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherApp.Model
{
    /// <summary>
    /// DTO for the search conditions
    /// </summary>
    public class SearchConditions
    {
        public string SearchString { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
