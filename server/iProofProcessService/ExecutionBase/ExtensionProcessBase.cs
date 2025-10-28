

namespace CPS.Proof.DFSExtension
{
    using log4net;
    using SRA.Proof.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract   class ExtensionProcessBase :IExtBaseMetaData
    {
       
            private ILog _logger = LogManager.GetLogger(typeof(ExtensionProcessBase));

            #region Properties

            /// <summary>
            /// Gets a <see cref="System.Int64"/> that contains the package id.
            /// </summary>
            public abstract long PackageId
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the package name.
            /// </summary>
            public abstract string PackageName
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the package process map id.
            /// </summary>
            public abstract string PackageProcessMapId
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the package
            /// description.
            /// </summary>
            public abstract string PackageDescription
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="System.Int64"/> that contains the process id.
            /// </summary>
            public abstract string ProcessId
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the process name.
            /// </summary>
            public abstract string ProcessName
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the process
            /// description.
            /// </summary>
            public abstract string ProcessDescription
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="SRA.Proof.Helpers.ProcessType"/> that contains
            /// the ProcessType.
            /// </summary>
            public abstract ProcessType ProcessType
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the comments.
            /// </summary>
            public abstract string Comments
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the FileGroupId.
            /// </summary>
            public abstract string FileGroupId
            {
                get;
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the FileGroup.
            /// </summary>
            public abstract string FileGroup
            {
                get;
            }


            /// <summary>
            /// Gets or Sets <see cref="string"/> master form id.
            /// </summary>
            public abstract string MasterFormId
            {
                get;
            }

            /// <summary>
            /// Gets or Sets <see cref="System.Byte"/> process instance mode.
            /// </summary>
            public abstract byte ProcessInstanceMode
            {
                get;
            }


            /// <summary>
            /// Represents the version of process definition 
            /// assembly.
            /// </summary>
            public decimal Version { get; set; }


        /// <summary>
        /// Represents the property that holds global Variables
        /// </summary>
        public Dictionary<string, object> GlobalVariables { get; set; }

        /// <summary>
        /// Represent the property that holds the user 
        /// variables.
        /// </summary>
        public Dictionary<string, object> UserVariables { get; set; }



        #endregion


    }
}
