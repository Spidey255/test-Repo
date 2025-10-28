namespace CPS.Proof.DFSExtension
{
    using System;
    using System.Collections.Generic;
    using log4net;
    using SRA.Proof.Infrastructure;

    /// <summary>
    /// Represents a class for retrieval of Application Settings.
    /// </summary>
    public class AppParams
    {
        /// <summary>
        /// A <seealso cref="ILog"/> that holds the Log object.
        /// </summary>
        private static ILog _sysLog = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        static AppParams()
        {
            _sysLog = _sysLog = LogManager.GetLogger(typeof(AppParams));
        }

        /// <summary>
        /// Represents a method to refresh the settings.
        /// </summary>
        public static void Refresh()
        {
        }

        /// <summary>
        /// Represents a method to get the
        /// Param Value
        /// </summary>
        /// <param name="key">
        /// A string representing the key.
        /// </param>
        /// <returns>
        /// Param value otherwise null.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if the passed in key is null or empty.
        /// </exception>
        public static string GetValue(string key)
        {
            return AppConfigurationBuilder.GetConnectionString(key);
        }

        /// <summary>
        /// Reperesent the method of Bulid Root Section
        /// </summary>
        /// <param name="path"></param>
        public static void BuildRootSection(string path)
        {
            AppConfigurationBuilder.BuildRootSection(path);
        }

        /// <summary>
        /// Reperesent the method of Bulid Root Section
        /// </summary>
        /// <param name="path"></param>
        public static void BuildApplicationRootSection(string path)
        {
            AppConfigurationBuilder.BuildApplicationRootSection(path);
        }

        /// <summary>
        /// Reperesent the method of Bulid Root Section
        /// </summary>
        /// <param name="path"></param>
        public static void BuildApplicationConnectionRootSection(string path)
        {
            AppConfigurationBuilder.BuildApplicationConnectionRootSection(path);
        }

        /// <summary>
        /// Represent the method of get app value
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns the values of Get app values</returns>
        public static string GetAppValue(string key)
        {
            return AppConfigurationBuilder.GetAppSettings(key);
        }

        /// <summary>
        /// Represent the method of Get application path
        /// </summary>
        /// <returns>Returns the values of Get application path</returns>
        public static string GetApplicationPath()
        {
            return AppConfigurationBuilder.GetAppSettings("AppPath");
        }

        /// <summary>
        /// Represent the method of Get value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="converter"></param>
        /// <returns>Returns the values</returns>
        public static T GetValue<T>(string key, Converter<object, T> converter)
        {
            _sysLog.Debug("Entering GetValue");

            if (string.IsNullOrEmpty(key))
                throw new Exception("Key should not be empty.");

            var value = GetValue(key);

            if (value != null)
            {
                _sysLog.Debug("Exiting GetValue");

                object settingsObj = value;

                return converter(settingsObj);
            }

            _sysLog.Debug("Exiting GetValue");

            return default(T);

        }

        /// <summary>
        /// Represent the method of Get app settings
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns the values of Get app settings</returns>
        public static string GetAppSettings(string key)
        {
            return AppConfigurationBuilder.GetAppSettings(key);
        }

        /// <summary>
        /// Represent the method of Get deny string settings
        /// </summary>        
        /// <returns>Returns the values of deny strings</returns>
        public static List<string> GetDenyStrings()
        {
            return AppConfigurationBuilder.GetDenyStrings();
        }
    }
}