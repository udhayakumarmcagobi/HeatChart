using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HeatChart.Infrastructure.Common.Utilities
{
    public sealed class ObjHelper
    {
        /// <summary>
        /// Convert the value to upper case
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>returns upper case value/returns>
        public static string ToUpperCase(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            string result = string.Empty;

            result = value.ToUpper().Trim();

            return result;
        }

        /// <summary>
        /// Convert the value to Double
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>returns the converted double value </returns>
        public static double ConvertToDouble(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;

            double result = 0;

            double.TryParse(value, out result);

            return result;
        }

        /// <summary>
        /// Convert the Angle value to DMS format 
        /// </summary>
        /// <param name="angValue">angle value</param>
        /// <returns>DMS format of the angle value</returns>
        public static string ConvertAngValToDMSFormat(string angValue)
        {
            if (string.IsNullOrEmpty(angValue)) return string.Empty;

            if (isValidAngleValue(angValue)) return angValue;

            StringBuilder formattedValue = new StringBuilder();

            angValue = angValue.Replace("/", "");

            bool negativeAngle = false;
            if (angValue.Substring(0, 1) == "-")
            {
                negativeAngle = true;
                angValue = angValue.Substring(1, (angValue.Length - 1));
            }

            if (angValue.Contains("."))
            {
                string[] angValueArray = null;
                angValueArray = angValue.Split('.');
                angValue = angValueArray[0];
            }

            if (!string.IsNullOrEmpty(angValue))
            {
                switch (angValue.Length)
                {
                    case 4:
                        if (negativeAngle)
                        {
                            formattedValue.Append("-00");
                        }
                        else
                        {
                            formattedValue.Append("00");
                        }
                        formattedValue.Append("/" + angValue.Substring(angValue.Length - 4, 2));
                        formattedValue.Append("/" + angValue.Substring(angValue.Length - 2, 2));
                        break;

                    case 3:
                        if (negativeAngle)
                        {
                            formattedValue.Append("-00");
                        }
                        else
                        {
                            formattedValue.Append("00");
                        }
                        formattedValue.Append("/0" + angValue.Substring(0, 1));
                        formattedValue.Append("/" + angValue.Substring(angValue.Length - 2, 2));
                        break;
                    case 2:
                        if (negativeAngle)
                        {
                            formattedValue.Append("-00");
                        }
                        else
                        {
                            formattedValue.Append("00");
                        }
                        formattedValue.Append("/00");
                        formattedValue.Append("/" + angValue.Trim());
                        break;
                    case 1:
                        if (negativeAngle)
                        {
                            formattedValue.Append("-00");
                        }
                        else
                        {
                            formattedValue.Append("00");
                        }
                        formattedValue.Append("/00");
                        formattedValue.Append("/0" + angValue.Trim());
                        break;
                    default:
                        if (negativeAngle)
                        {
                            formattedValue.Append("-" + angValue.Substring(0, angValue.Length - 4));
                        }
                        else
                        {
                            formattedValue.Append(angValue.Substring(0, angValue.Length - 4));
                        }
                        formattedValue.Append("/" + angValue.Substring(angValue.Length - 4, 2));
                        formattedValue.Append("/" + angValue.Substring(angValue.Length - 2, 2));
                        break;
                }

            }
            else
            {
                return angValue;
            }

            return Convert.ToString(formattedValue);

        }

        /// <summary>
        /// Format the number value based on the given Decimal digits
        /// </summary>
        /// <param name="numVal">number value</param>
        /// <param name="decimalDigits">decimal digits</param>
        /// <returns>returns the formatted number value</returns>
        public static string FormatingNumberValuesBasedOnDecimalDigits(string numVal, int decimalDigits)
        {
            if (decimalDigits == 0 || string.IsNullOrEmpty(numVal)) return numVal;

            string formattedValue = string.Empty;

            string[] numValues = numVal.Split('.');
            if (numValues != null && numValues.Length > 1)
            {
                if (numValues[1].ToString().Length > decimalDigits)
                {
                    numValues[1] = numValues[1].Substring(0, decimalDigits);
                }
                if (decimalDigits > numValues[1].ToString().Length)
                {
                    int count = decimalDigits - numValues[1].ToString().Length;
                    if (count > 0)
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            numValues[1] = numValues[1] + "0";
                        }
                    }
                }
                formattedValue = numValues[0] + "." + numValues[1];
            }
            else
            {
                if (decimalDigits > 0)
                {
                    numVal = numVal + ".";
                    for (int i = 1; i <= decimalDigits; i++)
                    {
                        numVal = numVal + "0";
                    }
                }

                formattedValue = numVal;
            }

            return formattedValue;
        }

        /// <summary>
        /// Round the value based on the decimal digits
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="decimalDigits">decimal digits to be rounded</param>
        /// <returns>returns the rounded value basd on the decimal digits</returns>
        public static string RoundDecimalDigits(string value, short decimalDigits)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            string result = string.Empty;

            double item = 0;

            double.TryParse(value, out item);

            if (item > 0)
            {
                result = item.ToString();
            }

            return result;
        }

        /// <summary>
        /// Remove the trailing Decimal point of the string
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>returns the trailing decimal point removed string</returns>
        public static string RemoveTrailingDecimalPoint(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains(".")) return value;

            return value.TrimEnd('.');
        }

        /// <summary>
        /// Get the decimal digit format as string
        /// </summary>
        /// <param name="decimalDigits">number of decimal digits</param>
        /// <returns>returns the format of decimal digits</returns>
        public static string GetDecimalDigitFormat(short decimalDigits)
        {
            string format = string.Empty;

            switch (decimalDigits)
            {
                case 0:
                    format = "[X]";
                    break;
                case 1:
                    format = "[X.X]";
                    break;
                case 2:
                    format = "[X.XX]";
                    break;
                case 3:
                    format = "[X.XXX]";
                    break;
                case 4:
                    format = "[X.XXXX]";
                    break;
                case 5:
                    format = "[X.XXXXX]";
                    break;
                case 6:
                    format = "[X.XXXXXX]";
                    break;
                case 7:
                    format = "[X.XXXXXXX]";
                    break;
            }

            return format;
        }

        /// <summary>
        /// Convert the list of strings into list of Dictionary which is used to order the string fractions by Ascending
        /// </summary>
        /// <param name="fractionItems">string dictionary list</param>
        /// <returns>returns the proper key and value dictionary</returns>
        public static Dictionary<string, string> ConvertStringListInToDictStringDoubles(Dictionary<string, string> fractionItems)
        {
            if (fractionItems == null || !fractionItems.Any()) return new Dictionary<string, string>();

            Dictionary<string, double> dictStringDouble = new Dictionary<string, double>();

            fractionItems.Values.ToList().ForEach(x =>
            {
                double result = FractionToDouble(x.Replace("+", "").Replace("-", ""));

                if (x.Contains("-"))
                {
                    result = -Math.Abs(result);
                }

                dictStringDouble.Add(x, result);

            });

            var result1 = dictStringDouble.OrderBy(x => x.Value).ToList();

            Dictionary<string, string> resultDictStringDouble = new Dictionary<string, string>();

            result1.ForEach(x =>
            {
                resultDictStringDouble.Add(x.Key, x.Key.ToString());
            });

            return resultDictStringDouble;
        }

        /// <summary>
        /// Convert the string fraction to double
        /// </summary>
        /// <param name="fraction">fraction value</param>
        /// <returns>returns the double value</returns>
        private static double FractionToDouble(string fraction)
        {
            double result;

            if (double.TryParse(fraction, out result))
            {
                return result;
            }

            string[] split = fraction.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;

                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return (double)a / b;
                    }

                    int c;

                    if (int.TryParse(split[2], out c))
                    {
                        return a + (double)b / c;
                    }
                }
            }

            return 0;

        }

        /// <summary>
        /// Check the angle is valid or not
        /// </summary>
        /// <param name="angleValue">angle value</param>
        /// <returns>returns the angle is valid or not</returns>
        private static bool isValidAngleValue(string angleValue)
        {
            if (string.IsNullOrEmpty(angleValue)) return false;

            bool isValid = false;

            if (angleValue.Length > 6)
            {
                string[] angleArray = angleValue.Split('/');

                if (angleArray.Length == 3)
                {
                    isValid = true;
                    Int32 degreeValue = Convert.ToInt32(angleArray[0].Replace("-", ""));

                    if (!(degreeValue >= 0 && degreeValue <= 360))
                    {
                        isValid = false;
                    }

                    Int32 minuteValue = Convert.ToInt32(angleArray[1]);
                    if (!(minuteValue >= 0 && minuteValue <= 60))
                    {
                        isValid = false;
                    }

                    Int32 secondValue = Convert.ToInt32(angleArray[2]);
                    if (!(secondValue >= 0 && secondValue <= 60))
                    {
                        isValid = false;
                    }

                }
            }

            return isValid;

        }

        /// <summary>
        /// Format the numeric value
        /// replace Comma "," with empty
        /// replace backslash (\) with empty
        /// replace "mm" with empty
        /// </summary>
        /// <param name="value">value to be formatted</param>
        /// <returns>formatted numeric value</returns>
        public static string FormatNumericValue(string value)
        {
            return value.Replace(",", "").Replace("\"", "").Replace("mm", "");
        }

        /// <summary>
        /// Remove the specified text which ends with from string
        /// </summary>
        /// <param name="value">string value</param>
        /// <param name="characters">string to be removed</param>
        /// <returns>returns the result string</returns>
        public static string RemoveSpecifiedTextFromString(string value, string characters)
        {
            if (!value.EndsWith(characters)) return value;

            return value.Replace(characters, "");
        }

        /// <summary>
        /// Check the string ends with specified character
        /// </summary>
        /// <param name="value">string value</param>
        /// <param name="characters">string value to be ends with</param>
        /// <returns>returns the result string</returns>
        public static bool IsEndsWithSpecifiedString(string value, string characters)
        {
            return value.EndsWith(characters);
        }

        #region Writing into File

        #region Constant Declarations

        /// <summary>
        /// Format of datetime while writing into file
        /// </summary>
        public const string TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss = "yyyy-MM-dd hhmmss";

        #endregion

        #region public static Properties

        #endregion

        #region public static Methods
        public static string GetFileContents(string filename)
        {
            if (File.Exists(filename) == false) { return string.Empty; }
            try
            {
                string contents = string.Empty;
                StreamReader reader = new StreamReader(filename);
                contents = reader.ReadToEnd();
                reader.Close();
                return contents;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ObjHelper.GetFileContents - ERROR: {0}", ex.Message);
            }
            return string.Empty;
        }

        public static List<string> GetFileLines(string filename)
        {
            List<string> lines = new List<string>();
            if (File.Exists(filename) == false) { return lines; }
            try
            {
                StreamReader reader = new StreamReader(filename);
                string line = reader.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ObjHelper.GetFileLines - ERROR: {0}", ex.Message);
            }
            return lines;
        }

        public static object GetObjectFromFile(string filepath, Type objType)
        {
            string data = string.Empty;
            try
            {
                StreamReader reader = new StreamReader(filepath);
                XmlTextReader xmlReader = new XmlTextReader(reader);
                string rv = string.Empty;
                System.Runtime.Serialization.DataContractSerializer ser = new System.Runtime.Serialization.DataContractSerializer(objType);
                object obj = ser.ReadObject(xmlReader);
                xmlReader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("ObjHelper.GetObjectFromFile - ERROR: {0}", ex.Message));
            }
            return null;
        }

        public static bool HasItems(IList<object> obj)
        {
            if ((obj == null) || (obj.Count == 0)) { return false; }
            return true;
        }

        public static void LogConsumerRequest(object itemToLog, string itemIdentifier)
        {
            WriteObjectToFile(itemToLog, string.Format("{0}{1} - {2}.xml", ConfigHelper.ConsumerRequestLogPath, itemIdentifier, DateTime.Now.ToString(TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss)));
        }

        public static void LogConsumerResponse(object itemToLog, string itemIdentifier)
        {
            WriteObjectToFile(itemToLog, string.Format("{0}{1} - {2}.xml", ConfigHelper.ConsumerResponseLogPath, itemIdentifier, DateTime.Now.ToString(TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss)));
        }

        public static void LogProviderRequest(object itemToLog, string itemIdentifier)
        {
            WriteObjectToFile(itemToLog, string.Format("{0}{1} - {2}.xml", ConfigHelper.ProviderRequestLogPath, itemIdentifier, DateTime.Now.ToString(TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss)));
        }

        public static void LogProviderResponse(object itemToLog, string itemIdentifier)
        {
            WriteObjectToFile(itemToLog, string.Format("{0}{1} - {2}.xml", ConfigHelper.ProviderResponseLogPath, itemIdentifier, DateTime.Now.ToString(TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss)));
        }

        public static void LogConsumerRequestJson(object itemToLog, string itemIdentifier)
        {
            WriteObjectToFileAsJson(itemToLog, string.Format("{0}{1} - {2}.json", ConfigHelper.ConsumerRequestLogPath, itemIdentifier, DateTime.Now.ToString(TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss)));
        }

        public static void LogConsumerResponseJson(object itemToLog, string itemIdentifier)
        {
            WriteObjectToFileAsJson(itemToLog, string.Format("{0}{1} - {2}.json", ConfigHelper.ConsumerResponseLogPath, itemIdentifier, DateTime.Now.ToString(TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss)));
        }

        public static void LogProviderRequestJson(object itemToLog, string itemIdentifier)
        {
            WriteObjectToFileAsJson(itemToLog, string.Format("{0}{1} - {2}.json", ConfigHelper.ProviderRequestLogPath, itemIdentifier, DateTime.Now.ToString(TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss)));
        }

        public static void LogProviderResponseJson(object itemToLog, string itemIdentifier)
        {
            WriteObjectToFileAsJson(itemToLog, string.Format("{0}{1} - {2}.json", ConfigHelper.ProviderResponseLogPath, itemIdentifier, DateTime.Now.ToString(TIMESTAMP_DASHED_DATE_yyyy_MM_dd_hhmmss)));
        }

        public static object ObjectFromXML(string str, Type objectType)
        {
            try
            {
                System.IO.StringReader reader = new System.IO.StringReader(str);
                XmlTextReader xmlReader = new XmlTextReader(reader);
                System.Runtime.Serialization.DataContractSerializer ser = new System.Runtime.Serialization.DataContractSerializer(objectType);
                object obj = ser.ReadObject(xmlReader);
                xmlReader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ObjHelper.ObjectFromXML - ERROR: {0}", ex.Message);
            }
            return null;
        }

        public static string ObjectToXmlDS(object obj)
        {
            string rv = string.Empty;
            try
            {
                System.Runtime.Serialization.DataContractSerializer ser = new System.Runtime.Serialization.DataContractSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                ser.WriteObject(stream, obj);
                stream.Position = 0;
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                rv = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                return rv;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("ObjHelper.ObjectToXml - ERROR: {0}", ex.Message));
            }
            return string.Empty;
        }

        public static string ObjectToXmlXS(object obj)
        {
            string rv = string.Empty;
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            ser.Serialize(stream, obj);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            rv = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return rv;
        }

        public static void WriteFileLines(List<string> lines, string filepath)
        {
            try
            {
                StreamWriter writer = new StreamWriter(filepath);
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ObjHelper.WriteFileLines - ERROR: {0}", ex.Message);
            }
        }

        public static void WriteObjectToFile(object item, string filepath)
        {
            string data = string.Empty;
            try
            {
                StreamWriter writer = new StreamWriter(filepath);
                writer.Write(ObjectToXmlDS(item));
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("ObjHelper.WriteObjectToFile - ERROR: {0}", ex.Message));
            }
        }

        public static void WriteObjectToFileAsJson(object item, string filepath)
        {
            string data = string.Empty;
            try
            {
                StreamWriter writer = new StreamWriter(filepath);
                writer.Write(JsonConvert.SerializeObject(item));
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("ObjHelper.WriteObjectToFile - ERROR: {0}", ex.Message));
            }
        }

        public static void WriteToFile(string fileContents, string filepath)
        {
            string data = string.Empty;
            try
            {
                StreamWriter writer = new StreamWriter(filepath);
                writer.Write(fileContents);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("ObjHelper.WriteToFile - ERROR: {0}", ex.Message));
            }
        }

        #endregion

        #endregion
    }
}
