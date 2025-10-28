namespace CPS.Proof.DFSExtension
{
 using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class UserActivities
    {

        /// <summary>
        /// Gets/Sets the DisplayContext
        /// </summary>
        public string DisplayContext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Sets the DisplayContext
        /// </summary>
        public string KeyContext
        {
            get;
            set;
        }

        public string Context
        {
            get;
            set;
        }

        public string RefId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Sets the TransactionId
        /// </summary>
        public string TransactionId
        {
            get;
            set;
        }

        /// <summary>
        /// gets or sets a <see cref="string"/> that contains ActivityLogId.
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public string ActivityLogId { get; set; }

        /// <summary>
        /// gets or sets a <see cref="string"/> that contains the Operation Name.
        /// </summary>
        [DataMember(Name = "Name", IsRequired = false)]
        public string OperationName { get; set; }

        /// <summary>
        /// gets or sets a <see cref="string"/> that contains the Operation Context.
        /// </summary>
        [DataMember(Name = "Context", IsRequired = false)]
        public string OperationContext { get; set; }


        /// <summary>
        /// gets or sets a <see cref="string"/> that contains the Operation Context.
        /// </summary>
        [DataMember(Name = "KeyContext", IsRequired = false)]
        public string OperationKeyContext { get; set; }


        /// <summary>
        /// gets or sets a <see cref="string"/> that contains the Operation Context.
        /// </summary>
        [DataMember(Name = "DisplayContext", IsRequired = false)]
        public string OperationDisplayContext { get; set; }

        /// <summary>
        /// gets or sets a <see cref="string"/> taht contains the Operation Date. 
        /// </summary>
        [DataMember(Name = "OperationDate", IsRequired = false)]
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// gets or sets a <see cref="string"/> that contains the Slot Id. 
        /// </summary>
        [DataMember(Name = "SlotId", IsRequired = false)]
        public string SlotId { get; set; }

        /// <summary>
        /// gets or sets a <see cref="System.Int32"/> that contains the ActivityType id.
        /// </summary>
        [DataMember(Name = "ActivityTypeId", IsRequired = false)]
        public int ActivityTypeId { get; set; }

        /// <summary>
        /// gets or sets a <see cref="System.Int64"/> that contains total record count.
        /// </summary>
        [DataMember(Name = "TotalRecords", IsRequired = false)]
        public long TotalRecords { get; set; }

        /// <summary>
        /// gets or sets a <see cref="System.Int64"/> that contains In time.
        /// </summary>
        [DataMember(Name = "LoginTime", IsRequired = false)]
        public long LoginTime { get; set; }

        /// <summary>
        /// gets or sets a <see cref="System.Int32"/> that contains Thread Id.
        /// </summary>
        [DataMember(Name = "ThreadId", IsRequired = false)]
        public int ThreadId { get; set; }

        /// <summary>
        /// gets or sets a <see cref="string"/> that contains exception.
        /// </summary>
        [DataMember(Name = "Exception", IsRequired = false)]
        public string Exception { get; set; }

        /// <summary>
        /// gets or sets a <see cref="string"/> that contains the ProxyIp.
        /// </summary>
        [DataMember(Name = "ProxyIp", IsRequired = false)]
        public string ProxyIp { get; set; }

        /// <summary>
        /// gets or sets a <see cref="string"/> that contains UserName.
        /// </summary>
        [DataMember(Name = "UserName", IsRequired = false)]        
        public string UserName { get; set; }

        /// <summary>
        /// REpresents a property to identify the log type
        /// </summary>
        public ActivityLogType LogType { get; set; }

        /// <summary>
        /// REpresents a property to identify the TenantId
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// REpresents a property to identify the UserMapId
        /// </summary>
        public long UserMapId { get; set; }

    }

    public enum ActivityLogType : byte
    { 
        ApplicationLog = 1,
        ApplicationException = 2,
        ADOException = 3
    }
}