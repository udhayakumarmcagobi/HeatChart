using HeatChart.Entities.Sql;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HeatChart.Core.Test
{
    public class TestHeatChartContext : DbContext
    {
        public TestHeatChartContext()
            : base("Name=HeatChartContext")
        {

        }
        public TestHeatChartContext(bool enableLazyLoading, bool enableProxyCreation)
            : base("Name=HeatChartContext")
        {
            Configuration.ProxyCreationEnabled = enableProxyCreation;
            Configuration.LazyLoadingEnabled = enableLazyLoading;
        }
        public TestHeatChartContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<MaterialRegisterHeader> materialRegisterHeaders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Suppress code first model migration check          
            Database.SetInitializer<TestHeatChartContext>(new AlwaysCreateInitializer());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public void Seed(TestHeatChartContext Context)
        {
            //var listCountry = new List<Country>() {
            // new Country() { Id = 1, Name = "US" },
            // new Country() { Id = 2, Name = "India" },
            // new Country() { Id = 3, Name = "Russia" }
            //};
            //Context.Countries.AddRange(listCountry);
            //Context.SaveChanges();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<TestHeatChartContext>
        {
            protected override void Seed(TestHeatChartContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<TestHeatChartContext>
        {
            protected override void Seed(TestHeatChartContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class AlwaysCreateInitializer : DropCreateDatabaseAlways<TestHeatChartContext>
        {
            protected override void Seed(TestHeatChartContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }


    }

}
