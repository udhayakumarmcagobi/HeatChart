using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<Users>
    {
        public UserConfiguration()
        {
            Property(user => user.Username).IsRequired().HasMaxLength(100);
            Property(user => user.Email).IsRequired().HasMaxLength(200);
            Property(user => user.HashedPassword).IsRequired().HasMaxLength(200);
            Property(user => user.Salt).IsRequired().HasMaxLength(200);
            Property(user => user.IsLocked).IsRequired();
            Property(user => user.DateCreated);
        }
    }
}
