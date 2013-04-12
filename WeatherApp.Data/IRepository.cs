using System;
using System.Collections.Generic;

namespace WeatherApp.Data
{
    public enum SortOrder { Ascending, Descending }

    public interface IRepository<T>
    {
        /// <summary>
        /// Gets all data.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get all by specified order.
        /// </summary>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<T> GetAll<TOrderBy>(Func<T, TOrderBy> orderBy, SortOrder sortOrder = SortOrder.Ascending);

        /// <summary>
        /// Gets by criteria with specified order.
        /// </summary>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="criteria"></param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<T> Get<TOrderBy>(Func<T, bool> criteria, Func<T, TOrderBy> orderBy, SortOrder sortOrder = SortOrder.Ascending);

        /// <summary>
        /// Gets by specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<T> Get(ISpecification<T> criteria);

    }
}
