using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.DataRepository.Sql.Interfaces
{
    public interface IEFRepository : IDisposable
    {
    }

    public interface IEFRepository<TEntity> : IEFRepository
    {
        IQueryable<TEntity> AsQueryable();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] include);
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] include);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] include);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        void InsertAll(List<TEntity> entities);
        void Update(TEntity entity);
        void Commit();
    }
}
