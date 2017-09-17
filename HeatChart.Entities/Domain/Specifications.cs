using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql.Domain
{
    /// <summary>
    /// Used to store specification details
    /// </summary>
    public class Specifications : IEntityBase
    {
        public int ID { get; set; } // Auto generated ID
        public string Name { get; set; } // Name of the specification
        public string Description { get; set; } // Description of the specification
        public Boolean IsDeleted { get; set; } // It is used for soft delete

        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	

        public virtual ICollection<HeatChartDetails> HeatChartDetails { get; set; }
        public virtual ICollection<MaterialRegisterHeader> MaterialRegisterHeaders { get; set; }
    }
}
