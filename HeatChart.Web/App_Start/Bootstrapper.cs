using HeatChart.Infrastructure.Dependency;
using ModelMapper;
using System.Reflection;
using System.Web.Http;

namespace HeatChart.Web
{
    public class Bootstrapper
    {
        public static void RegisterComponents()
        {
            //Configure dependency using Autofac
            AutofacWebapiConfig.RegisterDependency(GlobalConfiguration.Configuration, Assembly.GetExecutingAssembly());

            //Configure Auto Mapper
            AutoMapperConfiguration.Configure();
        }
    }
}
