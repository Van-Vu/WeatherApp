using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class WeatherDetailViewModel
    {
        [Display(Name = "Station Name")]
        public string StationName { get; set; }

        [Display(Name="Lowest Temperature")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0000}")]
        public decimal LowestTemperature { get; set; }

        [Display(Name = "Time")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss}")]
        public DateTime TimeOfLowestTemperature { get; set; }

        [Display(Name = "Highest Temperature")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0000}")]
        public decimal HighestTemperature { get; set; }

        [Display(Name = "Time")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm:ss}")]
        public DateTime TimeOfHighestTemperature { get; set; }

        [Display(Name = "Mean Temperature")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0000}")]
        public decimal MeanTemperature { get; set; }

        public IEnumerable<Observation> History { get; set; }
    }
}