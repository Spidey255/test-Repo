#region Header
// --------------------------------------------------------------------------------------
// <copyright file="VirtualForm.cs" company="Cognitive Platform Solutions Pvt Ltd">
// Reproduction or transmission, in whole or in part, in any form or 
// by any means including electronic or mechanical or otherwise, is 
// prohibited without written permission from Cognitive Platform Solutions Pvt Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------
#endregion

namespace CPS.Proof.DFSExtension
{
    using System.Collections.Generic;
    using System;
    using SRA.Proof.Helpers;
    using log4net;
    using System.Data;
    using SRA.Proof.Infrastructure;
    using SRA.Proof.Middleware;
    using System.Linq;
    using System.Text.Json;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Xml.Linq;
    using CPS.Proof.DFSExtension;
    using static System.Net.Mime.MediaTypeNames;
    using System.Text.RegularExpressions;
    using System.Threading;
    using SRA.Proof.Common;

    public abstract class VirtualForm : IVirtualPage
    {

        #region  private Constant

        /// <summary>
        /// A <see cref="string"/> that represents the year
        /// </summary>
        private const string YearConst = "yy";

        /// <summary>
        /// A <see cref="string"/> that represents the month
        /// </summary>
        private const string MonthConst = "mm";

        /// <summary>
        /// A <see cref="string"/> that represents the day
        /// </summary>
        private const string DayConst = "dd";

        /// <summary>
        /// A <see cref="string"/> that represents the hour
        /// </summary>
        private const string Hour = "hh";

        /// <summary>
        /// A <see cref="string"/> that represents the minute
        /// </summary>
        private const string Minute = "mi";

        /// <summary>
        /// A <see cref="string"/> that represents the second
        /// </summary>
        private const string Second = "ss";

        /// <summary>
        /// A <see cref="string"/> that represents the ComboBox
        /// Control Type
        /// </summary>
        private const string ComboBox = "ComboBox";

        /// <summary>
        /// A <see cref="System.String"/> that represents the MultiSelectComboBox
        /// Control Type
        /// </summary>
        private const string MultiSelectComboBox = "MultiSelectComboBox";

        /// <summary>
        /// A <see cref="string"/> that represents the Grid
        /// Control Type
        /// </summary>
        private const string Grid = "Grid";

        /// <summary>
        /// A <see cref="string"/> that represents the ListBox
        /// Control Type
        /// </summary>
        private const string ListBox = "ListBox";

        /// <summary>
        /// A <see cref="string"/> that represents the RadioButton
        /// Control Type
        /// </summary>
        private const string RadioButton = "RadioButton";

        /// <summary>
        /// A <see cref="string"/> that represents the NumericTextBox
        /// Control Type
        /// </summary>
        private const string NumericTextBox = "NumericTextBox";

        /// <summary>
        /// A <see cref="string"/> that represents the CheckBox
        /// Control Type
        /// </summary>
        private const string CheckBox = "CheckBox";

        /// <summary>
        /// A <see cref="string"/> that represents the JSON
        /// Control Type
        /// </summary>
        private const string JSON = "JSON";

        /// <summary>
        /// A <see cref="string"/> that represents the DateTimePicker
        /// Control Type
        /// </summary>
        private const string DateTimePicker = "DateTimePicker";

        /// <summary>
        /// A <see cref="string"/> that represents document type.
        /// </summary>
        private const string DocumentType = "DocumentType";

        /// <summary>
        /// A <see cref="string"/> that represents gvDestinationActivity.
        /// </summary>
        private const string gvDestinationActivity = "gv_destinationactivity";

        /// <summary>
        /// A <see cref="string"/> that represents ISpacePage.0
        /// </summary>0
        private const string ISpacePage = "ISpaceExt";

        /// <summary>
        /// A <see cref="string"/> that represents ISpaceUnsigned.
        /// </summary>
        private const string ISpaceUnsigned = "ISpaceUnsigned";

        /// <summary>
        /// A <see cref="string"/> that represents ISpaceAnonymous.
        /// </summary>
        private const string ISpaceAnonymous = "ISpaceAnonymous";

        /// <summary>
        /// A <see cref="string"/> that represents ISpaceUrlPath.
        /// </summary>
        private const string ISpaceUrlPath = "{0}?SesId={1}&PkActMId={2}&fEId={3}&PkPrMId={4}&fVId={5}";

        /// <summary>
        /// A <see cref="string"/> that represents BaseUrl.
        /// </summary>
        private const string BaseUrlPath = "BaseUrl";


        /// <summary>
        /// A <see cref="string"/> that represents BaseUrl.
        /// </summary>
        private const string StandardDateFormat = "MM/dd/yyyy HH:mm:ss";

