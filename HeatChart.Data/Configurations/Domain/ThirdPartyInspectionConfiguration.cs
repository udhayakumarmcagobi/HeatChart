using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class ThirdPartyInspectionConfiguration : EntityBaseConfiguration<ThirdPartyInspection>
    {
        public ThirdPartyInspectionConfiguration()
        {
            Property(tpi => tpi.Name).IsRequired().HasMaxLength(50);
            Property(tpi => tpi.Email).IsOptional().HasMaxLength(50);
            Property(tpi => tpi.Landline).IsOptional().HasMaxLength(15);
            Property(tpi => tpi.Mobile).IsOptional().HasMaxLength(15);
            Property(tpi => tpi.Address).IsOptional().HasMaxLength(200);

            Property(tpi => tpi.AdditionalDetails).IsOptional().HasMaxLength(1000);

            Property(tpi => tpi.CreatedBy).IsRequired().HasMaxLength(50);
            Property(tpi => tpi.CreatedOn).IsRequired();
            Property(tpi => tpi.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(tpi => tpi.ModifiedOn).IsRequired();

            Property(tpi => tpi.IsDeleted).IsOptional();
        }
    }
}
