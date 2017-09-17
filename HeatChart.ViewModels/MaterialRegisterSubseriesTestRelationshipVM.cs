using HeatHeatChart.ViewModels.Domain;

namespace HeatHeatChart.ViewModels
{
    public class MaterialRegisterSubseriesTestRelationshipVM
    {
        public int ID { get; set; }
        public int TestID { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }

        public MaterialRegisterSubSeriesVM MaterialRegisterSubSeries { get; set; }
        public TestVM Test { get; set; }
    }
}
