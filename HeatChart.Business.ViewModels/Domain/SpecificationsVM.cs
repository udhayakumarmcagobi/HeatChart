using System;

namespace HeatChart.Business.ViewModels.Domain
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
    }
}
