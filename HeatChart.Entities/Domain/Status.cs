using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql.Domain
{
    public class Status : IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MaterialRegisterHeader> MaterialRegisterHeaders { get; set; }

        public virtual ICollection<MaterialRegisterSubSeries> MaterialRegisterSubSeries { get; set; }
    }
}
