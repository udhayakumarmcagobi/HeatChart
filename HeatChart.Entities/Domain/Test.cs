using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;

namespace HeatChart.Entities.Sql.Domain
{
    public class Test : IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // Description of the Test
        public Boolean IsDeleted { get; set; }

        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	

        public virtual ICollection<MaterialRegisterSubseriesTestRelationship> MaterialRegisterSubSeriesTestRelationsips { get; set; }
    }
}
