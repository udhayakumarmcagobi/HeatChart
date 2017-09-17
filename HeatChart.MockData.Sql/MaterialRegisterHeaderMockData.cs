using HeatChart.Entities.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.MockData.Sql
{
    public class MaterialRegisterHeaderMockData
    {        
        public static MaterialRegisterHeader GetMaterialRegisterHeader()
        {
            var materialRegisterHeader = new MaterialRegisterHeader()
            {
                CreatedBy = "Udhay",       
                RawMaterialFormID = 1,
                SupplierID = 1,     
                SpecificationsID = 1, 
                ThirdPartyInspectionID = 1,
                CreatedOn = DateTime.Now,
                CTNumber = "MP1212",
                DimensionID = 1,
                ModifiedBy = "udhay",
                ModifiedOn = DateTime.Now,
                PartiallyAcceptedRemarks = "Testing",
                StatusID = 1,
                SupplierPODate = DateTime.Now,
                SupplierPONumber = "XYZ"                
            };

            return materialRegisterHeader;
        }
    }
}
