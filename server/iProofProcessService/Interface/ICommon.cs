

namespace CPS.Proof.DFSExtension
{
    using SRA.Proof.Helpers;
    using System.Collections.Generic;
    public interface ICommon
    {
        SlotToken Authorize(string strslotToken, 
            UserActivities activity);

        void GetAllVariables(SlotToken token, 
            ref Dictionary<string,ServiceElementData> dicParams, 
            out Dictionary<string, object>  globalVariables, out Dictionary<string, object> userVariables );

        void RemoveVariables(ref Dictionary<string, ServiceElementData> dicParams,
             Dictionary<string, object> globalVariables,  Dictionary<string, object> userVariables);

        object? GetObjectValue(object? obj,int dataTypeId);
    }
}
