using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql.Interfaces;
using HeatChart.Infrastructure.Dependency.Extensions;
using System.Net.Http;

namespace HeatChart.Infrastructure.Dependency.Core
{
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public IEFRepository<TEntity> GetDataRepository<TEntity>(HttpRequestMessage request) where TEntity : class, IEntityBase, new()
        {
            return request.GetDataRepository<TEntity>();
        }
    }
    public interface IDataRepositoryFactory
    {
        IEFRepository<TEntity> GetDataRepository<TEntity>(HttpRequestMessage request) where TEntity : class, IEntityBase, new();
    }
}
