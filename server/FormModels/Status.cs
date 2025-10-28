namespace CPS.Proof.DFSExtension
{
 using System.Runtime.Serialization;


    /// <summary>
    /// Represents the status of exection of methods.
    /// </summary>
    [DataContract]
    public enum Status
    {       
        /// <summary>
        /// Success
        /// </summary>
        Success = 0,

        /// <summary>
        /// Failure 
        /// </summary>
        Failure,

        /// <summary>
        /// Duplicate data found - 8 steps uses this.
        /// </summary>
        Duplicate,

        /// <summary>
        /// Lock Process/Lock Process instance
        /// </summary>
        LockError,

        /// <summary>
        /// Load Process Instance. If a connection to 
        /// an external database (when a query is executed) fails
        /// this error is returned.
        /// </summary>
        ExternalConnectionError,

        /// <summary>
        /// While executing an external database call like a Trigger
        /// query, if lets say, the tables do not exist, an External
        /// SQL error is returned.
        /// </summary>
        ExternalSQLError,

        /// <summary>
        /// Used for optimistic concurrency implementation of grids
        /// in 8 steps.
        /// </summary>
        ConcurrencyViolation,

        /// <summary>
        /// Used to indicate to the user interface that no 
        /// instance is available for processing.
        /// </summary>
        NoInstanceFound,

        /// <summary>
        /// Used to indicate the user that there are 
        /// instances available for the process.
        /// </summary>
        InstanceFound,

        /// <summary>
        /// Used to indicate to the user that the login
        /// to Document management server failed.
        /// </summary>
        DMSLoginFailed,

        /// <summary>
        /// Used to indicate to the user that all the
        /// rules failed to execute for the activity.
        /// </summary>
        RuleExecutionFailed,

        /// <summary>
        /// Used to indicate to the user that the given limit
        /// for adding an instance has exceeded.
        /// </summary>
        InstanceLimitExceeded,

        /// <summary>
        /// Used to indicate to the user that the given limit
        /// for creating a new process has exceeded.
        /// </summary>
        ProcessLimitExceeded,

        /// <summary>
        /// Used to indicate to the user that the given limit
        /// for creating a new version of the process has exceeded.
        /// </summary>
        VersionLimitExceeded,

        /// <summary>
        /// Used to indicate to the user that the given package
        /// is under going an offline publish.
        /// </summary>
        PackagePublishInProgress = 32,

        /// <summary>
        /// Represents the status that indicates the user limit 
        /// exceeded for named/concurrent users license.
        /// </summary>
        UserLimitExceeded = 34,

        /// <summary>
        /// Represents the status that indicates an evaluation 
        /// license has expired.
        /// </summary>
        EvaluationExpired,

        /// <summary>
        /// Represent the status that indicates the feature is
        /// disabled by license or feature upper limit exceeded.
        /// </summary>
        FeatureDisabled,

        /// <summary>
        /// Represent the status the indicates the Activity not accessible.
        /// </summary>
        ActivityNotAccessible,

        /// <summary>
        /// Represents the status that indicates the validation failure 
        /// for instance data.
        /// </summary>
        ValidationFailed,

        /// <summary>
        /// Represent the status the indicates the whether processid 
        /// selected or not.
        /// </summary>
        NoneProcessId,

        /// <summary>
        /// Represents the status the indicate process is not
        /// defined to package.
        /// </summary>
        ProcessNotDefined,

        /// <summary>
        /// Represents the status indicate select process deleted
        /// successfully
        /// </summary>
        SucessDelete,

        /// <summary>
        /// Represents the status indicate select process deleted
        /// failure
        /// </summary>
        FailureDelete,

        /// <summary>
        /// Represents the status indicate Package is locked.
        /// </summary>
        PackageLocked,

        /// <summary>
        /// Represents the status indicate Package is not well
        /// defined.
        /// </summary>
        PackageNotWellDefined,

        /// <summary>
        /// Represents the status indicate user does not have
        /// right to access.
        /// </summary>
        RightAccessDenied,

        /// <summary>
        /// Indicates that instance is already accepted.
        /// </summary>
        InstanceAlreadyAccepted,

        /// <summary>
        /// Indicates that no data found to process the request
        /// </summary>
        NoDataFound,

        /// <summary>
        /// Indicates Domain data map associated with some
        /// Expressions. So domain data cannot be deleted.
        /// </summary>
        DomainDataMapAssociated,

        /// <summary>
        /// Indicates Prerequisite Required to perform 
        /// the copyProcess operation
        /// </summary>
        PrerequisiteRequired,

        /// <summary>
        /// Indicates the same user already Locked the instance 
        /// </summary>
        InstanceAlreadyLocked,

        /// <summary>
        /// Indicates the same user already Locked the instance 
        /// </summary>
        InstanceAlreadySubmitted,

        /// <summary>
        /// Indicates the Source and Destination process do match
        /// with references during import and merge process
        /// </summary>
        ProcessMismatch,

        /// <summary>
        /// Indicates the Status of process
        /// </summary>
        ProcessLocked,

        /// <summary>
        /// Indicates the Status of Invalid datatype
        /// </summary>
        InvalidDataType = 55,

        /// <summary>
        /// Indicates the Status of Invalid control type
        /// </summary>
        InvalidControlType = 56,

        /// <summary>
        /// Indicates the Status of Invalid name
        /// </summary>
        InvalidName = 57,

        /// <summary>
        /// Indicates the Email not configured for existing user
        /// </summary>
        MailIdNotFound=58,

        /// <summary>
        /// Indicates newly created 
        /// </summary>
        NewlyCreated=59,

        /// <summary>
        /// Indicates the Status of the Invalid ElementName
        /// </summary>
        InvalidElementName=60,

        /// <summary>
        /// Indicates Process-Prerequisite Required to perform 
        /// the Import/Copy operation
        /// </summary>
        ProcessPrerequisiteRequired=61,

        /// <summary>
        /// Indicates  Activity-Prerequisite Required to perform 
        /// the Import/Copy operation
        /// </summary>
        ActivityPrerequisiteRequired = 62,

        /// <summary>
        /// Indicates  DataElements-Prerequisite Required to perform 
        /// the Import/Copy operation
        /// </summary>
        DataElementsPrerequisiteRequired = 63,

        /// <summary>
        /// Indicates  Rule-Prerequisite Required to perform 
        /// the Import/Copy operation
        /// </summary>
        RulePrerequisiteRequired = 64,

        /// <summary>
        /// Indicates  Duplicate Process Name Warning in 
        /// the Import/Copy operation
        /// </summary>
        DuplicateProcessNameFound = 65,

        /// <summary>
        /// Indicates  Activity Selection Required to perform
        /// the Import operation for Caption Definer
        /// </summary>
        MetaModelPrerequisite = 66,

          /// <summary>
        /// Indicates  Duplicate Local Number or EmailId Warning in
        /// the Import operation for OrganizationUnit
        /// </summary>
        DuplicateLocalNumberorEmailFound = 67,

        /// <summary>
        /// Indicates  Activity not saved
        /// </summary>
        ActivityNotSaved = 68,

        /// <summary>
        /// Indicates  Forking that is mapped with 
        /// the DFS
        /// </summary>
        ForkingCannotBeDeleted = 69,

        
        /// <summary>
        /// Indicates xml Product Version is 
        /// not allowed to Import process
        /// </summary>
        ProductVersionNotAllowed = 70,
        InvalidFormat = 71,

       /// <summary>
       /// Indicates  TenantDuplication   
       /// </summary>
        TenantCodeDuplicate = 74,

        /// <summary>
        /// Indicates  TenantName Duplication
        /// </summary>
        TenantNameDuplicate = 75,

        /// <summary>
        /// Indicates  Template Reference Failed
        /// </summary>
        TemplateReferenceFailed = 76,

        /// <summary>
        /// Indicates License Failed
        /// </summary>
        LicenseFailed = 77,

        /// <summary>
        /// Indicates  Template Update
        /// </summary>
        update = 78,

        /// <summary>
        /// Indicates  Data already present
        /// </summary>
        DataPresent = 79,

        /// <summary>
        /// Indicates Data already not present
        /// </summary>
        DataNotExists = 80,

        /// <summary>
        /// Indicates the Status to delete primary key
        /// </summary>
        deleteprimarykey = 81,

        /// <summary>
        /// Indicates the Status that 
        /// operation failed in remote machine.
        /// </summary>
        RemoteMachineFailed = 82,

        /// <summary>
        /// Indicates the Status that 
        /// it is aaociated with task
        /// </summary>
        CannotdeleteImportrequirements = 83,

        FileSizeExceeded = 84,

        InvalidFileType = 85
       

    }
}