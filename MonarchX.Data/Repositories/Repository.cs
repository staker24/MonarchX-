using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MonarchX.Data;

namespace MonarchX.Data
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext DbContext;

        public Repository(DbContext context)
        {
            DbContext = context;
        }
        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate);
        }
        public TEntity Get(Guid id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().ToList();
        }
        public void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().RemoveRange(entities);
        }

        public (bool Saved, int SavedCount, string SavedMessage) Save()
        {
            var isOk = false;
            var count = 0;
            var message = "";

            try
            {
                count = DbContext.SaveChanges();
                isOk = true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                if (ex.Message.Contains("See the inner exception for details.") && ex.InnerException?.Message != null)
                {
                    if (ex.InnerException.Message.StartsWith("Cannot insert duplicate key row"))
                    {
                        message = "Object already exists.";
                    }
                    else
                    {
                        message = ex.InnerException.Message;
                    }
                }
            }

            return (isOk, count, message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~Repository()
        {
            Dispose(false);
        }
        protected void Dispose(Boolean disposing)
        {
            // free unmanaged ressources here
            if (disposing)
            {
                // This method is called from Dispose() so it is safe to
                // free managed ressources here
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }
    }
}