using HeatChart.Entities.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Domain.Core.Utilities
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public Users User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
