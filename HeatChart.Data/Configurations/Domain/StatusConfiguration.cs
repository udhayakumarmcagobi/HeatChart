using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class StatusConfiguration : EntityBaseConfiguration<Status>
    {
        public StatusConfiguration()
        {
            Property(status => status.Name).IsRequired().HasMaxLength(10);           
        }
    }
}
