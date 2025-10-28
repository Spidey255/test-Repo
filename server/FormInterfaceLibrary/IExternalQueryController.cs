using System.Data;
using System.Collections.Generic;
using System;

namespace CPS.Proof.DFSExtension
{
    public interface IExternalQueryController
    {
        /// <summary>
        /// Represents the method that is used to get
        /// the grid data source based on the given 
        /// query and connection string.
        /// </summary>
        /// <param name="query">
        /// A <see cref="string"/>that holds the Query.
        /// </param>
        /// <param name="connectionString">
        /// A <see cref="string"/> that holds the connectionString
        /// </param>
        /// <param name="queryResults">
        /// A <see cref="List{O}"/> that holds the Query Results.
        /// </param>
        Status GetGridDataSource(ConnectionString connectionstring,
            string query, out DataTable queryResult);

         /// <summary>
        /// Represents the method that is used to execute 
        /// the query based on the given details.
        /// </summary>
        /// <param name="query">
        /// A <see cref="string"/>that holds the Query.
        /// </param>
        /// <param name="connectionString">
        /// A <see cref="string"/> that holds the connectionString
        /// </param>
        /// <param name="queryResults">
        /// A <see cref="Dictionary{S,O}"/> that holds the Query Results.
        /// </param>
        /// <returns>Execution Status</returns>
        Status ExecuteQuery(string query, 
            string connectionString,
                out Dictionary<short, object> queryResults);


         /// <summary>
        /// Represents the method that retrieves the System
        /// Application Settings Details.
        /// </summary>
        /// <param name="ProofconnectionId">
        /// A <see cref="string"/> 
        /// that contains the proof connection Id
        /// </param>
        /// <param name="AppConnectionId">
        /// A <see cref="string"/> 
        /// that contains the Application  connectionsettings Id
        /// </param>
        /// <returns>
        /// A <see cref="ConnectionString"/> that holds the Application Connection string
        /// </returns>

        ConnectionString ConnectionById(string ProofconnectionId,
                   string AppConnectionId);


        Status GetElementClientData(string instanceId, string widgetId, out string formData);

        Status GetComboDataSource(SlotToken token, Tuple<string,string> comboSource,
            out DataTable queryResult);


        Status ExecuteQuery(string connectionString,
            string query,
                      out DataTable queryResults);

        Status SaveFormInstanceData(SlotToken token,  Context context,string formJsondata);

         Status GetGridInstanceData(SlotToken token,  Context context,
            out DataTable queryResult,out DataTable pagination);

        Status GetFormInstanceData(SlotToken token, Context context,
            out DataTable queryResult);

    }

}