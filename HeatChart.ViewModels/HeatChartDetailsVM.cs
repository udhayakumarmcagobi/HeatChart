﻿using HeatHeatChart.ViewModels.Domain;
using System;
using System.Collections.Generic;

namespace HeatHeatChart.ViewModels
{
    // Heat chart Details - Each Heat Chart header can have multiple heat Chart Details
    public class HeatChartDetailsVM
    {
        public int ID { get; set; } // Auto generated ID
        public int HeatChartHeaderID { get; set; } // Heat Chart Header ID
        public string PartNumber { get; set; } // Part numbers
        public string PartNumberDescription { get; set; } // Part number Description
        public string SheetNo { get; set; } // Sheet No

        public Nullable<int> HeatChartMaterialHeaderRelationshipID { get; set; }
        public SpecificationsVM SpecificationSelected { get; set; } // Selected Specification
        public List<SpecificationsVM> Specifications { get; set; } // Specifications
        public DimensionVM DimensionSelected { get; set; } // Selected Dimension
        public List<DimensionVM> Dimensions { get; set; } // Dimensions

        public string Dimension { get; set; }

        public bool IsDeleted { get; set; }

        public List<MaterialRegisterHeaderVM> MaterialRegisterHeaders { get; set; }
        public List<MaterialRegisterSubSeriesVM> MaterialRegisterSubSeries { get; set; }

        public MaterialRegisterHeaderVM MaterialRegisterHeaderSelected { get; set; }
        public MaterialRegisterSubSeriesVM MaterialRegisterSubSeriesSelected { get; set; }

        public HeatChartHeaderVM HeatChartHeader { get; set; } // Material Header relationship                
    }
}
