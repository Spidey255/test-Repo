namespace CPS.Proof.DFSExtension
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    /// <summary>
    /// Represents a class used to hold token and
    /// logged in user details.
    /// </summary>
    [Serializable]
    public class SlotToken
    {
        /// <summary>
        /// Gets or sets the slot token id.
        /// </summary>
        /// <value>
        /// The slot id.
        /// </value>
        [DataMember(Name = "SID", IsRequired = false, EmitDefaultValue = false)]
        public string SID { get; set; }

        /// <summary>
        /// Gets or sets the slot token id.
        /// </summary>
        /// <value>
        /// The slot id.
        /// </value>
        [DataMember(Name = "segmentName", IsRequired = false, EmitDefaultValue = false)]
        public string segmentName { get; set; }

        /// <summary>
        /// Gets or sets the slot token id.
        /// </summary>
        /// <value>
        /// The slot id.
        /// </value>
        [DataMember(Name = "segmentId", IsRequired = false, EmitDefaultValue = false)]
        public string segmentId { get; set; }



        /// <summary>
        /// Gets or sets the slot token id.
        /// </summary>
        /// <value>
        /// The slot id.
        /// </value>
        [DataMember(Name = "totalSegments", IsRequired = false, EmitDefaultValue = false)]
        public string totalSegments { get; set; }

        /// <summary>
        /// Gets or sets the slot token id.
        /// </summary>
        /// <value>
        /// The api Key.
        /// </value>
        [DataMember(Name = "apiKey", IsRequired = false, EmitDefaultValue = false)]
        public string apiKey { get; set; }

        /// <summary>
        /// Gets or sets the slot token id.
        /// </summary>
        /// <value>
        /// The app Id.
        /// </value>
        [DataMember(Name = "appId", IsRequired = false, EmitDefaultValue = false)]
        public string appId { get; set; }

        /// <summary>
        /// Gets or sets the slot token id.
        /// </summary>
        /// <value>
        /// The slot id.
        /// </value>
        [DataMember(Name = "NotificationId", IsRequired = false, EmitDefaultValue = false)]
        public string NotificationId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [DataMember(Name = "UID", IsRequired = false, EmitDefaultValue = false)]
        public long UID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [DataMember(Name = "TranId", IsRequired = false, EmitDefaultValue = false)]
        public string TranId { get; set; }

        /// <summary>
        /// Gets or sets the user map id.
        /// </summary>
        /// <value>
        /// The user map id.
        /// </value>
        [DataMember(Name = "UMID", IsRequired = false, EmitDefaultValue = false)]
        public long UMID { get; set; }

        /// <summary>
        /// Gets or sets the user time zone.
        /// </summary>
        /// <value>
        /// The user time zone.
        /// </value>
        [DataMember(Name = "TZ", IsRequired = false, EmitDefaultValue = false)]
        public string TZ { get; set; }

        /// <summary>
        /// Gets or sets the role map id.
        /// </summary>
        /// <value>
        /// The role map id.
        /// </value>
        [DataMember(Name = "RMId", IsRequired = false, EmitDefaultValue = false)]
        public long RMId { get; set; }

        /// <summary>
        /// Gets or sets role based authenticate.
        /// </summary>
        /// <value>
        /// return "1" for Role based Authenticate else "0"
        /// </value>
        [DataMember(Name = "IsRoleBased", IsRequired = false, EmitDefaultValue = false)]
        public string IsRoleBased { get; set; }

        /// <summary>
        /// Gets or sets ShowInstances WithinGroup.
        /// </summary>
        /// <value>
        /// return "1" for ShowInstances if 
        /// the instnace created by users under the same orgunit.
        /// else "0"
        /// </value>
        [DataMember(Name = "ShowInstancesWithinGroup", IsRequired = false, EmitDefaultValue = false)]
        public string ShowInstancesWithinGroup { get; set; }

        /// <summary>
        /// Gets or sets the RoleNames of the user.
        /// </summary>
        /// <value>
        /// The RoleNames.
        /// </value>
        [DataMember(Name = "RoleNames", IsRequired = false, EmitDefaultValue = false)]
        public string RoleNames { get; set; }

        /// <summary>
        /// Gets or sets the RoleNames of the LoggedInRoleName.
        /// </summary>
        /// <value>
        /// The LoggedInRoleName.
        /// </value>
        [DataMember(Name = "LoggedInRoleName", IsRequired = false, EmitDefaultValue = false)]
        public string LoggedInRoleName { get; set; }

        /// <summary>
        /// Gets or sets the Role Descriptions.
        /// </summary>
        /// <value>
        /// The RoleDescription.
        /// </value>
        [DataMember(Name = "RoleDescriptions", IsRequired = false, EmitDefaultValue = false)]
        public string RoleDescriptions { get; set; }

        /// <summary>
        /// Gets or sets the DisplayName.
        /// </summary>
        /// <value>
        /// The DisplayName.
        /// </value>
        [DataMember(Name = "DisplayName", IsRequired = false, EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        /// <value>
        /// Session id.
        /// </value>
        [DataMember(Name = "SesId", IsRequired = false, EmitDefaultValue = false)]
        public string SesId { get; set; }

        /// <summary>
        /// Gets or sets the tenant id.
        /// </summary>
        [DataMember(Name = "TenId", IsRequired = false, EmitDefaultValue = false)]
        public string TenId { get; set; }

        /// <summary>
        /// Gets or sets the tenant code.
        /// </summary>
        [DataMember(Name = "TenCode", IsRequired = false, EmitDefaultValue = false)]
        public string TenCode { get; set; }

        /// <summary>
        /// Gets or sets the layout preferences.
        /// </summary>
        [DataMember(Name = "LayoutPref", IsRequired = false, EmitDefaultValue = false)]
        public string LayoutPref { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        [DataMember(Name = "Lang", IsRequired = false, EmitDefaultValue = false)]
        public string Lang { get; set; }

        /// <summary>
        /// Gets or sets the comma separated role map ids.
        /// </summary>
        [DataMember(Name = "RlMIds", IsRequired = false, EmitDefaultValue = false)]
        public string RlMIds { get; set; }

        /// <summary>
        /// Gets or sets database or connection type.
        /// </summary>
        [DataMember(Name = "DbType", IsRequired = false, EmitDefaultValue = false)]
        public DbTypes DbType { get; set; }

        /// <summary>
        /// Gets or sets the connection name.
        /// </summary>
        [DataMember(Name = "ConnectionName", IsRequired = false, EmitDefaultValue = false)]
        public string ConnectionName { get; set; }

        /// <summary>
        /// Gets or sets the connection name.
        /// </summary>
        [DataMember(Name = "RemoteUrl", IsRequired = false, EmitDefaultValue = false)]
        public string RemoteUrl { get; set; }

        /// <summary>
        /// Gets or sets the ActivityContext.
        /// IP and name of client Machine
        /// </summary>
        /// <value>
        /// The ActivityContext.
        /// </value>
        [DataMember(Name = "ActivityContext", IsRequired = false, EmitDefaultValue = false)]
        public string ActivityContext { get; set; }

        /// <summary>
        /// Gets or Sets the RepConnectionName
        /// </summary>
        public string RepConnectionName
        {
            get { return AppParams.GetAppSettings("RepConnectionName"); }

            set
            {
                // Default 
            }

        }

        /// <summary>
        ///  Gets or Sets the AuditLogConnectionName
        /// </summary>
        public string AuditLogConnectionName
        {
            get { return AppParams.GetAppSettings("AuditLogConnectionName"); }

            set
            {
                // Default 
            }

        }

        /// <summary>
        /// Gets or Sets the RepDbType
        /// </summary>
        public DbTypes RepDbType
        {
            get
            {
                var dbType = (DbTypes)Enum.Parse(typeof(DbTypes),
                   AppParams.GetAppSettings("RepdbType"));

                return dbType;
            }
            set { }

        }


        /// <summary>
        /// Gets or sets the logged in username.
        /// </summary>
        [DataMember(Name = "UserName", IsRequired = false, EmitDefaultValue = false)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the token created date.
        /// </summary>
        [DataMember(Name = "CreatedOn", IsRequired = false, EmitDefaultValue = false)]
        public string CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the whether logged user is admin or not.
        /// </summary>
        /// <value>
        /// IsAdminUser flag.
        /// </value>
        [DataMember(Name = "IsAdminUser", IsRequired = false, EmitDefaultValue = false)]
        public bool IsAdminUser { get; set; }

        /// <summary>
        /// Gets or sets the user FirstName.
        /// </summary>
        /// <value>
        /// FirstName.
        /// </value>
        [DataMember(Name = "FirstName", IsRequired = false, EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user LastName.
        /// </summary>
        /// <value>
        /// LastName.
        /// </value>
        [DataMember(Name = "LastName", IsRequired = false, EmitDefaultValue = false)]
        public string LastName { get; set; }


        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        [DataMember(Name = "ConnectionString", IsRequired = false, EmitDefaultValue = false)]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the enable audit log
        /// </summary>
        [DataMember(Name = "EnblAudLog", IsRequired = false, EmitDefaultValue = false)]
        public bool EnblAudLog { get; set; }


       

    }
}