        /// <summary>
        /// A <see cref="string"/> that represents customDateFormats.
        /// </summary>
        private string[] customDateFormats = { "M/dd/yyyy h:mm:ss tt", "MM/dd/yyyy HH:mm:ss",
            "MM/dd/yyyy HH.mm.ss","MM-dd-yyyy HH:mm:ss","dd/MM/yyyy HH:mm:ss","dd/MM/yyyy hh:mm:ss","dd-MM-yyyy HH:mm:ss","dd-MM-yyyy hh:mm:ss",
            "dd/MM/yyyy hh:mm:ss tt", "dd/MM/yyyy h:mm:ss tt","MM-dd-yyyy HH.mm.ss",
            "d/MM/yyyy h:mm:ss tt", "M/d/yyyy h:mm:ss tt","MM/dd/yyyy h:mm:ss tt",
                "MM/d/yyyy h:mm:ss tt",
            "dd-MMM-yy hh:mm:ss tt",
                    "dd-MMM-yy h:mm:ss tt", "d-MMM-yy h:mm:ss tt",
                   "yyyy-MM-dd'T'hh:mm:ss", "yyyy-MM-dd'T'HH:mm:ss", "yyyy-MM-dd hh:mm:ss","yyyy-MM-dd HH:mm:ss"};


        #endregion

        #region Private Members

        /// <summary>
        /// Represents a private member to hold grid commands.
        /// </summary>
       
        /// <summary>
        /// A <see cref="List{I}"/> where I represents the
        /// <see cref="string"/> containing the months of the year
        /// </summary>
        private readonly List<string> _months = new List<string>(new[]
        {
            "January",

            "February",

            "March",

            "April",

            "May",

            "June",

            "July",

            "August",

            "September",

            "October",

            "November",

            "December"
        });



      

        // A private member to hold searched objects for reusability.
        private Dictionary<string, object> _tempObjects;

        /// <summary>
        /// Member variable for Log instance
        /// </summary>
        private readonly ILog _log;

        private string _identifier = null;
      

        public IExtObjectFactory _objectFactory { get; set; }       


        #endregion


        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public VirtualForm()
        {
            _log = LogManager.GetLogger(GetType());

            if (_tempObjects == null)
                _tempObjects = new Dictionary<string, object>();
        }

        #endregion

        #region Internal Methods


        

        /// <summary>
        /// Represents the method that executes given method name.
        /// </summary>
        /// <param name="methodName">
        /// A <see cref="IElementMetadata"/> that holds methods name.
        /// </param>
        public abstract void ExecuteMethod(string action,string element, 
            ref Dictionary<string, ServiceElementData> dfsparams);        
       

        public void WriteDebugInfo(string message, Exception ex = null)
        {
            if (ex != null)
            {
                _log.Debug(message, ex);

                return;
            }

            _log.Debug(message);
        }

        public Dictionary<string, string> GetQueryExpressionDataSource(string expressionId)
        {
            return _objectFactory.
                          GetQueryExpressionDataSource(expressionId);
        }

        public void WriteErrorInfo(string message, Exception ex = null)
        {
            if (ex != null)
            {
                _log.Error(message, ex);

                return;
            }

            _log.Error(message);
        }

        /// <summary>
        /// Represents a method that checks valid PostalCode.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string"/> that represents element value.
        /// </param>
        ///  <returns >it returns if tha PostalCode is correct else returns false</returns>
        public bool IsValidPostalCode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            //to do logic
            bool result = false;
            string regExPattern = @"^[1-9][0-9]{5}$";

            result = Regex.IsMatch(value, regExPattern, RegexOptions.IgnoreCase);

            return result;
        }


        /// <summary>
        ///  Method for RegularExpression
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="regExPattern"></param>
        /// <param name="index"></param> 
        /// <returns></returns>
        public string RegularExpression(dynamic inputString, string regExPattern, string index)
        {
            if (String.IsNullOrEmpty(inputString))
                return null;

            if (String.IsNullOrEmpty(regExPattern))
                return null;

            Match match = Regex.Match(inputString, regExPattern);

            if (match.Success)
                return match.Groups[index].Value;
            else
                return null;
        }

        /// <summary>
        /// Represents a method the sets property and value to control.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string"/> that represents element name.
        /// </param>
        ///  <returns >it returns if tha email is correct else returns false</returns>
        public bool IsValidEmail(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            //to do logic
            bool result = false;
            string regExPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$";

            result = Regex.IsMatch(value, regExPattern, RegexOptions.IgnoreCase);

            return result;
        }

        /// <summary>
        /// Represents a method to get current date time.
        /// </summary>
        /// <returns></returns>
        public DateTime CurrentDate()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

            culture.DateTimeFormat.ShortDatePattern = "";

            culture.DateTimeFormat.LongTimePattern = StandardDateFormat;

            Thread.CurrentThread.CurrentCulture = culture;

