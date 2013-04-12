using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WeatherApp.Model;

namespace WeatherApp.Data.Xml
{
    /// <summary>
    /// Implementation of the repository
    /// </summary>
    public class WeatherRepository: IRepository<Observation>
    {
        private Observations _observations;
        private IDataSource _dataSource;

        public Observations Observations
        {
            get
            {
                if (_observations == null)
                {
                    _observations = _dataSource.BuildDataSource<Observations>();
                }

                return _observations;
            }
        }

        public WeatherRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Observation> GetAll()
        {
            return Observations.Observation.AsEnumerable();
        }

        public IEnumerable<Observation> GetAll<TOrderBy>(Func<Observation, TOrderBy> orderBy, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return Observations.Observation.OrderBy(orderBy).AsEnumerable();
            }
            return Observations.Observation.OrderByDescending(orderBy).AsEnumerable();
        }

        public IEnumerable<Observation> Get<TOrderBy>(Func<Observation, bool> criteria, Func<Observation, TOrderBy> orderBy, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return Observations.Observation.Where(criteria).OrderBy(orderBy).AsEnumerable();
            }
            return Observations.Observation.Where(criteria).OrderByDescending(orderBy).AsEnumerable();
        }

        public IEnumerable<Observation> Get(ISpecification<Observation> criteria)
        {
            if (criteria == null)
            {
                return GetAll();
            }

            return criteria.SatisfyingEntitiesFrom(GetAll().AsQueryable()).AsEnumerable();
        }
    }
}
