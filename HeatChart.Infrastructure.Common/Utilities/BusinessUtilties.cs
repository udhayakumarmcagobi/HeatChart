using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Infrastructure.Common.Utilities
{
    public class BusinessUtilties
    {
        public static string AutoCalculateCTNumber(List<string> CTNumberList)
        {
            string CTFormatCurrentYear = ConfigurationReader.CTPregix + Convert.ToString(DateTime.Now.Year).Substring(2, 2);

            int CTCount = CTNumberList.Where(x => x.StartsWith(CTFormatCurrentYear)).Count();

            return string.Format("{0}{1}{2}", CTFormatCurrentYear, "/", CTCount + 1);

        }
        public static string AutoCalculateHeatChartNumber(List<string> HCNumberList)
        {
            string HCFormatCurrentYear = ConfigurationReader.HCPregix + Convert.ToString(DateTime.Now.Year).Substring(2, 2);

            int HCCount = HCNumberList.Where(x => x.StartsWith(HCFormatCurrentYear)).Count();

            return string.Format("{0}{1}{2}", HCFormatCurrentYear, "/", HCCount + 1);

        }

        public static void WriteLogIntoFile(string log)
        {
            string createText = log + Environment.NewLine;

            string fileName = DateTime.Today + ".txt";
            File.WriteAllText(@"C:\inetpub\wwwroot\Logs\"+ fileName, createText);
        }
    }
}
