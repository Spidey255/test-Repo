using CPS.Proof.DFSExtension;
using SRA.Proof.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Proof.DFSExtension
{
    public abstract class ExtObjectFactoryBase :IExtObjectFactory
    { 
        /// <summary>
        /// Represents the method that fetches process 
        /// definition details.
        /// </summary>
        /// <param name="packageProcessMapId">
        /// A <see cref="string"/> instance that hold 
        /// package processs map identifier.
        /// </param>
        /// <returns>
        /// Process instance is returned.
        /// </returns>
        public abstract IExtBaseMetaData GetProcessInstance(string packageProcessMapId);
       
        /// <summary>
        /// Represents the method that fetches process 
        /// definition details.
        /// </summary>
        /// <param name="packageProcessMapId">
        /// A <see cref="string"/> instance that hold 
        /// package processs map identifier.
        /// </param>
        /// <returns>
        /// Process instance is returned.
        /// </returns>
        //public abstract Status GetAllVariables(SlotToken token,
        //    out Dictionary<string, object> globalVariables,
        //        out Dictionary<string, object> userVariables);

        /// <summary>
        /// Represents the method that is used to get the 
        /// dfs virtual instance.
        /// </summary>
        /// <param name="processActivityMapId">
        /// A <see cref="string"/> that contains the 
        /// processActivityMapid
        /// </param>
        /// <returns>
        /// A new virtual instance is returned.
        /// </returns>
        public abstract IVirtualPage GetDfsVirtualInstance
            (string processActivityMapId);

        /// <summary>
        /// Represents the method that is used to get the 
        /// query expression data source. 
        /// </summary>
        /// <param name="expressionId">
        /// A <see cref="string"/> that contains the 
        /// expression id.
        /// </param>
        /// <returns></returns>
        public abstract Dictionary<string, string> GetQueryExpressionDataSource
            (string expressionId);

        /// <summary>
        /// Represents the method that is used to get the 
        /// query expression data source. 
        /// </summary>
        /// <param name="expressionId">
        /// A <see cref="string"/> that contains the 
        /// expression id.
        /// </param>
        /// <returns></returns>
        public abstract Tuple<string, string> GetComboDataSource(string ElementName);

         /// <summary>
        /// Represents the method that is used to get the 
        /// query expression data source. 
        /// </summary>
        /// <param name="expressionId">
        /// A <see cref="string"/> that contains the 
        /// expression id.
        /// </param>
        /// <returns></returns>
        public abstract int GetGridRPP(string ElementName);
    }
}
