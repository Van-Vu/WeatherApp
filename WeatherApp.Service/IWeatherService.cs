using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Model;

namespace WeatherApp.Service
{
    public interface IWeatherService
    {
        /// <summary>
        /// For task 1
        /// </summary>
        /// <returns></returns>
        IEnumerable<Observation> GetLatestData();

        /// <summary>
        /// For task 2
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        IEnumerable<Observation> GetStationData(string stationName);

        /// <summary>
        ///  For task 3
        /// </summary>
        /// <param name="searchConditions"></param>
        /// <returns></returns>
        IEnumerable<Observation> SearchStation(SearchConditions searchConditions);
    }
}
