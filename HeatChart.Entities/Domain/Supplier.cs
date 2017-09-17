using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql.Domain
{
    /// <summary>
    /// HeatChart Supplier Information
    /// </summary>
    public class Supplier : IEntityBase
    {
        public int ID { get; set; } // Auto generated ID
        public string Name { get; set; } // Name 
        public string Address { get; set; } // Address 
        public string Email { get; set; } // Email ID 
        public string Landline { get; set; } // Landline phone
        public string Mobile { get; set; } // Mobile phone
        public string AdditionalDetails { get; set; } // To store any additional details of the customer. 
        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	
        public Boolean IsDeleted { get; set; } // It is used for soft delete

        public virtual ICollection<HeatChartHeader> HeatChartHeaders { get; set; }
        public virtual ICollection<MaterialRegisterHeader> MaterialRegisterHeaders { get; set; }
    }
}
