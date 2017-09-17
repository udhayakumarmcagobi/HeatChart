using HeatChart.Entities.Sql.Domain;
using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql
{
    public class MaterialRegisterSubSeries : IEntityBase
    {
        public int ID { get; set; } // It is unique ID for Material register sub series. Each Material Header can have multiple sub series
        public string SubSeriesNumber { get; set; } // Sub series number
        public int MaterialRegisterHeaderID { get; set; } // Material Header ID
        public int StatusID { get; set; } // It is for Accepted, rejected
        public bool IsDeleted { get; set; } // The status of the records.whether it is deleted or not.

        public virtual Status Status { get; set; }

        public virtual MaterialRegisterHeader MaterialRegisterHeader { get; set; }

        public virtual LabReport LabReport { get; set; }

        public virtual MillDetail MillDetail { get; set; }

        public virtual ICollection<MaterialRegisterFileDetail> MaterialRegisterFileDetails { get; set; }

        public virtual ICollection<MaterialRegisterSubseriesTestRelationship> MaterialRegisterSubSeriesTestRelationships { get; set; }

    }
}
