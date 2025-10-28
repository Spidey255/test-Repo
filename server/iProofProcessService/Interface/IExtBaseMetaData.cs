


namespace CPS.Proof.DFSExtension
{
using SRA.Proof.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public interface IExtBaseMetaData
    {
        #region Package and Process Members

      
        /// <summary>
        /// Gets a <see cref="string"/> that contains the package process map id.
        /// </summary>
        string PackageProcessMapId
        {
            get;
        }

     
        /// <summary>
        /// Gets a <see cref="System.Int64"/> that contains the process id.
        /// </summary>
        string ProcessId
        {
            get;
        }

        /// <summary>
        /// Gets a <see cref="string"/> that contains the process name.
        /// </summary>
        string ProcessName
        {
            get;
        }

        /// <summary>
        /// Gets a <see cref="SRA.Proof.Helpers.ProcessType"/> that contains
        /// the ProcessType.
        /// </summary>
        ProcessType ProcessType
        {
            get;
        }

        /// <summary>
        /// Gets a <see cref="string"/> that contains the comments.
        /// </summary>
        //string Comments
        //{
        //    get;
        //}

        /// <summary>
        /// Gets a <see cref="string"/> that contains the File group Id.
        /// </summary>
        string FileGroupId
        {
            get;
        }

        /// <summary>
        /// Gets a <see cref="string"/> that contains the File group Name.
        /// </summary>
        string FileGroup
        {
            get;
        }

        /// <summary>
        /// Gets a <see cref="string"/> that contains the Master FormId.
        /// </summary>
        string MasterFormId
        {
            get;
        }

        /// <summary>
        /// Gets or Sets <see cref="System.Byte"/> process instance mode.
        /// </summary>
        byte ProcessInstanceMode
        {
            get;
        }

        /// <summary>
        /// Get a <see cref="int"/> that contains the 
        /// version of definition assembly
        /// </summary>
        decimal Version { get; set; }

        /// <summary>
        /// Represents the property that holds global Variables
        /// </summary>
        Dictionary<string, object> GlobalVariables { get; set; }

        /// <summary>
        /// Represent the property that holds the user 
        /// variables.
        /// </summary>
        Dictionary<string, object> UserVariables { get; set; }

       

        #endregion
    }
}
