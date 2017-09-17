namespace HeatHeatChart.ViewModels
{
    public class MaterialRegisterFileDetailVM
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }       
        public int MaterialRegisterSubSeriesID { get; set; }

        public MaterialRegisterSubSeriesVM MaterialRegisterSubSeries { get; set; }

    }
}
