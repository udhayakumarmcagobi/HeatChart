using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Xml;

namespace HeatChart.Infrastructure.Common.Utilities
{

    public partial class ConfigHelper
    {

        #region "Private Constant Declarations"
        // AppSetting Config Key Values
        private const string DATA_STORE_PATH_CONFIG_KEY = "DataStoreRelativePath";
        private const string LOG_CONSUMER_REQUESTS_CONFIG_KEY = "LogConsumerRequests";
        private const string LOG_CONSUMER_RESPONSES_CONFIG_KEY = "LogConsumerResponses";
        private const string LOG_PROVIDER_REQUESTS_CONFIG_KEY = "LogProviderRequests";
        private const string LOG_PROVIDER_RESPONSES_CONFIG_KEY = "LogProviderResponses";
        private const string MOCK_PATH_CONFIG_KEY = "MockRelativePath";
        private const string REPORT_ELAPSED_TIME_STATISTIC_CONFIG_KEY = "ReportElapsedTimeStatistics";
        private const string REPORT_USAGE_STATISTIC_CONFIG_KEY = "ReportUsageStatistics";
        private const string ROOT_PATH_CONFIG_KEY = "RootAbsolutePath";

        private const string TEST_DATA_PATH_CONFIG_KEY = "TestDataRelativePath";
        //Default Configuration Settings
        private const string CONSUMER_LOG_FOLDER_NAME_DEFAULT = "Consumer\\";
        private const string DATA_STORE_FOLDER_NAME_DEFAULT = "Data\\";
        private const bool LOG_CONSUMER_REQUESTS_DEFAULT = false;
        private const bool LOG_CONSUMER_RESPONSES_DEFAULT = false;
        private const bool LOG_PROVIDER_REQUESTS_DEFAULT = false;
        private const bool LOG_PROVIDER_RESPONSES_DEFAULT = false;
        private const string MOCK_SOURCE_FOLDER_NAME_DEFAULT = "Mocks\\";
        private const string PROVIDER_LOG_FOLDER_NAME_DEFAULT = "Provider\\";
        private const bool REPORT_ELAPSED_TIME_STATISTIC_DEFAULT = false;
        private const bool REPORT_USAGE_STATISTIC_DEFAULT = false;
        private const string REQUEST_LOG_FOLDER_NAME_DEFAULT = "Request\\";
        private const string RESPONSE_LOG_FOLDER_NAME_DEFAULT = "Response\\";
        private const string TEST_DATA_FOLDER_NAME_DEFAULT = "TestData\\";

        private const string CURRENT_FOLDER_DEFAULT = ".\\";

        #endregion

        #region "Public Constant Declarations"
        #endregion

        #region "Private Shared Variable Declarations"
        #endregion

        #region "Public Shared Properties"

        public static string ConsumerRequestLogPath
        {
            get
            {
                string _LogPath = string.Format("{0}{1}{2}", RootPath, CONSUMER_LOG_FOLDER_NAME_DEFAULT, REQUEST_LOG_FOLDER_NAME_DEFAULT);
                if ((_LogPath.EndsWith("\\") == false))
                    _LogPath += "\\";
                return _LogPath;
            }
        }

        public static string ConsumerResponseLogPath
        {
            get
            {
                string _LogPath = string.Format("{0}{1}{2}", RootPath, CONSUMER_LOG_FOLDER_NAME_DEFAULT, RESPONSE_LOG_FOLDER_NAME_DEFAULT);
                if ((_LogPath.EndsWith("\\") == false))
                    _LogPath += "\\";
                return _LogPath;
            }
        }

        public static string DataStorePath
        {
            get
            {
                string _DataStoreRelativePath = ConfigHelper.GetAppSettingConfigValueAsString(DATA_STORE_PATH_CONFIG_KEY, DATA_STORE_FOLDER_NAME_DEFAULT);
                string _DataStorePath = string.Format("{0}{1}", RootPath, _DataStoreRelativePath);
                if ((_DataStorePath.EndsWith("\\") == false))
                    _DataStorePath += "\\";
                return _DataStorePath;
            }
        }

        public static bool LogConsumerRequests
        {
            get
            {
                bool _Result = ConfigHelper.GetAppSettingConfigValueAsBoolean(LOG_CONSUMER_REQUESTS_CONFIG_KEY, LOG_CONSUMER_REQUESTS_DEFAULT);
                return _Result;
            }
        }

