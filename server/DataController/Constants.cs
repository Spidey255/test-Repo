namespace CPS.Proof.DFSExtension
{
    internal class AppConnectionStringsConstants
    {
        /// <summary>
        /// A <see cref="string"/> constant that contains sp name
        /// to get system parameter details.
        /// </summary>
        public const string GetSysConnectionSettings =
            "GetSysConnectionSettings";

        /// <summary>
        /// A <see cref="string"/> constant that contains param column
        /// name.
        /// </summary>
        public const string ConnId = "ConnectionSettingsId";

        /// <summary>
        /// A <see cref="string"/> constant that contains param value 
        /// column name.
        /// </summary>
        public const string ConnName = "Name";

        /// <summary>
        /// A <see cref="string"/> constant that contains param value 
        /// column name.
        /// </summary>
        public const string ConnString = "ConnectionString";

        public const string ConnectionType = "ConnectionType";

        public const string RequestType = "RequestType";

        public const string HostName = "HostName";

        public const string Port = "Port";

        public const string Template = "Template";

        public const string MethodName = "Method";

        public const string NameSpace = "NameSpace";

        public const string ContentTypeConstant = "ContentType";

        public const string RequireAuthentication = "RequireAuthentication";

        public const string HasRequestBody = "HasRequestBody";

        public const string UserName = "UserName";

        public const string Password = "Password";

        public const string TLS = "TLS";

        public const string AutoConnect = "AutoConnect";

        public const string IdleTimeout = "IdleTimeOutMs";

        public const string SuccessCodes = "SuccessCodes";

        public const string ErrorCodes = "ErrorCodes";

        public const string Retries = "Retries";

        public const string RetryInterval = "RetryInterval";

        public const string IsAsync = "IsAsync";        

    }

     internal class ISpaceStoreHandlerConstants
    {
        /// <summary>
        /// A <see cref="string"/> that holds the string ElementId
        /// </summary>
        public const string ElementId = "ElementId";

         /// <summary>
        /// A <see cref="string"/> that holds the StoredProcedure SaveFormInstanceData
        /// </summary>
        public const string SaveFormInstanceData = "SaveFormInstanceData";

        /// <summary>
        /// A <see cref="string"/> that holds the StoredProcedure GetFormInstanceData
        /// </summary>
        public const string GetFormInstanceData = "GetFormInstanceData";

        /// <summary>
        /// A <see cref="string"/> that holds string FormInstanceId 
        /// </summary>
        public const string FormInstanceId = "FormInstanceId";

        /// <summary>
        /// A <see cref="string"/> that holds string FormData 
        /// </summary>
        public const string FormData = "FormData";

        /// <summary>
        /// Represents the constants the contains the column name PackageProcessMapId
        /// </summary>
        public const string PackageProcessMapId = "PackageProcessMapId";

        /// <summary>
        /// Represents the constants the contains the column name ProcessActivityMapId
        /// </summary>
        public const string ProcessActivityMapId = "ProcessActivityMapId";

         /// <summary>
        /// Represents the constants the contains the column name ComplexElementId
        /// </summary>   
        public const string ComplexElementId = "ComplexElementId";

         /// <summary>
        /// Represents the constants the contains the column name UpdatedOn
        /// </summary>   
        public const string UpdatedOn = "UpdatedOn";

        /// <summary>
        /// Represents the constants the contains the column name UpdatedBy
        /// </summary>   
        public const string UpdatedBy = "UpdatedBy";
    }
}
