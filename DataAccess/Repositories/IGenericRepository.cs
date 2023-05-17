using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        ///     Get all items
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get();

        /// <summary>
        ///     Get qualifying items
        /// </summary>
        IQueryable<T> Get(Expression<Func<T, bool>> filter);

        /// <summary>
        ///     Get qualifying items in order specified
        /// </summary>
        IQueryable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        /// <summary>
        ///     Get qualifying items in order specified including extra fields
        /// </summary>
        IQueryable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includes);

        /// <summary>
        ///     Get item by key
        /// </summary>
        T GetById(object id);

        /// <summary>
        ///     Insert a new item
        /// </summary>
        EntityEntry<T> Insert(T entityToInsert);

        /// <summary>
        ///     Delete an item by its key
        /// </summary>
        void Delete(object id);

        /// <summary>
        ///     Delete the passed item
        /// </summary>
        void Delete(T entityToDelete);

        /// <summary>
        ///     Update the passed item
        /// </summary>
        void Update(T entityToUpdate);
    }
}
