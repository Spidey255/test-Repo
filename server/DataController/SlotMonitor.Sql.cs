using System;
using log4net;
using System.Data;
using System.Threading;
using SRA.Proof.Helpers;
using System.Data.Common;
using System.Transactions;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Newtonsoft.Json;
using System.Text;
using SRA.Proof.Middleware;
namespace CPS.Proof.DFSExtension
{
public class SlotMonitorSql : MsSQLControllerBase, ISlotMonitor
{
        #region Member Variables

        /// <summary>
        /// A <seealso cref="ILog"/> instance for logging.
        /// </summary>
        private readonly ILog _sysLog;

        #endregion

         #region Constructor

        /// <summary>
        /// Represents the default constructor that initializes
        /// the logger instance.
        /// </summary>
        public SlotMonitorSql()
        {
            _sysLog = LogManager.GetLogger(GetType());


        }

        #endregion

         /// <summary>
        /// Represents the method used to validate a given slot.
        /// </summary>
        /// <param name="token">
        /// A <see cref="string"/> that holds the slotId.
        /// </param>
        /// <returns>
        /// Returns a boolean based on the availability of the slot id
        /// </returns>
        public bool IsSlotValid(ref SlotToken token, UserActivities activities)
        {
            IDataReader reader = null;

            
            try
            {
                _sysLog.Debug("Entering IsSlotValid");

                if (token == null || token.SID.Trim().Length == 0)
                    return false;


                Database dataBase = GetDatabaseInstance(token);


                if (dataBase == null)
                        return false;


                    var isEnableAuditLog = false;

                    var expiredSlotList = string.Empty;

                    using (var dbCommand = dataBase.GetStoredProcCommand
                        ("ValidateSlot"))
                    {
                        // Set the command timeout value.
                        dbCommand.CommandTimeout = 10000;

                        dataBase.AddInParameter(dbCommand,
                            "SlotId",
                            DbType.String, token.SID);

                        dataBase.AddInParameter(dbCommand,
                            "IsRoleBasedAuthentication",
                            DbType.String, token.IsRoleBased);

                    dataBase.AddInParameter(dbCommand,
                           "IsStaySignedIn",
                           DbType.String, token.RemoteUrl);



                    reader = dataBase.ExecuteReader(dbCommand);

                        var userTableColumnTimeZoneIndex = reader.GetOrdinal
                            ("TimeZone");

                        var userTableColumnUserIdIndex = reader.GetOrdinal
                            ("UserId");

                        var userTableColumnTenantIdIndex = reader.GetOrdinal
                            ("TenantId");

                        var userTableColumnTenantCodeIndex = reader.GetOrdinal
                           ("TenantCode");

                        var userTableColumnLayoutPrefIndex = reader.GetOrdinal
                            ("LayoutPref");

                        var userTableColumnLanguageIdIndex = reader.GetOrdinal
                            ("LangId");

                        var userTableColumnUserMapIdIndex = reader.GetOrdinal
                            ("UserMapId");

                        var userTableColumnRoleMapIdIndex = reader.GetOrdinal
                            ("RoleMapId");

                        var userTableColumnLoggedInRoleMapIdIndex = reader.GetOrdinal
                            ("LoggedInRoleMapId");

                        var userTableColumnLoggedInRoleNameIndex = reader.GetOrdinal
                                ("LoggedInRoleName");

                        var userTableColumnUserNameIndex = reader.GetOrdinal
                                    ("UserName");

                        var userTableColumnRoleNameIndex = reader.GetOrdinal
                            ("RoleName");

                        var userTableColumnRoleDescriptionIndex = reader.GetOrdinal
                            ("RoleDescription");

                        var userTableColumnFirstNameIndex = reader.GetOrdinal
                            ("FirstName");

                        var userTableColumnLastNameIndex = reader.GetOrdinal
                            ("LastName");

                        var isSystemIndex = reader.GetOrdinal
                           ("IsSystem");

                        var enableAuditLogIndex = reader.GetOrdinal
                              ("EnableAuditLog");

                        var connectionStringIndex = reader.GetOrdinal
                               ("ProofSQLDB");

                        token.UID = -1;

                        while (reader.Read())
                            {
                                //Read the values of the columns that will not 
                                //have null values in the database.

                                token.UID = reader.GetInt64
                                    (userTableColumnUserIdIndex);

                                token.TZ = reader.GetString
                                    (userTableColumnTimeZoneIndex);

                                token.UserName = reader.GetString
                                    (userTableColumnUserNameIndex);

                                if (!reader.IsDBNull(userTableColumnLastNameIndex))
                                    token.LastName = reader.GetString
                                        (userTableColumnLastNameIndex);

                                token.DisplayName = string.Format("{0} {1}",
                                        reader.GetString(userTableColumnFirstNameIndex),
                                        token.LastName);

                                token.IsAdminUser = reader.GetBoolean(isSystemIndex);

                                token.FirstName = reader.GetString
                                    (userTableColumnFirstNameIndex);

                                if (!reader.IsDBNull(userTableColumnTenantIdIndex))
                                    token.TenId = reader.GetString
                                        (userTableColumnTenantIdIndex);

                                if (!reader.IsDBNull(userTableColumnTenantCodeIndex))
                                    token.TenCode = reader.GetString
                                        (userTableColumnTenantCodeIndex);

                                if (!reader.IsDBNull(userTableColumnLayoutPrefIndex))
                                    token.LayoutPref = reader.GetString
                                        (userTableColumnLayoutPrefIndex);

                                if (!reader.IsDBNull(userTableColumnLanguageIdIndex))
                                    token.Lang = reader.GetString
                                        (userTableColumnLanguageIdIndex);

                                if (!reader.IsDBNull(userTableColumnUserMapIdIndex))
                                    token.UMID = reader.GetInt64
                                        (userTableColumnUserMapIdIndex);

                                if (!reader.IsDBNull(userTableColumnRoleMapIdIndex))
                                    token.RlMIds = reader.GetString
                                        (userTableColumnRoleMapIdIndex);

                                if (!reader.IsDBNull(userTableColumnLoggedInRoleMapIdIndex))
                                    token.RMId = reader.GetInt64
                                        (userTableColumnLoggedInRoleMapIdIndex);

                                if (!reader.IsDBNull(userTableColumnLoggedInRoleNameIndex))
                                    token.LoggedInRoleName = reader.GetString
                                        (userTableColumnLoggedInRoleNameIndex);

                                if (!reader.IsDBNull(userTableColumnRoleNameIndex))
                                                    token.RoleNames = reader.GetString
                                                        (userTableColumnRoleNameIndex);

                                if (!reader.IsDBNull
                                    (userTableColumnRoleDescriptionIndex))
                                    token.RoleDescriptions = reader.GetString
                                        (userTableColumnRoleDescriptionIndex);

                            if (!reader.IsDBNull(enableAuditLogIndex))
                            {
                                isEnableAuditLog =
                                    reader.GetBoolean(enableAuditLogIndex);

                                token.EnblAudLog= reader.GetBoolean(enableAuditLogIndex);
                            }

                            if (!reader.IsDBNull(connectionStringIndex))
                                token.ConnectionString =
                                    reader.GetString(connectionStringIndex);

                        }

                       

                        
                    }


               

                if (token.UID == -1)
                    return false;

                return true;
            }
            catch (ProofException ex)
            {
                _sysLog.Error("ERROR in IsSlotValid", ex);

                return false;
            }
            catch (Exception e)
            {
                _sysLog.Error("ERROR in IsSlotValid", e);

                return false;
            }
            finally
            {
              

                if (reader != null)
                {
                    _sysLog.Debug("Exiting IsSlotValid");

                    if (!reader.IsClosed)
                        reader.Close();
                    reader.Dispose();
                }
            }
        }
}
}