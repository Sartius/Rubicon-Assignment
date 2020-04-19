using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
    }
}
