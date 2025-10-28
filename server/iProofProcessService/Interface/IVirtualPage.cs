namespace CPS.Proof.DFSExtension
{
using System.Collections.Generic;
using System.Data;
using SRA.Proof.Helpers;
    public interface IVirtualPage
    {

        IExtObjectFactory _objectFactory { get; set; }
     
        void ExecuteMethod(string action, string element,
            ref Dictionary<string,ServiceElementData> dfsparams);
        
        DataTable GetComboDetail();     
      
        Dictionary<short, object> ExecuteQueryBinding
            (string queryExpressionId, Dictionary<short, object> dic);

        Dictionary<short, object> ExecuteQuery
               (string expressionId, string query, bool? isLookup);

        List<Dictionary<string, string>>? GetElementClientData
                (string instanceId, string widgetId);

    }
}