            return DateTime.Now;
        }

        public DataTable GetComboDetail()
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// Represents a method to get string.Empty.
        ///// </summary>
        //public string Empty()
        //{
        //    return string.Empty;
        //}


        #region String Functions

        /// <summary>
        /// Represents a method to get the substring.
        /// </summary>
        public string SubString(string originalString, int startIndex, int length)
        {
            if (originalString == null) return null;

            return originalString.Substring(startIndex, length);
        }

        /// <summary>
        /// Method converts the input string to upper case.
        /// </summary>
        public string Upper(string originalString)
        {
            if (originalString == null) return null;

            return originalString.ToUpper();
        }

        /// <summary>
        /// Method converts the input string to lower case.
        /// </summary>
        public string Lower(string originalString)
        {
            if (originalString == null) return null;

            return originalString.ToLower();
        }

        /// <summary>
        /// Represents a method to convert a string to 
        /// camel case
        /// </summary>
        public string CamelC(string originalString)
        {
            originalString = PascalC(originalString);

            return originalString.Substring(0, 1).ToLower() +
                    originalString.Substring(1);
        }

        /// <summary>
        /// Represents a method to convert a string to
        /// pascal case.
        /// </summary>
        public string PascalC(string originalString)
        {
            var info = Thread.CurrentThread.CurrentCulture.TextInfo;

            originalString = info.ToTitleCase(originalString);

            var parts = originalString.Split(new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            return String.Join(String.Empty, parts);
        }

        /// <summary>
        /// Represents a method to convert a string to 
        /// proper case
        /// </summary>
        public string ProperC(string originalString)
        {
            _log.DebugFormat("ProperCasing for the value {0}", originalString);

            var info = Thread.CurrentThread.CurrentCulture.TextInfo;

            if (info == null)
            {
                _log.Debug("Info is null");
                return "";
            }

            if (string.IsNullOrWhiteSpace(originalString))
            {
                _log.Debug("originalString is null or empty");
                return "";
            }

            originalString = info.ToTitleCase(originalString.ToLower());

            _log.DebugFormat("Value after prooper casing is {0}", originalString);

            return originalString;
        }

        /// <summary>
        /// Represents a method to get string.Empty.
        /// </summary>
        public string Empty()
        {
            return string.Empty;
        }

        /// <summary>
        /// Represents a method to get new GUID.
        /// </summary>
        public string NewId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Represents a method to convert a string to 
        /// Encrypted data.
        /// </summary>
        public string Encrypt(string originalString)
        {
            var cryptoManager
                = ObjectManager.Acquire<ICryptoManager>();

            try
            {
                return cryptoManager.Encrypt(originalString);
            }
            finally
            {
                if (cryptoManager != null)
                    ObjectManager.Release(cryptoManager);
            }
        }

        /// <summary>
        /// Represents a method to convert a string to 
        /// Decrypted data.
        /// </summary>
        public string Decrypt(string originalString)
        {
            var cryptoManager
                = ObjectManager.Acquire<ICryptoManager>();

            try
            {
                return cryptoManager.Decrypt(originalString);
            }
            finally
            {
                if (cryptoManager != null)
                    ObjectManager.Release(cryptoManager);
            }
        }

        /// <summary>
        /// Method returns the length of the string.
        /// </summary>
        public int Length(string originalString)
        {
            if (originalString == null) return 0;

            return originalString.Length;
        }

        /// <summary>
        /// Method used to replace the string.
        /// </summary>
        public string Replace
            (string originalString, string oldString, string replaceString)
        {
            if (originalString == null) return null;

            if (replaceString == null) return null;

            return originalString.Replace(oldString, replaceString);
        }

        /// <summary>
        /// Method to trim a string.
        /// </summary>
        public string Trim(string originalString)
        {
            if (originalString == null) return null;

            return originalString.Trim();
        }

        /// <summary>
        ///  Method for contains
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchText"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public bool Contains(string inputString, string searchText, bool caseSensitive = false)
        {
            if (String.IsNullOrEmpty(inputString))
                return false;

            if (String.IsNullOrEmpty(searchText))
                return false;

            bool result = false;

            if (caseSensitive)
                result = inputString.Contains(searchText);
            else
            {
                result = Regex.IsMatch(inputString, searchText, RegexOptions.IgnoreCase);
            }

            return result;
        }

        /// <summary>
        ///  Method for IndexOf
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="searchText"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="startIndex"></param>      
        /// <returns></returns>
        public int IndexOf(string inputString, string searchText,
            int startIndex, int length, bool caseSensitive = false)
        {
            int result = -1;

            if (String.IsNullOrEmpty(inputString))
                return result;

            if (String.IsNullOrEmpty(searchText))
                return result;

            if (caseSensitive)
                result = inputString.IndexOf(searchText, startIndex);
            else
            {
                result = inputString.IndexOf(searchText, startIndex,
                    StringComparison.CurrentCultureIgnoreCase);
            }

            return result;
        }

        /// <summary>
        ///  Method for Insert
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="insertText"></param>
        /// <param name="index"></param> 
        /// <returns></returns>
        public string Insert(string inputString, string insertText, int index)
        {
            if (String.IsNullOrEmpty(inputString))
                return null;

            if (String.IsNullOrEmpty(insertText))
                return null;

            return inputString.Insert(index, insertText);
        }

        /// <summary>
        ///  Method for CommaSeparatedValues
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="delimiter"></param>
        /// <param name="index"></param> 
        /// <returns></returns>
        public string CommaSeparatedValues(string inputString, string delimiter, int index)
        {
            if (String.IsNullOrEmpty(inputString))
                return null;

            if (String.IsNullOrEmpty(delimiter))
                delimiter = ",";

            var delimiterChar = delimiter.ToCharArray()[0];

            var result = inputString.Split(delimiterChar);

            if (result.Length > index)
                return result[index];
            else
                return null;
        }

        /// <summary>
        ///  Method for RegularExpression
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="regExPattern"></param>
        /// <param name="index"></param> 
        /// <returns></returns>
        public string RegularExpression(string inputString, string regExPattern, string index)
        {
            if (String.IsNullOrEmpty(inputString))
                return null;

            if (String.IsNullOrEmpty(regExPattern))
                return null;

            Match match = Regex.Match(inputString, regExPattern);

            if (match.Success)
                return match.Groups[index].Value;
            else
                return null;
        }


        /// <summary>
        ///  Method for IsNullOrEmpty
        /// </summary>
        /// <param name="input"></param> 
        /// <returns></returns>
        public bool IsNullOrEmpty(dynamic input)
        {
            if (input == null)
                return true;

            if (IsDictionary(input))
                return false;

            return String.IsNullOrEmpty(input.ToString());
        }

        /// <summary>
        /// Represents the method that checks
        /// whether given object is dictionary or not
        /// </summary>
        /// <param name="obj">
        /// A <see cref="object"/> that holds
        /// object
        /// </param>
        /// <returns></returns>
        public bool IsDictionary(object obj)
        {
            if (obj == null) return false;

            return obj is Dictionary<string, string>;
        }

        /// <summary>
        ///  Method for StringCompare
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="compareString"></param>
        /// <param name="caseSensitive"></param> 
        /// <returns></returns>
        public bool StringCompare(string inputString, string compareString, bool caseSensitive)
        {
            if (caseSensitive)
                return String.Equals(inputString, compareString);
            else
                return String.Equals(inputString, compareString, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        ///  Method for StringFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>       
        /// <returns></returns>
        public string StringFormat(string format, object[] args)
        {
            if (String.IsNullOrEmpty(format))
                return null;

            return String.Format(format, args);
        }

        #endregion


        #region Date Functions

        /// <summary>
        /// Represents a method to get current date time.
        /// </summary>
        /// <returns></returns>
        //public DateTime CurrentDate()
        //{
        //    CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

        //    culture.DateTimeFormat.ShortDatePattern = "";

        //    culture.DateTimeFormat.LongTimePattern = StandardDateFormat;

        //    Thread.CurrentThread.CurrentCulture = culture;

        //    return DateTime.Now;
        //}

        /// <summary>
        /// Represents a method to get current UTC date time.
        /// </summary>
        /// <returns></returns>
        public DateTime UTCDate()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// Method to add days.
        /// </summary>
        public DateTime AddDay
            (DateTime originalDate, int days)
        {
            return originalDate.AddDays(days);
        }

        /// <summary>
        /// Method to add year.
        /// </summary>
        public DateTime AddYear
            (DateTime originalDate, int year)
        {
            return originalDate.AddYears(year);
        }

        /// <summary>
        /// Method to add year.
        /// </summary>
        public DateTime AddMonth
            (DateTime originalDate, int month)
        {
            return originalDate.AddMonths(month);
        }

        /// <summary>
        /// Method to add hours.
        /// </summary>
        public DateTime AddHour
            (DateTime originalDate, double hour)
        {
            return originalDate.AddHours(hour);
        }

        /// <summary>
        /// Method to add minute.
        /// </summary>
        public DateTime AddMinute
            (DateTime originalDate, double minute)
        {
            return originalDate.AddMinutes(minute);
        }

        /// <summary>
        /// Method to add seconds.
        /// </summary>
        public DateTime AddSeconds
            (DateTime originalDate, double seconds)
        {
            return originalDate.AddSeconds(seconds);
        }

        /// <summary>
        /// Method to get day
        /// </summary>
        public int Day(DateTime originalDate)
        {
            return originalDate.Day;
        }

        /// <summary>
        /// Method to get day of week
        /// </summary>
        public int DayOfWeek(DateTime originalDate)
        {
            return (int)originalDate.DayOfWeek;
        }

        /// <summary>
        /// Method to get day of week
        /// </summary>
        public string DayOfWeekString
            (DateTime originalDate)
        {
            var DayOfWeekString = originalDate.DayOfWeek.ToString();
            //var resourceManager = new ResourceManager
            //   (typeof(VirtualEventHandlerStrings));

            //return resourceManager.GetString(DayOfWeekString);
            return DayOfWeekString;
        }

        /// <summary>
        /// Method to get day of year
        /// </summary>
        public int DayOfYear(DateTime originalDate)
        {
            return originalDate.DayOfYear;
        }

        /// <summary>
        /// Method to get month
        /// </summary>
        public int Month(DateTime originalDate)
        {
            return originalDate.Month;
        }

        /// <summary>
        /// Method to get Hour
        /// </summary>
        public int GetHour(DateTime originalDate)
        {
            return originalDate.Hour;
        }

        /// <summary>
        /// Method to get Minute
        /// </summary>
        public int GetMinute(DateTime originalDate)
        {
            return originalDate.Minute;
        }

        /// <summary>
        /// Method to get Seconds
        /// </summary>
        public int GetSeconds(DateTime originalDate)
        {
            return originalDate.Second;
        }

        /// <summary>
        /// Method to get month string
        /// </summary>
        public string MonthString(DateTime originalDate)
        {
            var month = _months[originalDate.Month - 1];

            //var resourceManager = new ResourceManager
            //   (typeof(VirtualEventHandlerStrings));

            //return resourceManager.GetString(month);
            return month;



        }

        /// <summary>
        /// Method to get Year
        /// </summary>
        public int Year(DateTime originalDate)
        {
            return originalDate.Year;
        }

        /// <summary>
        /// Method to find the data difference between two dates.
        /// </summary>
        public int DateCompare(string datePart, DateTime date1, DateTime date2)
        {



            switch (datePart)
            {
                case YearConst:
                    var zeroTime = new DateTime(1, 1, 1);

                    var timeSpan = (date2 - date1);

                    if (timeSpan.Days <= 0)
                    {
                        if (date1 > date2)
                            return -Math.Abs((zeroTime + (date1 - date2)).Year - 1);
                        else
                            return -Math.Abs((zeroTime + (date2 - date1)).Year - 1);
                    }

                    return (zeroTime + timeSpan).Year - 1;

                case MonthConst:
                    return ((date2.Year - date1.Year) * 12) +
                           date2.Month - date1.Month;

                case DayConst:
                    date1 = date1.Date;
                    date2 = date2.Date;
                    return (date2 - date1).Days;

                case Hour:
                    return Convert.ToInt32(Math.Truncate((date2 - date1).TotalHours));

                case Minute:
                    return Convert.ToInt32(Math.Truncate((date2 - date1).TotalMinutes));

                case Second:
                    return Convert.ToInt32(Math.Truncate((date2 - date1).TotalSeconds));
            }
            return -1;
        }

        # endregion

        # region Math Functions



        /// <summary>
        /// Method to round off the decimal values to decimals.
        /// </summary>
        public dynamic Round(dynamic input, int decimals)
        {
            return Math.Round(input, decimals);
        }

        /// <summary>
        /// Method to round off the decimal values to integer.
        /// </summary>
        public dynamic Round(dynamic input)
        {
            return Math.Round(input);
        }

        /// <summary>
        /// Method to Floor the decimal values to integer.
        /// </summary>
        public dynamic Floor(dynamic input)
        {
            return Math.Floor(input);
        }

        /// <summary>
        /// Method to Ceiling the decimal values to integer.
        /// </summary>
        public dynamic Ceiling(dynamic input)
        {
            return Math.Ceiling(input);
        }

        /// <summary>
        /// Method to Absolute the decimal values to integer.
        /// </summary>
        public dynamic Absolute(dynamic input)
        {
            return Math.Abs(input);
        }

        /// <summary>
        /// Method to SquareRoot the double values to integer.
        /// </summary>
        public double SquareRoot(double input)
        {
            return Math.Sqrt(input);
        }

        # endregion

        # region Conversion Functions

        /// <summary>
        /// Method to convert string to integer
        /// </summary>
        public int ToInteger(string originalString)
        {
            try
            {
                return originalString == null ? 0 : Convert.ToInt32(originalString);
            }
            catch (Exception ex)
            {
                _log.Error("Error in ToInteger Converison", ex);

                return 0;
            }
        }

        /// <summary>
        /// Represents a property to convert object to 
        /// string.
        /// </summary>
        public string Tostring(dynamic input)
        {
            if (input == null) return null;

            return input.ToString();
        }

        /// <summary>
        /// Method to convert string to bool
        /// </summary>
        public bool ToBoolean(string originalString)
        {
            if (originalString.ToUpper() == "TRUE" || originalString == "1")
                return true;

            return false;
        }

        /// <summary>
        /// Method to convert string to DateTime
        /// </summary>
        public DateTime ToDateTime(string originalString)
        {
            try
            {
                return DateTime.ParseExact(originalString, customDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }

            catch (Exception ex)
            {
                _log.Error("Error in ToDateTime Converison", ex);

                return DateTime.Now;
            }
        }

        /// <summary>
        /// method to convert inputdate to specified dateformat
        /// </summary>
        /// <param name="input">A<see cref="System.DateTime"/>holds the date</param>
        /// <param name="format">A<see cref="string"/>holds the date format</param>
        /// <returns>A<see cref="string"/> returns formated datetime</returns>
        public string DateFormatS(DateTime input, string format)
        {
            try
            {
                return input.ToString(format);
            }

            catch (Exception ex)
            {
                _log.Error("Error in DateFormatToString Converison", ex);

                return input.ToString();
            }
        }

        /// <summary>
        /// method to convert utcdate to specified timezone
        /// </summary>
        /// <param name="input">A<see cref="System.DateTime"/>
        /// holds the utcdate</param>
        /// <param name="format">A<see cref="string"/>
        /// holds the timezone id</param>
        /// <returns>A<see cref="System.DateTime"/> 
        /// returns Converted zonedatetime</returns>
        public DateTime ToTimeZone(DateTime utcDate, string timeZone)
        {
            try
            {
                var userZone = TimeZoneInfo.FindSystemTimeZoneById
                      (timeZone);

                return TimeZoneInfo.ConvertTimeFromUtc
                    (utcDate, userZone);
            }

            catch (Exception ex)
            {
                _log.Error("Error in TimeZone Converison", ex);

                return utcDate;
            }
        }

        /// <summary>
        /// Method to convert dynamic to short
        /// </summary>
        public short ToShort(dynamic input)
        {
            if (input == null) return default(short);

            try
            {
                return Convert.ToInt16(input);
            }

            catch (Exception ex)
            {
                _log.Error("Error in ToShort Converison", ex);

                return default(short);
            }
        }

        /// <summary>
        /// Method to convert dynamic to Long
        /// </summary>
        public long ToLong(dynamic input)
        {
            if (input == null) return default(long);
            try
            {
                return Convert.ToInt64(input);
            }

            catch (Exception ex)
            {
                _log.Error("Error in ToLong Converison", ex);

                return default(long);
            }
        }

        /// <summary>
        /// Method to convert dynamic to decimal
        /// </summary>
        public decimal ToDecimal(dynamic input)
        {
            if (input == null) return default(decimal);
            try
            {
                return Convert.ToDecimal(input);

            }

            catch (Exception ex)
            {
                _log.Error("Error in ToDecimal Converison", ex);

                return default(decimal);
            }
        }

        /// <summary>
        /// Method to convert dynamic to double
        /// </summary>
        public double ToDouble(dynamic input)
        {
            if (input == null) return default(double);

            try
            {
                return Convert.ToDouble(input);
            }

            catch (Exception ex)
            {
                _log.Error("Error in ToDecimal Converison", ex);

                return default(double);
            }
        }

        #endregion

        public Dictionary<short, object> ExecuteQuery
               (string expressionId, string query, bool? isLookup)
        {
            

            string connectionId = string.Empty;

            IExternalQueryController externalQueryController = null;

            Dictionary<short, object> queryResults = null;

            try
            {

                var querySourceDetails = _objectFactory.
                           GetQueryExpressionDataSource(expressionId);

                connectionId = querySourceDetails.Select
                            (item => item.Value).FirstOrDefault();

              
                string proofConnectionId = querySourceDetails.Select
                     (item => item.Key).FirstOrDefault();

                var connectionString = GetAppConnectionString
                    (proofConnectionId, connectionId);


                if (connectionString == null)
                {
                    _log.Error("Connection String is null or empty");

                    _log.Error("Exiting ExecuteHttpRequest");

                    return null;
                }

                 externalQueryController = 
                    
                    ObjectManager.Acquire<IExternalQueryController>();

                externalQueryController.ExecuteQuery
                    (connectionString.Connection,query,out queryResults);


                return queryResults;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if(externalQueryController !=null)
                {
                    ObjectManager.Release<IExternalQueryController>(externalQueryController);
                }

            }
        }

        public List<Dictionary<string, string>>? GetElementClientData
            (string instanceId, string widgetId)
        {
            IExternalQueryController dFSExecution = null;

            string formInstanceData = null;

            try
            {
                dFSExecution = ObjectManager.Acquire<IExternalQueryController>();

                
                var status = dFSExecution.GetElementClientData
                    (instanceId, widgetId, out formInstanceData);

                var lst = JObject.Parse(formInstanceData)["rows"];


                return  ConvertjsonToDataTable(lst.ToString());
                
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                if (dFSExecution != null)
                    ObjectManager.Release<IExternalQueryController>(dFSExecution);

            }
        }


        public List<Dictionary<string, string>>? ConvertjsonToDataTable(string jsonData)
        {

            var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonData);

         
            return keyValuePairs;
        }


       

        /// <summary>
        /// Represents a method used to get repository column data.
        /// </summary>
        /// <param name="elementData">
        /// </param>
        /// <param name="columnName"></param>
        /// <param name="isPrimaryKey"></param>
        /// <param name="isIdentity"></param>
        /// <returns></returns>
        public RepositoryColumn GetRepositoryColumnObject(string elementData,
            string columnName, bool isPrimaryKey, bool? isIdentity, int dataType)
        {
            var repositoryColumn = new RepositoryColumn();

            try
            {

                

                    _log.DebugFormat("Entering GetRepositoryColumnObject for column name:{0}"
                        , columnName);

                    repositoryColumn.IsKey = isPrimaryKey;
                    repositoryColumn.IsIdentity = (isIdentity.HasValue) ? isIdentity.Value : false;

                repositoryColumn.Value = elementData;
                repositoryColumn.DataType = (DataTypes)dataType;
                repositoryColumn.Name = columnName;
               
                return repositoryColumn;


                /*  if (string.IsNullOrEmpty(elementData.Value))
                  {
                      elementData.Value = (elementData.ECT == ControlType.NumericTextBox.ToString()) ?
                      Convert.ToString(elementData.DV) : elementData.DT;
                  }

                  if (elementData.ECT == ControlType.ComboBox.ToString())
                  {
                      if (!String.IsNullOrEmpty(elementData.ComboSelectedValue))
                          repositoryColumn.Value = elementData.ComboSelectedValue;
                      else if (!String.IsNullOrEmpty(elementData.Value))
                          repositoryColumn.Value = elementData.Value;
                  }

                  else if (elementData.ECT == ControlType.RadioButton.ToString())
                  {
                      repositoryColumn.Value = (elementData.DVl != null && elementData.DVl.Count > 0) ?
                          elementData.DVl.Keys.FirstOrDefault() : elementData.Value;
                  }

                  else if (elementData.ECT == ControlType.DocumentType.ToString())
                  {
                      if (elementData.RType == 5 || elementData.RType == 6 || elementData.RType == 7)
                      {
                          string buildElemData = string.Empty;

                          repositoryColumn.Value = null;

                          if (elementData.DVl != null && elementData.DVl.Count > 0)
                          {
                              foreach (var elem in elementData.DVl)
                              {
                                  buildElemData = string.Format("{0}{1}#{2},", buildElemData, elem.Key, elem.Value);
                              }

                              buildElemData = buildElemData.Remove(buildElemData.Length - 1, 1);

                              repositoryColumn.Value = buildElemData;
                          }
                      }
                      else
                      {
                          repositoryColumn.Value = (elementData.DVl != null
                          && elementData.DVl.Count > 0) ?
                          elementData.DVl.Keys.FirstOrDefault() : null;
                      }
                  }

                  else if (elementData.ECT == ControlType.CheckBox.ToString())
                  {
                      repositoryColumn.Value = (string.IsNullOrEmpty(elementData.Value))
                      ? false : ((elementData.Value) == "0" ||
                      (elementData.Value).ToLower() == "false") ? false : true;
                  }

                  else if (elementData.ECT == ControlType.ListBox.ToString() ||
                      elementData.ECT == ControlType.MultiSelectComboBox.ToString())
                  {
                      if (elementData.DVl != null && elementData.DVl.Count > 0)
                          foreach (var listItem in elementData.DVl)
                          {
                              var itemValue = String.Format("{0}#{1}", listItem.Key, listItem.Value);
                              if (repositoryColumn.Value == null ||
                               repositoryColumn.Value.ToString().Length <= 0)
                                  repositoryColumn.Value = itemValue;
                              else
                                  repositoryColumn.Value = String.Format("{0},{1}",
                                   repositoryColumn.Value, itemValue);
                          }
                      else
                      {
                          repositoryColumn.Value = elementData.Value;
                      }
                  }

                  else
                  {
                      repositoryColumn.Value = elementData.Value;
                  }
                  repositoryColumn.DataType = (DataTypes)dataType;
                  repositoryColumn.Name = columnName;
                  repositoryColumn.IEnc = elementData.IEnc ?? false;


                  return repositoryColumn;
                }
           */


            }

            catch (Exception ex)
            {
                _log.Error("Error executing GetRepositoryColumnObject method", ex);

                return null;
            }
            return null;
        }


        //Retrives Application Connectionstring By ConnectionId
        //from Redis Cache
        private ConnectionString GetAppConnectionString
            (string proofConnectionId, string appConnectionId)
        {
            IExternalQueryController appConnectionStrings = null;

            try
            {


                appConnectionStrings = ObjectManager.Acquire<IExternalQueryController>();

                if (appConnectionStrings == null)
                {

                    return null;
                }

                return appConnectionStrings.ConnectionById
                    (proofConnectionId, appConnectionId);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (appConnectionStrings != null)
                    ObjectManager.Release<IExternalQueryController>(appConnectionStrings);
            }
        }

        /// <summary>
        /// Represents a method used to bind query results to
        /// elements based on specified index.
        /// </summary>
        /// <param name="queryExpressionId"></param>
        /// <returns></returns>
        public Dictionary<short, object> ExecuteQueryBinding
            (string queryExpressionId, Dictionary<short, object> dic)
        {
            
            try
            {
               
                _log.Debug("Entering ExecuteQueryBinding");
            
                return new Dictionary<short, object>();

                _log.Debug("Exiting ExecuteQueryBinding");

                
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Error in ExecuteQueryBinding, {0}", ex);

               

                return null;
            }
            finally
            {
                
            }

            return null;
        }


        public DataTable SetGridDataSource(string expressionId, string query)
        {
           
            string connectionId = string.Empty;

            IExternalQueryController externalQueryController = null;

            //Getting the grid data source
            DataTable queryResults = null;

            try
            {

                var querySourceDetails = _objectFactory.
                           GetQueryExpressionDataSource(expressionId);

                connectionId = querySourceDetails.Select
                            (item => item.Value).FirstOrDefault();


                string proofConnectionId = querySourceDetails.Select
                     (item => item.Key).FirstOrDefault();

                var connectionString = GetAppConnectionString
                    (proofConnectionId, connectionId);


                if (connectionString == null)
                {
                    _log.Error("Connection String is null or empty");

                    _log.Error("Exiting ExecuteHttpRequest");

                    return null;
                }

                externalQueryController =

                   ObjectManager.Acquire<IExternalQueryController>();

                externalQueryController.GetGridDataSource
                    (connectionString, query, out queryResults);


                return queryResults;

               
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                if(externalQueryController != null)
                {
                    ObjectManager.Release<IExternalQueryController>(externalQueryController);
                }

            }
            }


        public object? GetObjectValue(object? obj)
        {
            try
            {
                switch (obj)
                {
                    case null:
                        return "NULL";
                    case JsonElement jsonElement:
                        {
                            var typeOfObject = jsonElement.ValueKind;
                            var rawText = jsonElement.GetRawText(); // Retrieves the raw JSON text for the element.

                            return typeOfObject switch
                            {
                                JsonValueKind.Number => float.Parse(rawText, CultureInfo.InvariantCulture),
                                JsonValueKind.String => obj.ToString(), // Directly gets the string.
                                JsonValueKind.True => true,
                                JsonValueKind.False => false,
                                JsonValueKind.Null => null,
                                JsonValueKind.Undefined => null, // Undefined treated as null.
                                JsonValueKind.Object => rawText, // Returns raw JSON for objects.
                                JsonValueKind.Array => rawText, // Returns raw JSON for arrays.
                                _ => rawText // Fallback to raw text for any other kind.
                            };
                        }
                    default:
                        throw new ArgumentException("Expected a JsonElement object", nameof(obj));
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public void SetGridData(DataTable queryresult,
            List<Triplet<string, short, short?>> bindings,string elementId, ref Dictionary<string, ServiceElementData> ISpace)
        {
            try
            {

              
                ServiceElementData serviceElementData = new ServiceElementData { ElementId = elementId };

                serviceElementData.Child = new List<ServiceElementData>();

                short rowsequence = 1;

                foreach (DataRow row in queryresult.Rows)
                {
                   

                   

                    var rowItem = new ServiceElementData();

                    rowItem.Child = new List<ServiceElementData>();

                    rowItem.RwId = Guid.NewGuid().ToString();
                    var indexer = bindings.GetEnumerator();
                    while (indexer.MoveNext())
                    {
                        short sequence = 0;

                        foreach (var item in row.ItemArray)
                    {                        
                        

                       
                            if(indexer.Current.SecondValue==sequence)
                            {

                                var gridcolumn = new ServiceElementData();
                               
                                gridcolumn.ElementId = indexer.Current.FirstValue;

                                gridcolumn.Value = item.ToString();

                                rowItem.Child.Add(gridcolumn);

                            }
                           
                            sequence++;
                        }
                        
                    }

                    rowItem.SEQ = rowsequence;

                    serviceElementData.Child.Add(rowItem);

                    rowsequence++;
                }

                if(ISpace == null)
                    ISpace = new Dictionary<string, ServiceElementData>();

                ISpace.Add(elementId, serviceElementData);

            }
            catch(Exception ex)
            {

            }
        }




        #endregion

       





    }
}
