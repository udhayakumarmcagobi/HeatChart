namespace HeatChart.Business.Models
{
    public class MaterialRegisterFileDetail 
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }       
        public int MaterialRegisterSubSeriesID { get; set; }

        public MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }

    }
}
