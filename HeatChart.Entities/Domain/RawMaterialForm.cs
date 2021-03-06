﻿using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql.Domain
{
    /// <summary>
    /// Used to store RawMaterialForm details
    /// </summary>
    public class RawMaterialForm : IEntityBase
    {
        public int ID { get; set; } // Auto generated ID
        public string Name { get; set; } // Name of the RawMaterialForm
        public string Description { get; set; } // Description of the RawMaterialForm
        public Boolean IsDeleted { get; set; } // It is used for soft delete

        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	

        public virtual ICollection<MaterialRegisterHeader> MaterialRegisterHeaders { get; set; }
    }
}
