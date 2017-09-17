using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class SpecificationsConfiguration : EntityBaseConfiguration<Specifications>
    {
        public SpecificationsConfiguration()
        {
            Property(spec => spec.Name).IsRequired().HasMaxLength(50);
            Property(spec => spec.Description).IsOptional();

            Property(spec => spec.CreatedBy).IsRequired().HasMaxLength(50);
            Property(spec => spec.CreatedOn).IsRequired();
            Property(spec => spec.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(spec => spec.ModifiedOn).IsRequired();

            Property(spec => spec.IsDeleted).IsOptional();
        }
    }
}
