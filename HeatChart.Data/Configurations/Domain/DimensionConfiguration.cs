using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class DimensionConfiguration : EntityBaseConfiguration<Dimension>
    {
        public DimensionConfiguration()
        {
            Property(dim => dim.Name).IsRequired().HasMaxLength(50);
            Property(dim => dim.Description).IsOptional();

            Property(dim => dim.CreatedBy).IsRequired().HasMaxLength(50);
            Property(dim => dim.CreatedOn).IsRequired();
            Property(dim => dim.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(dim => dim.ModifiedOn).IsRequired();

            Property(dim => dim.IsDeleted).IsOptional();
        }
    }
}
