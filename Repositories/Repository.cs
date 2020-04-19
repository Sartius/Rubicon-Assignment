using BlogEFModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class Repository<T> : IRepository<T>
            where T : class
    {
        //protected IObjectSet<T> _objectSet;
        protected DbSet<T> _dbSet;
        public Repository(BlogDatabaseContext context)
        {
            //_objectSet = context.CreateObjectSet<T>();
            _dbSet = context.Set<T>();
        }
        #region IRepository<T> Members
        public IEnumerable<T> GetAll()
        {
            //return _objectSet;
            return _dbSet;
        }
        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            //return _objectSet.Where(filter);
            return _dbSet.Where(filter);
        }
        public void Add(T entity)
        {
            //_objectSet.AddObject(entity);
            _dbSet.Add(entity);
        }
        public void Remove(T entity)
        {
            //_objectSet.DeleteObject(entity);
            _dbSet.Remove(entity);
        }
        #endregion
    }
}
