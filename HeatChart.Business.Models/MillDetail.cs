using System;

namespace HeatChart.Business.Models
{
    public class MillDetail 
    {
        public int ID { get; set; }
        public string HeatOrLotNumber { get; set; }
        public string ProductNumber { get; set; }
        public string MillName { get; set; }
        public string TCNumber { get; set; }
        public DateTime TCDate { get; set; }
        public bool IsDeleted { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }

        public MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }

    }
}
