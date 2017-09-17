using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class CustomerConfiguration : EntityBaseConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            Property(customer => customer.Name).IsRequired().HasMaxLength(50);
            Property(customer => customer.Email).IsOptional().HasMaxLength(50);
            Property(customer => customer.Landline).IsOptional().HasMaxLength(15);
            Property(customer => customer.Mobile).IsOptional().HasMaxLength(15);
            Property(customer => customer.Address).IsOptional().HasMaxLength(200);

            Property(customer => customer.AdditionalDetails).IsOptional().HasMaxLength(1000);

            Property(customer => customer.CreatedBy).IsRequired().HasMaxLength(50);
            Property(customer => customer.CreatedOn).IsRequired();
            Property(customer => customer.ModifiedBy).IsRequired().HasMaxLength(50);
            Property(customer => customer.ModifiedOn).IsRequired();

            Property(customer => customer.IsDeleted).IsOptional();
        }
    }
}
