using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WeatherApp.Data
{
    /// <summary>
    /// http://huyrua.wordpress.com/2012/09/16/entity-framework-poco-repository-and-specification-pattern-upgraded-to-ef-5/
    /// </summary>
    public class Specification<T> : ISpecification<T>
    {
        public Specification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Specification<T> And(Specification<T> specification)
        {
            return new Specification<T>(this.Predicate.And(specification.Predicate));
        }

        public Specification<T> And(Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>(this.Predicate.And(predicate));
        }

        public Specification<T> Or(Specification<T> specification)
        {
            return new Specification<T>(this.Predicate.Or(specification.Predicate));
        }

        public Specification<T> Or(Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>(this.Predicate.Or(predicate));
        }

        public T SatisfyingEntityFrom(IQueryable<T> query)
        {
            return query.Where(Predicate).SingleOrDefault();
        }

        public IEnumerable<T> SatisfyingEntitiesFrom(IQueryable<T> query)
        {
            return query.Where(Predicate);
        }

        public Expression<Func<T, bool>> Predicate;
    }
}
