using HeatChart.Entities.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Data.Sql.Configurations
{
    public class LabReportConfiguration : EntityBaseConfiguration<LabReport>
    {
        public LabReportConfiguration()
        {
            Property(labReport => labReport.LabName).HasMaxLength(50);
            Property(labReport => labReport.TCNumber).IsRequired().HasMaxLength(50);
            Property(labReport => labReport.Description).HasMaxLength(150);
            Property(labReport => labReport.TCDate).IsRequired();
        }
    }
}
