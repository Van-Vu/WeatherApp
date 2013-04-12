using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherApp.Data
{
    /// <summary>
    /// http://huyrua.wordpress.com/2012/09/16/entity-framework-poco-repository-and-specification-pattern-upgraded-to-ef-5/
    /// </summary>
    public interface ISpecification<T>
    {
        T SatisfyingEntityFrom(IQueryable<T> query);

        IEnumerable<T> SatisfyingEntitiesFrom(IQueryable<T> query);
    }
}
