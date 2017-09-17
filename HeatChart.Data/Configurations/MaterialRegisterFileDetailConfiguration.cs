using HeatChart.Entities.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Data.Sql.Configurations
{
    public class MaterialRegisterFileDetailConfiguration : EntityBaseConfiguration<MaterialRegisterFileDetail>
    {
        public MaterialRegisterFileDetailConfiguration()
        {
            Property(mrf => mrf.FileName).HasMaxLength(50).IsRequired();
            Property(mrf => mrf.Path).HasMaxLength(100).IsRequired();

            HasRequired(mrf => mrf.MaterialRegisterSubSeries).WithMany().
                        HasForeignKey(rel => rel.MaterialRegisterSubSeriesID).WillCascadeOnDelete(false);
        }
    }
}
