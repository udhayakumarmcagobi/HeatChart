using System;

namespace HeatHeatChart.ViewModels
{
    public class MillDetailVM 
    {
        public int ID { get; set; }
        public string HeatOrLotNumber { get; set; }
        public string ProductNumber { get; set; }
        public string MillName { get; set; }
        public string TCNumber { get; set; }
        public DateTime TCDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }

        public MaterialRegisterSubSeriesVM MaterialRegisterSubSeries { get; set; }

    }
}
