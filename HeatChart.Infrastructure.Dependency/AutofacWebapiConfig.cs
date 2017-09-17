using Autofac;
using Autofac.Integration.WebApi;
using HeatChart.Data.Sql;
using HeatChart.DataRepository.Sql.EFRepository;
using HeatChart.DataRepository.Sql.Infrastructure;
using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.DataRepository.Sql.UnitOfWork;
using HeatChart.Domain.Core.Abstracts;
using HeatChart.Domain.Core.Services;
using HeatChart.Infrastructure.Common.Utilities;
using HeatChart.Infrastructure.Dependency.Core;
using System;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace HeatChart.Infrastructure.Dependency
{
    public class AutofacWebapiConfig
    {
        public static IContainer container;
        
        public static void RegisterDependency(HttpConfiguration config, Assembly executingAssembly)
        {
            Initialize(config, RegisterServices(new ContainerBuilder(), executingAssembly));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder containerBuilder, Assembly executingAssembly)
        {
            try
            {
                containerBuilder.RegisterApiControllers(executingAssembly);

                containerBuilder.RegisterType<HeatChartContext>()
                    .As<DbContext>()
                    .InstancePerRequest();

                containerBuilder.RegisterType<DBFactory>()
                    .As<IDBFactory>()
                    .InstancePerRequest();

                containerBuilder.RegisterType<EFUnitOfWork>()
                    .As<IEFUnitOfWork>()
                    .InstancePerRequest();

                containerBuilder.RegisterType<EFUnitOfWork>()
                    .As<IUnitOfWork>()
                    .InstancePerRequest();

                containerBuilder.RegisterGeneric(typeof(EFRepository<>))
                    .As(typeof(IEFRepository<>))
                    .InstancePerRequest();

                containerBuilder.RegisterType<DataRepositoryFactory>()
                    .As<IDataRepositoryFactory>()
                    .InstancePerRequest();

                containerBuilder.RegisterType<MembershipService>()
                    .As<IMembershipService>()
                    .InstancePerRequest();

                containerBuilder.RegisterType<EncryptionService>()
                    .As<IEncryptionService>()
                    .InstancePerRequest();

                container = containerBuilder.Build();


                return container;
            }
            catch(Exception ex)
            {
                if (ConfigHelper.LogProviderRequests) ObjHelper.LogProviderRequest(ex, "UnityResolver");

                return null;
            }
        }
    }
}
