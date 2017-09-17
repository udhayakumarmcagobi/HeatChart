using HeatChart.DataRepository.Sql.Interfaces;
using HeatChart.Entities.Sql;
using HeatChart.Entities.Sql.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.DataRepository.Sql.Extensions
{
    public static class MaterialRegisterHeaderExtensions
    {
        public static MaterialRegisterHeader GetSingleByMaterialRegisterHeaderCTNumber(this IEFRepository<MaterialRegisterHeader> materialRegisterHeaderRepository, string CTNumber)
        {
            return materialRegisterHeaderRepository.GetFirstOrDefault(x => x.CTNumber.Equals(CTNumber));
        }

        public static MaterialRegisterHeader GetSingleByMaterialRegisterHeaderID(this IEFRepository<MaterialRegisterHeader> materialRegisterHeaderRepository, int id)
        {
            return materialRegisterHeaderRepository.GetFirstOrDefault(x => x.ID == id);
        }

        public static bool MaterialRegisterHeaderExists(this IEFRepository<MaterialRegisterHeader> materialRegisterHeaderRepository, string CTNumber)
        {
            return materialRegisterHeaderRepository.GetAll()
                .Any(c => c.CTNumber.ToLower().Equals(CTNumber.ToLower()));
        }
    }
}
