using HeatChart.Business.Models.Domain;

namespace HeatChart.Business.Models
{
    public class UserRole 
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
