using HeatChart.Data.Sql;
using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using HeatChart.DataRepository.Sql.Extensions;
using System.Data.Entity.Infrastructure;

namespace HeatChart.DataRepository.Sql.EFRepository
{
    public class EFRepository<TEntity> : IEFRepository<TEntity> where TEntity : class
    {
        private bool _disposed;
        private readonly DbSet<TEntity> _dbSet;

        protected readonly DbContext Context;
        protected readonly IEFUnitOfWork UnitOfWork;

        public EFRepository(IEFUnitOfWork uow)
        {
            //if (uow == null) throw new ArgumentNullException("uow");

            UnitOfWork = uow;
            Context = uow?.Context;
            _dbSet = Context.Set<TEntity>();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] include)
        {
            return (include != null)
                       ? Context.Set<TEntity>().IncludeMultiple(include)
                       : Context.Set<TEntity>();
        }

        public TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] include)
        {
            return (include != null)
                       ? Context.Set<TEntity>().IncludeMultiple(include).Where(where).SingleOrDefault()
                       : Context.Set<TEntity>().Where(where).SingleOrDefault();
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] include)
        {
            return (include != null)
                       ? Context.Set<TEntity>().IncludeMultiple(include).Where(where).FirstOrDefault()
                       : Context.Set<TEntity>().Where(where).FirstOrDefault();
        }

        public virtual void Update(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = Context.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Modified;

            //Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void InsertAll(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void Commit()
        {
            UnitOfWork.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    UnitOfWork.Dispose();
                }
            }
            _disposed = true;
        }

    }
}
