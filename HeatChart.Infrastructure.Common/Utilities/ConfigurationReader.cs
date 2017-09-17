using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace HeatChart.Infrastructure.Common.Utilities
{
    public class ConfigurationReader
    {
        public static string CTPregix
        {
            get
            {
                return (WebConfigurationManager.AppSettings["CTPrefix"]);
            }
        }

        public static string HCPregix
        {
            get
            {
                return (WebConfigurationManager.AppSettings["HCPrefix"]);
            }
        }

        public static string MaterialRegisterFilePath
        {
            get
            {
                return (WebConfigurationManager.AppSettings["MaterialRegisterFilePath"]);
            }
        }

        public static string HeatChartFilePath
        {
            get
            {
                return (WebConfigurationManager.AppSettings["HeatChartFilePath"]);
            }
        }

        public static int Membership
        {
            get
            {
                return (Convert.ToInt32(WebConfigurationManager.AppSettings["Membership"]));
            }
        }

        public static string AWSAccessKey
        {
            get
            {
                return (WebConfigurationManager.AppSettings["AWSAccessKey"]);
            }
        }

        public static string AWSSecretKey
        {
            get
            {
                return (WebConfigurationManager.AppSettings["AWSSecretKey"]);
            }
        }

        public static string BucketName
        {
            get
            {
                return (WebConfigurationManager.AppSettings["BucketName"]);
            }
        }

        public static string AWSMaterialRegisterFolderName
        {
            get
            {
                return (WebConfigurationManager.AppSettings["AWSMaterialRegisterFolderName"]);
            }
        }

        public static string AWSHeatChartFolderName
        {
            get
            {
                return (WebConfigurationManager.AppSettings["AWSHeatChartFolderName"]);
            }
        }

        public static bool IsHeatChartNumberAutoCalculate
        {
            get
            {
                return Convert.ToBoolean(WebConfigurationManager.AppSettings["IsHeatChartNumberAutoCalculate"]);
            }
        }
        public static bool IsCheckTestNumberAutoCalculate
        {
            get
            {
                return Convert.ToBoolean(WebConfigurationManager.AppSettings["IsCheckTestNumberAutoCalculate"]);
            }
        }

    }
}
