using HeatChart.Entities.Sql;

namespace HeatChart.Data.Sql.Configurations
{
    public class MaterialRegisterHeaderConfiguration : EntityBaseConfiguration<MaterialRegisterHeader>
    {
        public MaterialRegisterHeaderConfiguration()
        {
            Property(mrh => mrh.CTNumber).IsRequired().HasMaxLength(50);
            Property(mrh => mrh.StatusID).IsRequired();
            Property(mrh => mrh.SupplierPONumber).IsRequired().HasMaxLength(50);
            Property(mrh => mrh.SupplierPODate).IsRequired();
            Property(mrh => mrh.PartiallyAcceptedRemarks).HasMaxLength(100);
            Property(mrh => mrh.OtherInfo).IsOptional();
            Property(mrh => mrh.Quantity).IsOptional();

            Property(mrh => mrh.CreatedBy).IsRequired().HasMaxLength(50);
            Property(mrh => mrh.CreatedOn).IsRequired();
            Property(mrh => mrh.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(mrh => mrh.ModifiedOn).IsRequired();

            Property(hcd => hcd.IsDeleted).IsOptional();

            HasRequired(mrh => mrh.Specification).
                WithMany(rel => rel.MaterialRegisterHeaders).WillCascadeOnDelete(false);

            HasRequired(mrh => mrh.ThirdPartyInspections).
                WithMany(rel => rel.MaterialRegisterHeaders).WillCascadeOnDelete(false);

            HasRequired(mrh => mrh.Suppliers).
                WithMany(rel => rel.MaterialRegisterHeaders).WillCascadeOnDelete(false);
        }
    }
}
