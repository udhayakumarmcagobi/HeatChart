using HeatChart.Entities.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Data.Sql.Configurations
{
    public class MillDetailConfiguration : EntityBaseConfiguration<MillDetail>
    {
        public MillDetailConfiguration()
        {
            Property(mill => mill.MillName).HasMaxLength(50);
            Property(mill => mill.HeatOrLotNumber).HasMaxLength(50);
            Property(mill => mill.ProductNumber).HasMaxLength(50);
            Property(mill => mill.TCNumber).IsRequired().HasMaxLength(50);
            Property(mill => mill.TCDate).IsRequired();
        }
    }
}
