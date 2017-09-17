namespace HeatChart.Business.Models
{
    /// <summary>
    /// This is relationship entity to establish relationship between Heat Chart Details and Material register header and sub series
    /// </summary>
    public class HeatChartMaterialHeaderRelationship 
    {
        public int ID { get; set; } // Auto generated ID
       
        public int HeatChartDetailsID { get; set; }
        public int MaterialRegisterHeaderID { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }

        public HeatChartDetails HeatChartDetails { get; set; }   
        public MaterialRegisterHeader MaterialRegisterHeaders { get; set; }  
        public MaterialRegisterSubSeries MaterialRegisterSubSeries { get; set; }

    }
}