        public static bool LogConsumerResponses
        {
            get
            {
                bool _Result = ConfigHelper.GetAppSettingConfigValueAsBoolean(LOG_CONSUMER_RESPONSES_CONFIG_KEY, LOG_CONSUMER_RESPONSES_DEFAULT);
                return _Result;
            }
        }

        public static bool LogProviderRequests
        {
            get
            {
                bool _Result = ConfigHelper.GetAppSettingConfigValueAsBoolean(LOG_PROVIDER_REQUESTS_CONFIG_KEY, LOG_PROVIDER_REQUESTS_DEFAULT);
                return _Result;
            }
        }

        public static bool LogProviderResponses
        {
            get
            {
                bool _Result = ConfigHelper.GetAppSettingConfigValueAsBoolean(LOG_PROVIDER_RESPONSES_CONFIG_KEY, LOG_PROVIDER_RESPONSES_DEFAULT);
                return _Result;
            }
        }

        public static string MockPath
        {
            get
            {
                string _MockRelativePath = ConfigHelper.GetAppSettingConfigValueAsString(MOCK_PATH_CONFIG_KEY, MOCK_SOURCE_FOLDER_NAME_DEFAULT);
                string _MockPath = string.Format("{0}{1}", RootPath, _MockRelativePath);
                if ((_MockPath.EndsWith("\\") == false))
                    _MockPath += "\\";
                return _MockPath;
            }
        }

        public static string ProviderRequestLogPath
        {
            get
            {
                string _LogPath = string.Format("{0}{1}{2}", RootPath, PROVIDER_LOG_FOLDER_NAME_DEFAULT, REQUEST_LOG_FOLDER_NAME_DEFAULT);
                if ((_LogPath.EndsWith("\\") == false))
                    _LogPath += "\\";
                return _LogPath;
            }
        }

        public static string ProviderResponseLogPath
        {
            get
            {
                string _LogPath = string.Format("{0}{1}{2}", RootPath, PROVIDER_LOG_FOLDER_NAME_DEFAULT, RESPONSE_LOG_FOLDER_NAME_DEFAULT);
                if ((_LogPath.EndsWith("\\") == false))
                    _LogPath += "\\";
                return _LogPath;
            }
        }

        public static bool ReportElapsedTimeStatistic
        {
            get
            {
                bool _Result = ConfigHelper.GetAppSettingConfigValueAsBoolean(REPORT_ELAPSED_TIME_STATISTIC_CONFIG_KEY, REPORT_ELAPSED_TIME_STATISTIC_DEFAULT);
                return _Result;
            }
        }

        public static bool ReportUsageStatistic
        {
            get
            {
                bool _Result = ConfigHelper.GetAppSettingConfigValueAsBoolean(REPORT_USAGE_STATISTIC_CONFIG_KEY, REPORT_USAGE_STATISTIC_DEFAULT);
                return _Result;
            }
        }

        public static string RootPath
        {
            get
            {
                string _RootPath = ConfigHelper.GetAppSettingConfigValueAsString(ROOT_PATH_CONFIG_KEY, ".\\");
                if ((_RootPath.EndsWith("\\") == false))
                    _RootPath += "\\";
                return _RootPath;
            }
        }

        public static string TestDataPath
        {
            get
            {
                string _TestDataRelativePath = ConfigHelper.GetAppSettingConfigValueAsString(TEST_DATA_PATH_CONFIG_KEY, TEST_DATA_FOLDER_NAME_DEFAULT);
                string _TestdataPath = string.Format("{0}{1}", RootPath, _TestDataRelativePath);
                if ((_TestdataPath.EndsWith("\\") == false))
                    _TestdataPath += "\\";
                return _TestdataPath;
            }
        }
        #endregion

        #region "Public Shared Methods"
        public static bool GetAppSettingConfigValueAsBoolean(string configurationKey, bool defaultValue = false)
        {
            string configValue = ConfigurationManager.AppSettings[configurationKey];
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;
            return Convert.ToBoolean(configValue);
        }

        public static int GetAppSettingConfigValueAsInteger(string configurationKey, int defaultValue = 0)
        {
            string configValue = ConfigurationManager.AppSettings[configurationKey];
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;
            return Convert.ToInt32(configValue);
        }

        public static string GetAppSettingConfigValueAsString(string configurationKey, string defaultValue = "")
        {
            string configValue = ConfigurationManager.AppSettings[configurationKey];
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;
            return configValue;
        }

        public static string GetConnectionString(string name)
        {
            string connStr = string.Empty;
            if ((ConfigurationManager.ConnectionStrings[name] != null))
                connStr = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return connStr;
        }
        #endregion

    }

}
