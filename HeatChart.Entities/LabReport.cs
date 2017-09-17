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
    public class LabReport : IEntityBase
    {
        public int ID { get; set; }
        public int SeqNum { get; set; }
        public string LabName { get; set; }
        public string TCNumber { get; set; }
        public DateTime TCDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }
    }
}
