
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SRA.Proof.Helpers;
using SRA.Proof.Middleware;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using SRA.Proof.Common;
namespace CPS.Proof.DFSExtension
{
    public class MsSQLControllerBase
    {
         /// <summary>
        /// Represents a reference to the instance of <see cref="ILog"/>
        /// implementation that is used for logging.
        /// </summary>
        private readonly ILog _sysLog;



        /// <summary>
        /// Represents <see cref="string"/> that holds the name of configuration
        /// file for Logsetting.
        /// </summary>
        public const string LogConfigFile = @"Configuration/LogSettings.Config";

        
        /// <summary>
        /// Initializes a new instance of 
        /// <see cref="MsSQLControllerBase"/> class.
        /// </summary>       
        public MsSQLControllerBase()
        {
            // Create the logger.
            _sysLog = LogManager.GetLogger(GetType());                          

        }

         #region Protected Methods

        protected Database GetDatabaseInstance()
        {

            

            try
            {

                var repConnection = AppParams.GetValue("RepConnectionName");


                var proofConnection = AppParams.GetValue("ProofConnectionName");

                var connectionString = GetConnectionStringById
                (repConnection, proofConnection);



                return CreateSQLDatabase(connectionString);

            }
            catch
            {
                throw;
            }


        }

        protected Database GetDatabaseInstance(SlotToken token)
        {        
            return GetProofInstance(token);
        
        }

         /// <summary>
        /// Represents a method used to Get DBInstance for the
        /// Multi TenantId
        /// </summary>
        /// A <see cref="string"/> contains the current user slot
        /// <param name="token"></param>        
        /// <returns>Database Instance</returns>
        protected string GetConnectionStringById
            ( string repConnectionName,string ProofConnectionId)
        {
            IDataReader reader = null;

            _sysLog.Debug("Entering GetConnectionStringById");

            Database database = null;

            try
            {
                                
                database = GetRepositoryDatabaseInstance
                                                    (repConnectionName);
                
                using (DbCommand command = database.GetStoredProcCommand
                   ("GetConnectionStringById"))
                {
                    command.CommandTimeout = 10000;

                    database.AddInParameter(command, "ConnectionSettingId",
                        DbType.String, ProofConnectionId);

                    reader = database.ExecuteReader(command);

                    while (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }

            }
            catch(Exception ex)
            {
                _sysLog.Error("Error in GetConnectionStringById", ex);
                return null;
            }

            finally
            {
                if (reader != null)
                {
                    if (!reader.IsClosed) reader.Close();

                    reader.Dispose();
                }

                _sysLog.Debug("Exiting GetConnectionStringById");
            }

            return string.Empty;
        }


         /// <summary>
        /// Represents the method that is used to check if the
        /// connection string retrieved from the configuration
        /// file is valid and to return a valid database instance
        /// initialised with the connection string.
        /// </summary>
        /// <returns>
        /// A <see cref="Database"/> object.
        /// </returns>
        protected Database GetRepositoryDatabaseInstance(string repConnectionName)
        {
            return new SqlDatabase
                (AppParams.GetValue(repConnectionName));
        }

        /// <summary>
        /// Method for invoking a default Database object. Reads default settings
        /// from the encrypted connectionstring 
        /// </summary>
        /// <example>
        /// <code>
        /// Database dbSvc = DatabaseFactory.CreateDatabase();
        /// </code>
        /// </example>
        /// <returns>Database</returns>

        public static Database CreateSQLDatabase(string connectionstring)
        {
            return new SqlDatabase(DecodeConnectionString(
                                               connectionstring));
        }
        
        /// <summary>
        /// Decrypts the password.
        /// </summary>
        /// <param name="encodedPassword">
        /// A <seealso cref="string"/> containing the encrypted
        /// password.
        /// </param>
        /// <returns>
        /// A decrypted <see cref="string"/>.
        /// </returns>
        private static string DecodeConnectionString(string encodedconnectionstring)
        {
            ICryptoManager cryptomanager = ObjectManager.Acquire<ICryptoManager>();

            try
            {

                if (encodedconnectionstring != null)
                {
                    var decryptedConnectionstring = cryptomanager.Decrypt(encodedconnectionstring);

                    //Discard provider name from the connectionstring
                    return decryptedConnectionstring.
                                Substring(decryptedConnectionstring.IndexOf(";") + 1);
                }
                else
                    return null;
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                if (cryptomanager != null)
                    ObjectManager.Release<ICryptoManager>(cryptomanager);
            }

        }

        /// <summary>
        /// Represents a helper method to retrieve the 
        /// database instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Database"/>instance.
        /// </returns>
        protected Database GetProofInstance(SlotToken token)
        {
            

            string proofConnectionString = null;

            

            try
            {
               

                if (proofConnectionString != null)
                {

                    return CreateSQLDatabase(proofConnectionString);

                }
                
                var connectionString = GetConnectionStringById
                        (token.RepConnectionName, token.ConnectionName);

               

                return  CreateSQLDatabase(connectionString);

            }
            catch
            {
                throw;
            }
            
                                 
        }

        #endregion
    }

}