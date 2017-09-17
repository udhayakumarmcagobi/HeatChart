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
    public class MillDetail : IEntityBase
    {
        public int ID { get; set; }
        public string HeatOrLotNumber { get; set; }
        public string ProductNumber { get; set; }
        public string MillName { get; set; }
        public string TCNumber { get; set; }
        public DateTime TCDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }

    }
}
