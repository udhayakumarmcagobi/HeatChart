using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class SupplierConfiguration : EntityBaseConfiguration<Supplier>
    {
        public SupplierConfiguration()
        {
            Property(supplier => supplier.Name).IsRequired().HasMaxLength(50);
            Property(supplier => supplier.Email).IsOptional().HasMaxLength(50);
            Property(supplier => supplier.Landline).IsOptional().HasMaxLength(15);
            Property(supplier => supplier.Mobile).IsOptional().HasMaxLength(15);
            Property(supplier => supplier.Address).IsOptional().HasMaxLength(200);

            Property(supplier => supplier.AdditionalDetails).IsOptional().HasMaxLength(1000);

            Property(supplier => supplier.CreatedBy).IsRequired().HasMaxLength(50);
            Property(supplier => supplier.CreatedOn).IsRequired();
            Property(supplier => supplier.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(supplier => supplier.ModifiedOn).IsRequired();

            Property(supplier => supplier.IsDeleted).IsOptional();
        }
    }
}
