using System;

namespace HeatChart.Business.ViewModels.Domain
{
    public class TestVM 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Boolean IsDeleted { get; set; }

        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	        
    }
}
