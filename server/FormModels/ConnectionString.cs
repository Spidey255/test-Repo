namespace CPS.Proof.DFSExtension
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Gets or Sets a <see cref="ConnectionString"/>
    /// that holds the Connection String
    /// </summary>
    [Serializable]
    public class ConnectionString
    {
        /// <summary>
        /// Gets or sets a <see cref="string"/> 
        /// that contains the name 
        /// </summary>
        [DataMember(Name = "Name", IsRequired = false, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="string"/>
        /// that contains the connection 
        /// </summary>
        [DataMember(Name = "Connection", IsRequired = false, EmitDefaultValue = false)]
        public string Connection { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="string"/>
        /// that contains the ConnectionId
        /// </summary>
        [DataMember(Name = "ConnectionId", IsRequired = false, EmitDefaultValue = false)]
        public string ConnectionId { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="DbTypes"/>
        /// that contains the Dbtype
        /// </summary>
        [DataMember(Name = "DbType", IsRequired = false, EmitDefaultValue = false)]
        public DbTypes DbType { get; set; }

        /// <summary>
        /// Gets or Sets a <see cref="string"/>
        /// that contains the Hostname
        /// </summary>
        [DataMember(Name = "HostName", IsRequired = false, EmitDefaultValue = false)]
        public string HostName { get; set; }

        /// <summary>
        /// Gets or Sets a <see cref="string"/>
        /// that contains the template
        /// </summary>
        [DataMember(Name = "Template", IsRequired = false, EmitDefaultValue = false)]
        public string Template { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="System.Int16"/>
        /// that contains the port
        /// </summary>
        [DataMember(Name = "Port", IsRequired = false, EmitDefaultValue = false)]
        public int Port { get; set; }

        /// <summary>
        /// Gets or Sets a <see cref="ConnectionTypes"/>
        /// that holds the connenctiontype
        /// </summary>
        [DataMember(Name = "ConnectionType", IsRequired = false, EmitDefaultValue = false)]
        public ConnectionTypes ConnectionType { get; set; }

        /// <summary>
        /// Gets or Sets a <see cref="string"/>
        /// that holds the Namespace
        /// </summary>
        [DataMember(Name = "NameSpace", IsRequired = false, EmitDefaultValue = false)]
        public string NameSpace { get; set; }

        /// <summary>
        /// Gets or Sets a <see cref="string"/>
        /// that holds the Method Name
        /// </summary>
        [DataMember(Name = "MethodName", IsRequired = false, EmitDefaultValue = false)]
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or Sets a <see cref="RequestType"/>
        /// that holds thr request type
        /// </summary>
        [DataMember(Name = "RequestType", IsRequired = false, EmitDefaultValue = false)]
        public RequestType RequestType { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="System.Int16"/> that contains the 
        /// ContentType id
        /// (1-SOAP,2-XML,3-JSON)
        /// </summary>
        [DataMember(Name = "ContentType", IsRequired = false, EmitDefaultValue = false)]
        public ContentType ContentType { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="System.Boolean"/> 
        /// that contains the RequireAuthentication state
        /// </summary>
        [DataMember(Name = "RequireAuthentication", IsRequired = false, EmitDefaultValue = false)]
        public bool RequireAuthentication { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="System.Boolean"/> 
        /// that contains the HasRqstBdy state
        /// </summary>
        [DataMember(Name = "HasRequestBody", IsRequired = false, EmitDefaultValue = false)]
        public bool HasRequestBody { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="string"/> 
        /// that contains the UserName
        /// </summary>
        [DataMember(Name = "UserName", IsRequired = false, EmitDefaultValue = false)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="string"/> 
        /// that contains the Password
        /// </summary>
        [DataMember(Name = "Password", IsRequired = false, EmitDefaultValue = false)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="bool"/> that
        /// contains value for transport layered service.
        /// </summary>
        [DataMember(Name = "TransportLayeredService", IsRequired = false, EmitDefaultValue = false)]
        public bool TransportLayeredService { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="bool"/> that
        /// contains value for autoreconnect.
        /// </summary>
        [DataMember(Name = "AutoReConnect", IsRequired = false, EmitDefaultValue = false)]
        public bool AutoReConnect { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="bool"/> that
        /// contains value for IdleTimeouts.
        /// </summary>
        [DataMember(Name = "IdleTimeouts", IsRequired = false, EmitDefaultValue = false)]
        public int IdleTimeouts { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="string"/> that
        /// contains value for ErrorCodes.
        /// </summary>
        [DataMember(Name = "ErrorCodes", IsRequired = false, EmitDefaultValue = false)]
        public string ErrorCodes { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="string"/> that
        /// contains value for SuccessCodes.
        /// </summary>
        [DataMember(Name = "SuccessCodes", IsRequired = false, EmitDefaultValue = false)]
        public string SuccessCodes { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="int"/> that
        /// contains value for Retries.
        /// </summary>
        [DataMember(Name = "Retries", IsRequired = false, EmitDefaultValue = false)]
        public int Retries { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="double"/> that
        /// contains value for RetryInterval.
        /// </summary>
        [DataMember(Name = "RetryInterval", IsRequired = false, EmitDefaultValue = false)]
        public double RetryInterval { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="bool"/> that
        /// contains value for IsAsync.
        /// </summary>
        [DataMember(Name = "IsAsync", IsRequired = false, EmitDefaultValue = false)]
        public bool IsAsync { get; set; }

    }

     /// <summary>
    /// Represent the Enum for Database that Proof supports. This
    /// will have more entries when we support more databases.
    /// </summary>
    public enum DbTypes : byte
    {
        /// <summary>
        /// Represents the Sql server database.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        SQLServer = 0,

        /// <summary>
        /// Represents the My Sql server database.
        /// </summary>
        MySql = 1,

        /// <summary>
        /// Represents the Oracle server database.
        /// </summary>
        Oracle = 2,


        Socket = 3,
        WebServices = 4,
        FTP = 5,
        WCF = 6,
        /// <summary>
        /// Represents the ODBC server database.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        ODBC = 7


    }

    public enum RequestType : byte
    {
        Get = 1,
        Post = 2,
        Put = 3,
        Patch = 4,
        Delete = 5,
        Copy = 6,
        Head = 7,
        Options = 8,
        Link = 9,
        UnLink = 10,
        Purge = 11,
        Lock = 12,
        UnLock = 13,
        PropFind = 14,
        View = 15
    }

    public enum ConnectionTypes : byte
    {
        MsSql = 0,
        MySql = 1,
        Oracle = 2,
        Socket = 3,
        WebServices = 4,
        FTP = 5,
        WCF = 6,
        MSMQ = 7,
        StandardEvent = 8,
        CustomEvent = 9,
        TimeBasedEvent = 10,
        ExternalEvent = 11,
        BAMEvent = 12,
        REST = 13,
        POP3 = 14,
        IMAP = 15,
        SMTP = 16,
        URL = 17
    }

    /// <summary>
    /// Represents the enumeration that holds the Content Types.
    /// </summary>
    public enum ContentType : byte
    {
        SOAP = 1,
        XML = 2,
        JSON = 3
    }
}