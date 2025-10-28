using System.Collections.Generic;

namespace CPS.Proof.DFSExtension
{
    /// <summary>
    /// Represents the class that implements GridColumn Model
    /// </summary>
    public class Response
    {
        
        public List<ServiceElementData> Rows { get; set; }

        //public List<ServiceElementData> FormData { get; set; }

        public string ExecutionMessage { get; set; }

        public string Message { get; set; }

     
        public string RedirectURL { get; set; }

        public string NavigationType { get; set; }
    }
}
