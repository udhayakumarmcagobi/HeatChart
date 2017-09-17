using System.Collections.Generic;

namespace HeatHeatChart.ViewModels
{
    /// <summary>
    /// This is relationship entity to establish relationship between Heat Chart Details and Material register header and sub series
    /// </summary>
    public class HeatChartMaterialHeaderRelationshipVM
    {
        public int ID { get; set; } // Auto generated ID
       
        public int HeatChartDetailsID { get; set; }
        public int MaterialRegisterHeaderID { get; set; }
        public int MaterialRegisterSubSeriesID { get; set; }

        public HeatChartDetailsVM HeatChartDetails { get; set; }   
        public List<MaterialRegisterHeaderVM> MaterialRegisterHeaders { get; set; }  
        public List<MaterialRegisterSubSeriesVM> MaterialRegisterSubSeries { get; set; }

        public MaterialRegisterHeaderVM SelectedMaterialRegisterHeader { get; set; }
        public MaterialRegisterSubSeriesVM SelectedMaterialRegisterSubSeries { get; set; }

    }
}
