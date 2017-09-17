using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql
{
    public class MaterialRegisterFileDetail : IEntityBase
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }       
        public int MaterialRegisterSubSeriesID { get; set; }

        public virtual MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }

    }
}
