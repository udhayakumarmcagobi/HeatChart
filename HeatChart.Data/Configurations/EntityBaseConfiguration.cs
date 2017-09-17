using HeatChart.Entities.Sql.Interfaces;
using System.Data.Entity.ModelConfiguration;

namespace HeatChart.Data.Sql.Configurations
{
    public class EntityBaseConfiguration<T> : EntityTypeConfiguration<T> where T :class, IEntityBase
    {
        public EntityBaseConfiguration()
        {
            HasKey(e => e.ID);
        }
    }
}
