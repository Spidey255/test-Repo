#region Header
// --------------------------------------------------------------------------------------
// <copyright file="ISpace.cs" company="Cognitive Platform Solutions Pvt Ltd">
// Reproduction or transmission, in whole or in part, in any form or 
// by any means including electronic or mechanical or otherwise, is 
// prohibited without written permission from Cognitive Platform Solutions Pvt Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------
#endregion


using System.IO;
using System.Xml;
using System.Web;
using System.Xml.Linq;
using System.Resources;
using System.Threading;     
using System.Transactions;


namespace CPS.Proof.DFSExtension
{
    using System;
    using log4net;
    using System.Collections.Generic;
    using System.Linq;  
    using System.Data; 
    using System.Threading.Tasks;
    using SRA.Proof.Common;
    using System.Text.RegularExpressions;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Configuration;
    using Newtonsoft.Json;    
    using System.Globalization;
    using Newtonsoft.Json.Linq;
    using SRA.Proof.Helpers;
    using SRA.Proof.Middleware;

    // ReSharper disable once InconsistentNaming
    public class ISpace : CPS.Proof.DFSExtension.IISpace
    {

        /// <summary>
        /// Member variable for Log instance
        /// </summary>
        private readonly ILog _log;       

        public ISpace()
        {
            _log = LogManager.GetLogger(GetType());
        }

