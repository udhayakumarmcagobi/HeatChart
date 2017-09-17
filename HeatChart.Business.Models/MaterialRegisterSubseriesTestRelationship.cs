using HeatChart.Business.Models.Domain;

namespace HeatChart.Business.Models
{
    public class MaterialRegisterSubseriesTestRelationship 
    {
        public int ID { get; set; }
        public int TestID { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }

        public MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }
        public Test Test { get; set; }
    }
}
