using SRA.Proof.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CPS.Proof.DFSExtension
{
    public interface IISpace
    {
        /// <summary>
        /// Represents the method that set the 
        /// data source to the grid.
        /// </summary>
        /// <param name="expressionId">
        /// A <see cref="string"/> that represents the  expression id.
        /// </param>
        /// <param name="query">
        /// A <see cref="string"/> that contains the query 
        /// </param>
        DataTable SetGridDataSource(Dictionary<string, string> querySourceDetails , string query);

        Dictionary<short, object> ExecuteQuery
              (Dictionary<string, string> querySourceDetails, string query, bool? isLookup);

        /// <summary>
        /// Represents the method that set the grid data to 
        /// the respective columns.
        /// </summary>
        /// <param name="queryExpressionId">
        /// A <see cref="string"/> that represents the quary expression id.
        /// </param>
        /// <param name="bindingId">
        /// A <see cref="string"/> that represents the binding id.
        /// </param>
        /// <returns>
        /// Sum of the given query Expression Id and binding id.
        /// </returns>
        void SetGridData(DataTable queryresult,
            List<Triplet<string, short, short?>> bindings, string elementId,
            ref Dictionary<string, ServiceElementData> ISpace);


        /// <summary>
        /// Represents a method the sets property and value to control.
        /// </summary>
        /// <param name="elementName">
        /// A <see cref="string"/> that represents element name.
        /// </param>
        /// <param name="property">
        /// A <see cref="string"/> that represents property name.
        /// </param>
        /// <param name="value">
        /// A <see cref="string"/> that represents the property value.
        /// </param>
        void SetControlProperty(string elementName,
         string property, object controlValue, ref Dictionary<string, ServiceElementData> iSpace);
        /// <summary>
        /// Represents a method the sets property and value to UI control.
        /// </summary>
        /// <param name="elementName">
        /// A <see cref="string"/> that represents element name.
        /// </param>
        /// <param name="property">
        /// A <see cref="string"/> that represents property name.
        /// </param>
        /// <param name="value">
        /// A <see cref="string"/> that represents the property value.
        /// </param>
        void SetUIControlProperty(string elementName,
            string property, object value, ref Dictionary<string, ServiceElementData> ISpace);

         List<Dictionary<string, object>> Reload
                (string rElementName, string elementQuery, string connectionId);

    }
}