        /// <summary>
        /// Represents the method that gets the attribute type based on 
        /// given property.
        /// </summary>
        /// <param name="property">
        /// A <see cref="string"/> that holds property string.
        /// </param>
        /// <returns>
        /// Returns ElementAttributeType
        /// </returns>
        private ElementAttributeType GetAttributeType(string property)
        {
            ElementAttributeType attributeType = ElementAttributeType.None;

            switch (property.ToLower())
            {
                case "backcolor":
                    attributeType = ElementAttributeType.BackColor;
                    break;

                case "forecolor":
                    attributeType = ElementAttributeType.ForeColor;
                    break;

                case "bordercolor":
                    attributeType = ElementAttributeType.BorderColor;
                    break;

                case "enable":
                    attributeType = ElementAttributeType.Enable;
                    break;

                case "visible":
                    attributeType = ElementAttributeType.Visible;
                    break;

                case "mandatory":
                    attributeType = ElementAttributeType.Mandatory;
                    break;

                case "css":
                    attributeType = ElementAttributeType.CSS;
                    break;

                case "dcss":
                    attributeType = ElementAttributeType.CSS;
                    break;

                case "displayformat":
                    attributeType = ElementAttributeType.DisplayFormat;
                    break;

                case "showdialog":
                    attributeType = ElementAttributeType.ShowDialog;
                    break;

                case "hidedialog":
                    attributeType = ElementAttributeType.HideDialog;
                    break;

                case "showmodal":
                    attributeType = ElementAttributeType.ShowModal;
                    break;

            }

            return attributeType;
        }

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
        public void SetControlProperty(string elementName,
         string property, object controlValue,ref Dictionary<string,ServiceElementData> iSpace)
        {
            _log.Debug("Entering SetControlProperty");
            if (property.ToLower() == "backcolor" || property.ToLower() == "forecolor" || property.ToLower() == "bordercolor")
            {
                controlValue = string.Concat("#", controlValue);                
            }
            ElementAttributeType attributeType = ElementAttributeType.None;
            try
            {
                _log.DebugFormat("Data for SetControlProperty -{0},{1},{2}",
                    elementName, property, controlValue);

                if (string.IsNullOrEmpty(property))
                {
                    _log.ErrorFormat("Attribute property is null for {0}", elementName);

                    return;
                }

                if (controlValue == null)
                {
                    _log.ErrorFormat("Value is null for {0}", elementName);

                    return;
                }

                if (string.IsNullOrEmpty(elementName))
                {
                    _log.Error("Element Name is null");

                    return;
                }

                attributeType = GetAttributeType(property);

               
                var control = iSpace[elementName];

                if (control == null)
                {
                    _log.ErrorFormat("Element not found {0}", elementName);

                    return;
                }

                switch (attributeType)
                {
                    case ElementAttributeType.ForeColor:
                        iSpace[elementName].FCol = controlValue.ToString();
                        break;
                    case ElementAttributeType.BackColor:
                        iSpace[elementName].BaCol = controlValue.ToString();
                        break;
                    case ElementAttributeType.BorderColor:
                        iSpace[elementName].BrCl = controlValue.ToString();
                        break;
                    case ElementAttributeType.Enable:
                        iSpace[elementName].Enbl = controlValue.ToString();
                        break;
                    case ElementAttributeType.Visible:
                        iSpace[elementName].Visible = controlValue.ToString();
                        break;
                    case ElementAttributeType.Mandatory:
                        iSpace[elementName].Man = Convert.ToBoolean(controlValue);
                        break;
                    case ElementAttributeType.CSS:
                        iSpace[elementName].DCss = controlValue.ToString();
                        break;
                    //case ElementAttributeType.Pattern:
                    //    control.pa = controlValue.ToString();
                    //    break;
                    case ElementAttributeType.ShowDialog:
                        iSpace[elementName].ShowDialog = Convert.ToBoolean(controlValue);
                        break;
                    case ElementAttributeType.ShowModal:
                        iSpace[elementName].ShowModal = Convert.ToBoolean(controlValue);
                        break;
                    //case ElementAttributeType.HideDialog:
                    //    control.HideDialog = Convert.ToBoolean(controlValue);
                    //    break;
                    //case ElementAttributeType.DataKey:
                    //    control.DataKey = controlValue.ToString();
                    //    break;
                    //case ElementAttributeType.DisplayFormat:
                    //    control.DisplayFormat = controlValue.ToString();
                    //    break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                _log.Error("Error in SetControlProperty", ex);
            }
            finally
            {
                _log.Debug("Exiting SetControlProperty");
            }

        }

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
        public void SetUIControlProperty(string elementName,
            string property, object value,ref Dictionary<string,ServiceElementData> ISpace)
        {
            try
            {
                _log.Debug("Entering SetUIControlProperty");

                SetControlProperty(elementName, property, value,ref ISpace);

            }
            catch (Exception ex)
            {
                _log.Error("Error in SetUIControlProperty", ex);
            }
            finally
            {
                _log.Debug("Exiting SetUIControlProperty");
            }
        }


        #region Grid functions

        public int Sum(string gridcolumn, string gridvalue)
        {

            return 0;
        }

       
        #endregion


        public DataTable SetGridDataSource(Dictionary<string,string> querySourceDetails, string query)
        {
           

            string connectionId = string.Empty;

            IExternalQueryController externalQueryController = null;

            //Getting the grid data source
            DataTable queryResults = null;

            try
            {

                connectionId = querySourceDetails.Select
                            (item => item.Value).FirstOrDefault();


                string proofConnectionId = querySourceDetails.Select
                     (item => item.Key).FirstOrDefault();

                var connectionString = GetAppConnectionString
                    (proofConnectionId, connectionId);


                if (connectionString == null)
                {
                    _log.Error("Connection String is null or empty");

                    _log.Error("Exiting ExecuteHttpRequest");

                    return null;
                }

                externalQueryController =

                   ObjectManager.Acquire<IExternalQueryController>();

                query = System.Text.RegularExpressions.Regex.Replace(query, "[^\x20-\x7F]+", " ");

                externalQueryController.GetGridDataSource
                    (connectionString, query, out queryResults);


                 var reducedDataTable=queryResults.AsEnumerable().Take(25).CopyToDataTable();

                return reducedDataTable;              


            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (externalQueryController != null)
                {
                    ObjectManager.Release<IExternalQueryController>(externalQueryController);
                }

            }
        }


        public Dictionary<short, object> ExecuteQuery
              (Dictionary<string,string> querySourceDetails, string query, bool? isLookup)
        {


            string connectionId = string.Empty;

            IExternalQueryController externalQueryController = null;

            Dictionary<short, object> queryResults = null;

            try
            {               

                connectionId = querySourceDetails.Select
                            (item => item.Value).FirstOrDefault();


                string proofConnectionId = querySourceDetails.Select
                     (item => item.Key).FirstOrDefault();

                var connectionString = GetAppConnectionString
                    (proofConnectionId, connectionId);


                if (connectionString == null)
                {
                    _log.Error("Connection String is null or empty");

                    _log.Error("Exiting ExecuteHttpRequest");

                    return null;
                }

                externalQueryController =

                   ObjectManager.Acquire<IExternalQueryController>();

                query = System.Text.RegularExpressions.Regex.Replace(query, "[^\x20-\x7F]+", " ");

                externalQueryController.ExecuteQuery
                    (connectionString.Connection, query, out queryResults);


                return queryResults;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (externalQueryController != null)
                {
                    ObjectManager.Release<IExternalQueryController>(externalQueryController);
                }

            }
        }

        //Retrives Application Connectionstring By ConnectionId
        //from Redis Cache
        private ConnectionString GetAppConnectionString
            (string proofConnectionId, string appConnectionId)
        {
            IExternalQueryController appConnectionStrings = null;

            try
            {


                appConnectionStrings = ObjectManager.Acquire<IExternalQueryController>();

                if (appConnectionStrings == null)
                {

                    return null;
                }

                return appConnectionStrings.ConnectionById
                    (proofConnectionId, appConnectionId);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (appConnectionStrings != null)
                    ObjectManager.Release<IExternalQueryController>(appConnectionStrings);
            }
        }

        public void SetGridData(DataTable queryresult,
                   List<Triplet<string, short, short?>> bindings, string elementName, 
                   ref Dictionary<string, ServiceElementData> ISpace)
        {
            try
            {


                ServiceElementData serviceElementData = null;

                if(ISpace.ContainsKey(elementName))
                {
                    serviceElementData = ISpace[elementName];
                }
                else

                    serviceElementData=new ServiceElementData { ElementName = elementName  };

                serviceElementData.Child = new List<ServiceElementData>();

                short rowsequence = 1;

                foreach (DataRow row in queryresult.Rows)
                {

                    var rowItem = new ServiceElementData();

                    rowItem.Child = new List<ServiceElementData>();

                    rowItem.RwId = Guid.NewGuid().ToString();
                    var indexer = bindings.GetEnumerator();
                    while (indexer.MoveNext())
                    {                     

                        var gridcolumn = new ServiceElementData();

                        gridcolumn.ElementName = indexer.Current.FirstValue;

                        gridcolumn.Value = row[indexer.Current.SecondValue];

                        rowItem.Child.Add(gridcolumn);                        

                    }

                    rowItem.SEQ = rowsequence;

                    serviceElementData.Child.Add(rowItem);

                    rowsequence++;
                }

                if (ISpace == null)
                    ISpace = new Dictionary<string, ServiceElementData>();

                if (ISpace.ContainsKey(elementName))
                {
                    ISpace[elementName] = serviceElementData;
                }
                else
                {
                    ISpace.Add(elementName, serviceElementData);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public List<Dictionary<string,object>> Reload
                (string rElementName,string elementQuery,string connectionId)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            
            
            IExternalQueryController externalQueryController = null;

            DataTable queryResults = null;

            try
            {
                var proofConnectionId = AppParams.GetAppValue("ProofConnectionName");

                var connectionString = GetAppConnectionString
                   (proofConnectionId, connectionId);

                externalQueryController =

                   ObjectManager.Acquire<IExternalQueryController>();
                elementQuery = System.Text.RegularExpressions.Regex.Replace(elementQuery, "[^\x20-\x7F]+", " ");

                var status=externalQueryController.ExecuteQuery
                    (connectionString.Connection, elementQuery, out queryResults);

                if (status == Status.Success)
                {
                    foreach (DataRow dr in queryResults.Rows)
                    {
                        Dictionary<string, object>  keyValuePairs = new Dictionary<string, object>();

                        foreach (DataColumn col in queryResults.Columns)
                        {
                            if(keyValuePairs.ContainsKey(col.ColumnName))
                                keyValuePairs[col.ColumnName] = dr[0].ToString();
                            else
                                keyValuePairs.Add(col.ColumnName, dr[col]);
                        }

                        rows.Add(keyValuePairs);
                    }

                   return rows;
                }

                return rows;
            }
            catch
            {
                return null;
            }
        }


    }
}
