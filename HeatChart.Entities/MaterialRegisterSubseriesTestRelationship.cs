﻿using HeatChart.Entities.Sql.Domain;
using HeatChart.Entities.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Entities.Sql
{
    public class MaterialRegisterSubseriesTestRelationship : IEntityBase
    {
        public int ID { get; set; }
        public int TestID { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }

        public virtual MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }

        public virtual Test Test { get; set; }
    }
}
