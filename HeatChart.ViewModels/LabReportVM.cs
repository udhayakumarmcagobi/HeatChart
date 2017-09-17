using System;

namespace HeatHeatChart.ViewModels
{
    public class LabReportVM 
    {
        public int ID { get; set; }
        public int SeqNum { get; set; }
        public string LabName { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }
        public string TCNumber { get; set; }
        public DateTime TCDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public MaterialRegisterSubSeriesVM MaterialRegisterSubSeries { get; set; }
    }
}
