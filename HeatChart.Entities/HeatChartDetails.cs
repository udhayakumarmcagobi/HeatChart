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
    // Heat chart Details - Each Heat Chart header can have multiple heat Chart Details
    public class HeatChartDetails : IEntityBase
    {
        public int ID { get; set; } // Auto generated ID
        public int HeatChartHeaderID { get; set; } // Heat Chart Header ID
        public string PartNumber { get; set; } // Part numbers
        public string SheetNo { get; set; } // Sheet No
        public int SpecificationsID { get; set; }
        public int DimensionID { get; set; }

        public bool IsDeleted { get; set; }

        public virtual HeatChartHeader HeatChartHeader { get; set; } // Material Header relationship

        public virtual Specifications Specification { get; set; } // Specification

        public virtual Dimension Dimension { get; set; } // Dimension

        public virtual HeatChartMaterialHeaderRelationship HeathChartMaterialHeaderRelationships { get; set; }
    }
}
