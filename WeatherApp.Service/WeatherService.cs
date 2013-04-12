using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Data;
using WeatherApp.Model;

namespace WeatherApp.Service
{
    public class WeatherService : IWeatherService
    {
        private IRepository<Observation> _repository;

        public WeatherService(IRepository<Observation> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get latest recorded data of each station
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Observation> GetLatestData()
        {
            var weathers = _repository.GetAll(x => x.StationName);

            var groupedWeather = from w in weathers
                                group w by w.StationName into g
                                let date = g.Max(p => p.DateTime)
                                select new { latestDate = g.Where(p => p.DateTime == date) };
            return groupedWeather.ToList().Select(item => item.latestDate.FirstOrDefault());
        }

        /// <summary>
        /// Get all data related to a station
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public IEnumerable<Observation> GetStationData(string stationName)
        {
            if (string.IsNullOrWhiteSpace(stationName)) return null;

            return _repository.Get(x => x.StationName == stationName, x => x.DateTime);
        }

        /// <summary>
        /// Search a list of stations based on conditions supplied
        /// </summary>
        /// <param name="searchConditions"></param>
        /// <returns></returns>
        public IEnumerable<Observation> SearchStation(SearchConditions searchConditions)
        {
            if (searchConditions == null) return null;

            Specification<Observation> specification = null;
            if (!string.IsNullOrEmpty(searchConditions.SearchString))
            {
                specification = new StationNameContainsText(searchConditions.SearchString);
            }

            if (searchConditions.FromDate != DateTime.MinValue)
            {
                specification = specification != null ? specification.And(new WeatherDataRecordedFromDate(searchConditions.FromDate)) : new WeatherDataRecordedFromDate(searchConditions.FromDate);
            }

            if (searchConditions.ToDate != DateTime.MinValue)
            {
                specification = specification != null ? specification.And(new WeatherDataRecordedToDate(searchConditions.ToDate)) : new WeatherDataRecordedToDate(searchConditions.ToDate);
            }

            if (specification == null)
            {
                return _repository.GetAll(x => x.StationName);
            }

            return _repository.Get(specification);    
        }
    }
}
