using HeatChart.Data.Sql.Configurations;
using HeatChart.Data.Sql.Configurations.Domain;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HeatChart.Data.Sql
{
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class HeatChartContext : DbContext
    {
        public HeatChartContext()
            : base("HeatChart")
        {
            Database.SetInitializer<HeatChartContext>(null);

            Database.CommandTimeout = 600;
        }

        #region Entity Sets
        public IDbSet<Users> UserSet { get; set; }
        public IDbSet<Role> RoleSet { get; set; }
        public IDbSet<UserRole> UserRoleSet { get; set; }
        public IDbSet<Customer> CustomerSet { get; set; }
        public IDbSet<Supplier> SupplierSet { get; set; }
        public IDbSet<ThirdPartyInspection> ThirdPartyInspectionSet { get; set; }
        public IDbSet<Test> TestSet { get; set; }
        public IDbSet<RawMaterialForm> RawMaterialFormSet { get; set; }
        public IDbSet<Error> ErrorSet { get; set; }
        public IDbSet<Specifications> SpecificationsSet { get; set; }
        public IDbSet<Dimension> DimensionSet { get; set; }
        public IDbSet<MaterialRegisterHeader> MaterialRegisterHeaderSet { get; set; }
        public IDbSet<MaterialRegisterSubSeries> MaterialRegisterSubSeriesSet { get; set; }
        public IDbSet<MillDetail> MillDetailSet { get; set; }
        public IDbSet<LabReport> LabReportSet { get; set; }
        public IDbSet<HeatChartHeader> HeatChartHeaderSet { get; set; }
        public IDbSet<HeatChartDetails> HeatChartDetailsSet { get; set; }

        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new SupplierConfiguration());
            modelBuilder.Configurations.Add(new ThirdPartyInspectionConfiguration());
            modelBuilder.Configurations.Add(new RawMaterialFormConfiguration());
            modelBuilder.Configurations.Add(new TestConfiguration());
            modelBuilder.Configurations.Add(new SpecificationsConfiguration());
            modelBuilder.Configurations.Add(new DimensionConfiguration());

            modelBuilder.Configurations.Add(new MaterialRegisterHeaderConfiguration());
            modelBuilder.Configurations.Add(new MaterialRegisterSubSeriesConfiguration());
            modelBuilder.Configurations.Add(new MillDetailConfiguration());
            modelBuilder.Configurations.Add(new LabReportConfiguration());
            modelBuilder.Configurations.Add(new HeatChartHeaderConfiguration());
            modelBuilder.Configurations.Add(new HeatChartDetailsConfiguration());
            modelBuilder.Configurations.Add(new HeatChartMaterialHeaderRelationshipConfiguration());
            modelBuilder.Configurations.Add(new MaterialRegisterSubseriesTestRelationshipConfiguration());

        }
    }
}
