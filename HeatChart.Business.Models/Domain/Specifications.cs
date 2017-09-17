using System;
using System.Collections.Generic;

namespace HeatChart.Business.Models.Domain
{
    /// <summary>
    /// Used to store specification details
    /// </summary>
    public class Specifications
    {
        public int ID { get; set; } // Auto generated ID
        public string Name { get; set; } // Name of the specification
        public Boolean IsDeleted { get; set; } // It is used for soft delete

        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	

        public virtual IList<HeatChartDetails> HeatChartDetails { get; set; }
        public virtual IList<MaterialRegisterHeader> MaterialRegisterHeaders { get; set; }
    }
}
