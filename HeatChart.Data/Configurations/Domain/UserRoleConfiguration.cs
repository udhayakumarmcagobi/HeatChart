using HeatChart.Entities.Sql.Domain;

namespace HeatChart.Data.Sql.Configurations.Domain
{
    public class UserRoleConfiguration : EntityBaseConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            Property(userRole => userRole.UsersId).IsRequired();
            Property(userRole => userRole.RoleId).IsRequired();
        }
    }
}
