using log4net;
using SRA.Proof.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Proof.DFSExtension
{
    public abstract class ExtensionActivityBase
    {
        private ILog _logger = LogManager.GetLogger(typeof(ExtensionActivityBase));

        public abstract List<Triplet<string, short, short?>>
            GetQueryExpressionBindings(string expressionId);

        /// <summary>
        /// Method to check is given formversionId is valid,
        /// if given formversion is invalid then fallback 
        /// and return the next valid formversion
        /// </summary>
        /// <param name="formVersionId"></param>
        /// <param name="viewPort">A<see cref="ViewportTypes"/>holds the user
        /// viewport </param>
        /// <returns>Returns the values of Get Valid Form Version Id </returns>
        public abstract string GetValidFormVersionId(string formVersionId,
            ViewportTypes viewPort);



    }
}
