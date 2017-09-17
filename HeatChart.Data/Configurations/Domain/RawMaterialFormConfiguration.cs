using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class RawMaterialFormConfiguration : EntityBaseConfiguration<RawMaterialForm>
    {
        public RawMaterialFormConfiguration()
        {
            Property(rmf => rmf.Name).IsRequired().HasMaxLength(50);
            Property(rmf => rmf.Description).IsOptional();

            Property(rmf => rmf.CreatedBy).IsRequired().HasMaxLength(50);
            Property(rmf => rmf.CreatedOn).IsRequired();
            Property(rmf => rmf.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(rmf => rmf.ModifiedOn).IsRequired();

            Property(rmf => rmf.IsDeleted).IsOptional();
        }
    }
}
