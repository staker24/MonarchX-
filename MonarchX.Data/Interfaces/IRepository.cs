
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MonarchX.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        //bool Exists(Guid id);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        (bool Saved, int SavedCount, string SavedMessage) Save();
    }
}