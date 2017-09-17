using System;

namespace HeatChart.Business.Models
{
    public class LabReport 
    {
        public int ID { get; set; }
        public int SeqNum { get; set; }
        public string ReportName { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }
        public string TCNumber { get; set; }
        public DateTime TCDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }
    }
}
