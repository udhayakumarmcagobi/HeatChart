using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Domain.Core.Abstracts;
using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace HeatChart.Infrastructure.Dependency.Extensions
{
    public static class RequestMessageExtensions
    {
        internal static IMembershipService GetMembershipService(this HttpRequestMessage request)
        {
            return request.GetService<IMembershipService>();
        }

        private static TService GetService<TService>(this HttpRequestMessage request)
        {
            IDependencyScope dependencyScope = request.GetDependencyScope();
            TService service = (TService)dependencyScope.GetService(typeof(TService));

            return service;
        }

        internal static IEFRepository<TEntity> GetDataRepository<TEntity>(this HttpRequestMessage request) where TEntity : class, IEntityBase, new()
        {
            return request.GetService<IEFRepository<TEntity>>();
        }
    }
}
