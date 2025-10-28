namespace CPS.Proof.DFSExtension
{
    using Newtonsoft.Json;

    public class JSONSerializer
    {
        /// <summary>
        /// Represents the Method to Serialize string  using SpanJson        
        /// </summary>
        /// <returns>
        /// string
        /// </returns>
        public string Serialize<TData>(TData settings)
        {

            return SpanJson.JsonSerializer.NonGeneric.Utf16.Serialize(settings);
        }

        /// <summary>
        /// Represents the Method to Deserialie Json string to type <TData>
        /// </summary>
        /// <returns>
        /// Object of type TData
        /// </returns>
        public TData Deserialize<TData>(string jsonData)
        {


            return (TData)SpanJson.JsonSerializer.NonGeneric.Utf16.Deserialize
                (jsonData, typeof(TData));
        }

        /// <summary>
        /// Represents the Method to Deserialie string  to type DataTable
        /// using NewtonSoft Serialization (since SpanJson does not support for DataTable)
        /// </summary>
        /// <returns>
        /// Object of type TData
        /// </returns>
        public TData DeserializeDataTable<TData>(string jsonData)
        {

            return NewtonDeserialize<TData>(jsonData);

        }



        /// <summary>
        /// Represents the Method to Serialize DataTable
        /// using NewtonSoft Serialization (since SpanJson does not support for DataTable)
        /// </summary>
        /// <returns>
        /// string
        /// </returns>
        public string SerializeDataTable<TData>(TData settings)
        {
            return NewtonSerialize<TData>(settings);

        }

        /// <summary>
        /// Represents the Method to Deserialize string
        /// to type <TData>
        /// using NewtonSoft Serialization (since SpanJson does not support for DataTable)
        /// </summary>
        /// <returns>
        /// Object of type <TData>       
        /// </returns>
        public TData NewtonDeserialize<TData>(string jsonData)
        {

            return JsonConvert.DeserializeObject<TData>(jsonData);
        }

        /// <summary>
        /// Represents the Method to Serialize object of type TData        
        /// using NewtonSoft Serialization (since SpanJson does not support for DataTable)
        /// </summary>
        /// <returns>
        /// string
        /// </returns>
        public string NewtonSerialize<TData>(TData settings)
        {

            return JsonConvert.SerializeObject(settings);
        }


    }
}