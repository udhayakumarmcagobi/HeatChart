using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql
{
    /// <summary>
    /// This is relationship entity to establish relationship between Heat Chart Details and Material register header and sub series
    /// </summary>
    public class HeatChartMaterialHeaderRelationship : IEntityBase
    {
        public int ID { get; set; } // Auto generated ID       
        public int MaterialRegisterHeaderID { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }
        
        public virtual HeatChartDetails HeatChartDetails { get; set; }

        public virtual MaterialRegisterHeader MaterialRegisterHeaders { get; set; }

        public virtual MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }

    }
}
