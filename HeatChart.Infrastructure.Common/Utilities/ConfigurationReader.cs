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
        #region Other Configs

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

        public static string BucketName
        {
            get
            {
                return (WebConfigurationManager.AppSettings["BucketName"]);
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

        public static bool IsSaveToDirectory
        {
            get
            {
                return Convert.ToBoolean(WebConfigurationManager.AppSettings["IsSaveToDirectory"]);
            }
        }

        #endregion

        #region AWS Configs

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
        #endregion

        #region Heat Chart Comapny Config


        public static string CompanyName
        {
            get
            {
                return (WebConfigurationManager.AppSettings["CompanyName"]);
            }
        }

        public static string CompanyAddressHeader
        {
            get
            {
                return (WebConfigurationManager.AppSettings["CompanyAddressHeader"]);
            }
        }

        public static string CompanyAddress
        {
            get
            {
                return (WebConfigurationManager.AppSettings["CompanyAddress"]);
            }
        }

        public static string CompanyTelephone
        {
            get
            {
                return (WebConfigurationManager.AppSettings["CompanyTelephone"]);
            }
        }

        public static string CompanyEmail
        {
            get
            {
                return (WebConfigurationManager.AppSettings["CompanyEmail"]);
            }
        }

        public static string CompanyWebsite
        {
            get
            {
                return (WebConfigurationManager.AppSettings["CompanyWebsite"]);
            }
        }

        public static string CompanyCIN
        {
            get
            {
                return (WebConfigurationManager.AppSettings["CompanyCIN"]);
            }
        }

        public static string SurveyorName
        {
            get
            {
                return (WebConfigurationManager.AppSettings["SurveyorName"]);
            }
        }
        
        #endregion

    }
}
