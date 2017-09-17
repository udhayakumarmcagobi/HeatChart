using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class TestConfiguration : EntityBaseConfiguration<Test>
    {
        public TestConfiguration()
        {
            Property(test => test.Name).IsRequired().HasMaxLength(50);
            Property(test => test.Description).IsOptional();

            Property(test => test.CreatedBy).IsRequired().HasMaxLength(50);
            Property(test => test.CreatedOn).IsRequired();
            Property(test => test.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(test => test.ModifiedOn).IsRequired();

            Property(test => test.IsDeleted).IsOptional();
        }
    }
}
