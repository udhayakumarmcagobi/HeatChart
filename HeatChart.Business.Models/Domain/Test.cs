using System;
using System.Collections.Generic;

namespace HeatChart.Business.Models.Domain
{
    public class Test 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Boolean IsDeleted { get; set; }

        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	

        public virtual IList<MaterialRegisterSubseriesTestRelationship> MaterialRegisterSubSeriesTestRelationsips { get; set; }
    }
}
