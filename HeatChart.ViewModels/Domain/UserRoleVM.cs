using HeatHeatChart.ViewModels.Domain;

namespace HeatChart.Entities.Sql.Domain
{
    public class UserRoleVM
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public RoleVM Role { get; set; }
    }
}
