namespace CPS.Proof.DFSExtension
{
    using System;
    using System.Collections.Generic;  
    using System.Data;  
    using System.Text;
    using System.Text.Json; 
    using log4net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;   
    using SRA.Proof.Middleware;
       

     /// <summary>
    /// Represents the controller that handles iProofServicesHub
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class iProofServicesHubController : ControllerBase
    {

       #region Private Members

        /// <summary>
        /// A <see cref="ILog"/> instance used for application logging.
        /// </summary>
        private readonly ILog _sysLog;

       
        
        private string _ProcessMapId;


        #endregion

        #region Constructor

        /// <summary>
        /// Represents the constructor that creates an instance
        /// of ProjectsExplorer Controller.
        /// </summary>
        /// <param name="configuration">
        /// A <see cref="IConfiguration"/> that holds  
        /// destination details.
        /// </param>
        public iProofServicesHubController(IConfiguration configuration)
        {
            _sysLog = LogManager.GetLogger(this.GetType());       
           
        }

        #endregion


        /// <summary>
        /// Represents the method that Executes Form Load and returns Response
        /// by posting message in queue.
        /// </summary>
        /// <param name="context">              
        /// A <see cref="Context"/> that contains details to be sent   
        /// </param> 
        /// <returns>
        /// Returns the Response
        /// </returns>
        [HttpPost]
        [Route("ExecuteFormLoad")]
        public Response ExecuteFormLoad(Context context)
        {
            _sysLog.Debug("Entering ExecuteFormLoad");

            IVirtualPage virtualpageinstance = null;

            ICommon common=null;
            try
            {
                JSONSerializer serializer = new JSONSerializer();

                var input = serializer.Serialize<Context>(context).ToString();

                _sysLog.DebugFormat("ExecuteFormLoad Control - {0} Context -{1} ",context.ControlId, input);

                common=ObjectManager.Acquire<ICommon>();

                var token = common.Authorize(context.SlotId, null);

                
               
                switch(context.PackageProcessMapId)
                {
            
                                      case "B0EDBC7F-E6F9-4F68-92D1-C2D70DA0933A":   var  objectFactoryAssetVerification=new AssetVerificationObjectFactory();
                                             virtualpageinstance = objectFactoryAssetVerification.GetDfsVirtualInstance
                                                        (context.ProcessActivityMapId);

                                            virtualpageinstance._objectFactory = objectFactoryAssetVerification;
                                            break;

                                     case "B219A0EA-0254-4F69-B989-B681DD475183":   var  objectFactoryProjectsExplorer=new ProjectsExplorerObjectFactory();
                                             virtualpageinstance = objectFactoryProjectsExplorer.GetDfsVirtualInstance
                                                        (context.ProcessActivityMapId);

                                            virtualpageinstance._objectFactory = objectFactoryProjectsExplorer;
                                            break;

                 
                }

                
                 this._ProcessMapId = context.PackageProcessMapId;

                Dictionary<string, ServiceElementData> refParams =
                    new Dictionary<string, ServiceElementData>();

                foreach (var item1 in context.Params)
                {
                    string dickey = string.Empty;                   

                    refParams.Add(item1.ElementName, new ServiceElementData
                    {
                        ElementName = item1.ElementName,

                         EDT= item1.EDT,
                        
                        Value = common.GetObjectValue(item1.Value,item1.EDT),

                        Visible = null,

                        Enbl = null
                       
                    });
                    
                }

                refParams.Add("FormVersionId", new ServiceElementData { Value = context.FormVersionId });

                refParams.Add("InstanceId", new ServiceElementData { Value = context.FormInstanceId });

                refParams.Add("Message", new ServiceElementData { Value = string.Empty });

                input = serializer.Serialize(refParams).ToString();

                _sysLog.DebugFormat("Input Parameter -{0} ", input);               

                //common.GetAllVariables(token, ref refParams, out globalVariables, out userVariables);

                virtualpageinstance.ExecuteMethod("formonload", context.ControlId, ref refParams);

                common.RemoveVariables(ref refParams, globalVariables, userVariables);

                refParams.Remove("FormVersionId");

                refParams.Remove("InstanceId");

                Response response = new Response();

                response.Rows = new List<ServiceElementData>();

                foreach (var item in refParams)
                {
                    if (item.Value.ElementName != null)
                    {
                        foreach (var controltype in context.Params)
                        {
                            if (item.Value.ElementName == controltype.ElementName)
                            {

                                item.Value.ControlId = controltype.ControlId;

                                continue;
                            }
                        }
                        response.Rows.Add(item.Value);
                    }
                }



                //var output = serializer.Serialize(response).ToString();

                //_sysLog.DebugFormat("ExecuteFormLoad Control- {0} Response-{1}", context.ControlId, output);

                return response;


            }
            catch (Exception ex)
            {
                _sysLog.ErrorFormat("Error in ExecuteFormLoad {0} {1} {2}", ex, context.SlotId,context.ControlId);

                return null;
            }
            finally
            {
                if (virtualpageinstance != null)
                    ObjectManager.Release<IVirtualPage>(virtualpageinstance);

                _sysLog.Debug("Exiting ExecuteFormLoad");
            }
        }


         [HttpPost]
        [Route("ExecuteMultiRequest")]
        public Response ExecuteMultiRequest(Context context)
        {
            _sysLog.Debug("Entering ExecuteMultiRequest");

            IVirtualPage virtualpageinstance=null;

            ICommon common=null;

            try
            {
                JSONSerializer serializer = new JSONSerializer();

                var input = serializer.Serialize<Context>(context).ToString();

                _sysLog.DebugFormat("Manage Multirequest Control - {0} Context-{1} ",context.ControlId, input);
                
                
                common=ObjectManager.Acquire<ICommon>();

                var token = common.Authorize(context.SlotId, null);
                

                switch(context.PackageProcessMapId)
                {
            
                                      case "B0EDBC7F-E6F9-4F68-92D1-C2D70DA0933A":   var  objectFactoryAssetVerification=new AssetVerificationObjectFactory();
                                             virtualpageinstance = objectFactoryAssetVerification.GetDfsVirtualInstance
                                                        (context.ProcessActivityMapId);
                                            virtualpageinstance._objectFactory = objectFactoryAssetVerification;
                                            break;

                                     case "B219A0EA-0254-4F69-B989-B681DD475183":   var  objectFactoryProjectsExplorer=new ProjectsExplorerObjectFactory();
                                             virtualpageinstance = objectFactoryProjectsExplorer.GetDfsVirtualInstance
                                                        (context.ProcessActivityMapId);
                                            virtualpageinstance._objectFactory = objectFactoryProjectsExplorer;
                                            break;

                 
                }

                this._ProcessMapId = context.PackageProcessMapId;

                Dictionary<string,ServiceElementData> refParams= 
                    new Dictionary<string, ServiceElementData>();

                 foreach (var item1 in context.Params)
                {
                    string dickey = string.Empty;                   

                    refParams.Add(item1.ElementName, new ServiceElementData
                    {
                        ElementName = item1.ElementName,
                        
                        EDT= item1.EDT,
                        
                        Value = common.GetObjectValue(item1.Value,item1.EDT),

                        Visible = null,

                        Enbl = null
                       
                    });

                    
                }

               
                refParams.Add("FormVersionId", new ServiceElementData { Value = context.FormVersionId });

                refParams.Add("InstanceId", new ServiceElementData { Value = context.FormInstanceId });

                refParams.Add("Message", new ServiceElementData { Value = string.Empty });

                input = serializer.Serialize(refParams).ToString();

                _sysLog.DebugFormat("Input Parameter -{0} ", input);

               // GetAllVariables(token, ref dicParams,out globalVariables,out userVariables);

               var action = context.Action.ToLower();

               virtualpageinstance.ExecuteMethod(action, context.ControlId, ref refParams);

                common.RemoveVariables(ref refParams, globalVariables, userVariables);

                refParams.Remove("FormVersionId");

                refParams.Remove("InstanceId");                          
                              

                Response response = new Response();

                response.Rows = new List<ServiceElementData>();

                foreach (var item in refParams)
                {
                    if (action.ToLower().Equals("onclick"))
                    {
                       if(item.Value !=null)
                        {
                            var itemdet = new ServiceElementData();

                            itemdet = item.Value;

                            if(itemdet.ToString().Contains("ISpace.html"))
                            {
                                itemdet.ToString().Replace("~", "https://dev.iprooflowcode.com/Default");
                               // item.Value = itemdet;
                            }

                        }
                    }
                    response.Rows.Add(item.Value);
                }
                var output = serializer.Serialize(response).ToString();
                _sysLog.DebugFormat("Manage Multirequest Control -{0} Response-{1} ",context.ControlId, output);

                return response;

            }
            catch (Exception ex)
            {
                _sysLog.ErrorFormat("Error in  ExecuteMultiRequest in Control {0} {1}",context.ControlId, ex);
                               
                return new Response { ExecutionMessage = "Multirequest Failed" };                    
                   
            }
            finally
            {
                _sysLog.Debug("Exiting ExecuteMultiRequest");

                if(virtualpageinstance !=null)
                    ObjectManager.Release<IVirtualPage>(virtualpageinstance);
            }
        }

        [HttpPost]
        [Route("GetComboDetails")]
        public string GetComboDetails(Context context)
        {
            _sysLog.Debug("Entering GetComboDetails");

            DataTable queryresult = null;

            IExternalQueryController externalQueryController = null;

            ICommon common = null;

            Tuple<string,string> combosource=null;

            try
            {

                var status = Status.Failure;

                externalQueryController = ObjectManager.Acquire<IExternalQueryController>();

                common = ObjectManager.Acquire<ICommon>();                                

                var token = common.Authorize(context.SlotId, null);

                 switch(context.PackageProcessMapId)
                {
            
                                     
                    case "B0EDBC7F-E6F9-4F68-92D1-C2D70DA0933A":   
                                             foreach (var item in context.Params)
                                             {
                                            var  objectFactoryAssetVerification=new AssetVerificationObjectFactory();
                                             combosource = objectFactoryAssetVerification.GetComboDataSource
                                                        (item.ElementName);  
                                             }
                                            break;
                   
                                    
                    case "B219A0EA-0254-4F69-B989-B681DD475183":   
                                             foreach (var item in context.Params)
                                             {
                                            var  objectFactoryProjectsExplorer=new ProjectsExplorerObjectFactory();
                                             combosource = objectFactoryProjectsExplorer.GetComboDataSource
                                                        (item.ElementName);  
                                             }
                                            break;
                   
                 
                }               
                

                status = externalQueryController.GetComboDataSource
                        (token, combosource, out queryresult);

                if (status == Status.Success)
                {

                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

                    Dictionary<string, object> row;

                    foreach (DataRow dr in queryresult.Rows)
                    {
                        row = new Dictionary<string, object>();

                        foreach (DataColumn col in queryresult.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }

                        rows.Add(row);
                    }

                    var JSONresult = JsonConvert.SerializeObject(rows);


                    return JSONresult;
                }
                else
                    return null;              

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

                if(common != null)
                {
                    ObjectManager.Release<ICommon>(common);
                }

                _sysLog.Debug("Exiting GetComboDetails");
            }
            
        }

        [HttpPost]
        [Route("SaveWidgetInstanceData")]
        public Response SaveWidgetInstanceData(Context context)
        {
            _sysLog.Debug("Entering SaveWidgetInstanceData");

            string formjsonData = "";

            ICommon common=null;

            IExternalQueryController externalQueryController = null;

            try
            {
                common=ObjectManager.Acquire<ICommon>();

                List<ServiceElementData> lstformdata = new List<ServiceElementData>();

                 JSONSerializer serializer = new JSONSerializer();

                foreach (var item1 in context.FormData)
                {
                    

                    formjsonData = serializer.NewtonSerialize<ServiceElementData>(item1);
                }
                                

                externalQueryController = ObjectManager.Acquire<IExternalQueryController>();
                

                var token = common.Authorize(context.SlotId, null);                                
               
                var status = externalQueryController.
                    SaveFormInstanceData(token,context,formjsonData);

                Response response = new Response();

                if (status == Status.Success)
                {                   
                    response.ExecutionMessage = "Form Data Saved Successfully";

                    return response;
                }


                              
                return response;

            }
            catch (Exception ex)
            {
                return new Response {ExecutionMessage="Save Failed" };
            }
            finally
            {
                 if (externalQueryController != null)
                {
                    ObjectManager.Release<IExternalQueryController>(externalQueryController);
                }

                if(common != null)
                {
                    ObjectManager.Release<ICommon>(common);
                }
            }
        }

        [HttpPost]
        [Route("GetGridInstanceData")]
        public Response GetGridInstanceData(Context context)
        {
            _sysLog.Debug("Entering GetGridInstanceData");

            ICommon common=null;

            IExternalQueryController externalQueryController = null;

            try
            {
                common=ObjectManager.Acquire<ICommon>();

                JSONSerializer serializer = new JSONSerializer();                        

                externalQueryController = ObjectManager.Acquire<IExternalQueryController>();                

                var token = common.Authorize(context.SlotId, null);  
                
                DataTable table = null;

                DataTable pagination = null;
               
                var status = externalQueryController.
                    GetGridInstanceData(token,context,out table,out pagination);                       
                              

                var JSONString = new StringBuilder();
                
                if (table.Rows.Count > 0)
                {
                    JSONString.Append("[");

                    JSONString.Append("{\"ElementName" + "\":" + "\"" + context.ControlId + "\",");

                    if (pagination.Rows.Count > 0)
                    {
                        for (int i = 0; i < pagination.Rows.Count; i++)
                        {
                           
                                JSONString.Append("\"PageCount" + "\":" + "" + pagination.Rows[i]["PageCount"].ToString() + ",");

                                JSONString.Append("\"RecordsFrom" + "\":" + "" + pagination.Rows[i]["RecordsFrom"].ToString() + ",");

                                JSONString.Append("\"RecordsTo" + "\":" + "" + pagination.Rows[i]["RecordsTo"].ToString() + ",");

                                JSONString.Append("\"TotalRecords" + "\":" + "" + pagination.Rows[i]["TotalRecords"].ToString() + ",");
                            
                        }
                    }

                    JSONString.Append("\"Child" + "\":" + "[");

                    for (int i = 0; i < table.Rows.Count; i++)
                    {                       

                        JSONString.Append("{\"RwId" + "\":" + "\"" + table.Rows[i]["RowId"].ToString() + "\"," + "\"" + "SEQ" + "\":" +  Convert.ToInt32(table.Rows[i]["Sequence"]) +",");
                        

                        JSONString.Append("\"Child" + "\":" + "[");


                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (table.Columns[j].ColumnName.ToString() == "RowId" ||
                                table.Columns[j].ColumnName.ToString() == "Sequence")
                                continue;

                            if (j < table.Columns.Count - 1)
                            {
                                JSONString.Append("{\"ElementName"+ "\":" +"\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"" + "EDT" + "\":" + "9" + ",\"Value" + "\":"+"\"" + table.Rows[i][j].ToString().Replace("\"","\\\"") + "\"},");
                            }
                            else if (j == table.Columns.Count - 1)
                            {
                                JSONString.Append("{\"ElementName" + "\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"" + "EDT" + "\":" + "9" + ",\"Value" + "\":" + "\"" + table.Rows[i][j].ToString().Replace("\"", "\\\"") + "\"}");
                            }
                        }
                       
                        if (i == table.Rows.Count - 1)
                        {
                            JSONString.Append("]}");
                        }
                        else
                        {
                            JSONString.Append("]},");
                        }
                    }
                    JSONString.Append("]}]");

                }
                var res= JSONString.ToString();

              
                var gridObject = serializer.Deserialize<List<ServiceElementData>>(res);

                Response response = new Response();              

                response.Rows = gridObject;               

                if (status == Status.Success)
                {                   
                   
                    return response;
                }
                              
                return response;

            }
            catch (Exception ex)
            {
                return new Response {ExecutionMessage="Grid Load Failed" };
            }
            finally
            {
                 if (externalQueryController != null)
                {
                    ObjectManager.Release<IExternalQueryController>(externalQueryController);
                }

                if(common != null)
                {
                    ObjectManager.Release<ICommon>(common);
                }
            }
        }

          [HttpPost]
        [Route("GetFormInstanceData")]
        public Response GetFormInstanceData(Context context)
        {
            _sysLog.Debug("Entering GetFormInstanceData");

            ICommon common = null;

            IExternalQueryController externalQueryController = null;

            try
            {
                common = ObjectManager.Acquire<ICommon>();

                JSONSerializer serializer = new JSONSerializer();

                externalQueryController = ObjectManager.Acquire<IExternalQueryController>();

                var token = common.Authorize(context.SlotId, null);

                DataTable table = null;

                DataTable pagination = null;

                var status = externalQueryController.
                    GetFormInstanceData(token, context, out table);


                var JSONString = new StringBuilder();

                if (table.Rows.Count > 0)
                {
                    JSONString.Append("[");

                    JSONString.Append("{\"ElementName" + "\":" + "\"" + context.ControlId + "\",");

                   
                    JSONString.Append("\"Child" + "\":" + "[");

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                                               

                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (table.Columns[j].ColumnName.ToString() == "RowId" ||
                                table.Columns[j].ColumnName.ToString() == "Sequence")
                                continue;

                            if (j < table.Columns.Count - 1)
                            {
                                JSONString.Append("{\"ElementName" + "\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"" + "EDT" + "\":" + "9" + ",\"Value" + "\":" + "\"" + table.Rows[i][j].ToString() + "\"},");
                            }
                            else if (j == table.Columns.Count - 1)
                            {
                                JSONString.Append("{\"ElementName" + "\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"" + "EDT" + "\":" + "9" + ",\"Value" + "\":" + "\"" + table.Rows[i][j].ToString() + "\"}");
                            }
                        }
                     
                    }
                    JSONString.Append("]}]");

                }
                var res = JSONString.ToString();


                var formObject = serializer.Deserialize<List<ServiceElementData>>(res);

                Response response = new Response();

                response.Rows = formObject;

               
                if (status == Status.Success)
                {

                    return response;
                }

                return response;

            }
            catch (Exception ex)
            {
                return new Response { ExecutionMessage = "Form Load Failed" };
            }
            finally
            {
                if (externalQueryController != null)
                {
                    ObjectManager.Release<IExternalQueryController>(externalQueryController);
                }

                if (common != null)
                {
                    ObjectManager.Release<ICommon>(common);
                }
            }
        }


         public Dictionary<string, object> globalVariables =new Dictionary<string,object> 
		{
			                    {"gv_slotid", ""},
                                   {"gv_usermapid", ""},
                                   {"gv_userid", ""},
                                   {"gv_displayname", ""},
                                   {"gv_username", ""},
                                   {"gv_loggedinrolename", ""},
                                   {"gv_packageid", ""},
                                   {"gv_processmapid", ""},
                                   {"gv_activitymapid", ""},
                                   {"gv_instanceid", ""},
                                   {"gv_instanceurl", ""},
                                   {"gv_packagename", ""},
                                   {"gv_processname", ""},
                                   {"gv_activityname", ""},
                                   {"gv_activitytype", ""},
                                   {"gv_processtype", ""},
                                   {"gv_formversion", ""},
                                   {"gv_destinationactivity", ""},
                                   {"gv_tenantid", ""},
                                   {"gv_token", ""},
                                   {"gv_elapsedtime", ""},
                                   {"gv_loggedinrolemapid", ""},
                                   {"gv_age", ""},
                                   {"gv_ipaddress", "iproof-axon.com"},
                                   {"gv_ip", "http://cpsedu.in/Default/PageManager.html#"},
                                   {"gv_ipaddress1", "uat.erp.iith.ac.in"},
                                   {"gv_variableip", "cpsedu.in"},
                                   {"gv_campusid", "47E64798-8956-4B7F-9DC6-EA987BDD89B7"},
                                   {"gv_variableip1", "http://cpsedu.in/Default/Pages"},
                                   {"gv_entity_type", ""},
                                   {"gv_ipdefault", "http://iproofaxon.info/Default"},
                                   {"gv_newid", "newId()"},
                                   {"gv_urllink", "https://analytics.zoho.in/open-view/250300000000189994?ZOHO_CRITERIA=\"Roll No\"='"},
                                   {"gv_academiclandingpage", "~/Pages/DynamicPage/ISpace.html?PkActMId=07FD4342-7154-4242-9855-985C7EEDEB34&amp;frmElementId=E2EF4EB3-A7D5-4D7B-8C85-2F00E8C12D72&amp;PkPrMId=56A504C0-737D-4966-A6B1-94B2108007A7&amp;formVersionId=393849FA-6127-4DD6-8039-392C9F9146AD&amp;Ver=0.0000"},
                                   {"gv_academicslandingpage", "~/Pages/DynamicPage/ISpace.html?PkActMId=8DDCA45D-6AC2-43D8-8F65-A8E7559BD7FB&frmElementId=40BCDFD7-C44E-43C2-A8CA-501B775EE852&PkPrMId=A0A04081-4ED0-4533-8753-B18FA2D02BC1&formVersionId=39D1D926-FDA2-48FD-82D1-546BFD96CD12&Ver=0.0000"},
                                   {"gv_studentlandingpage", "~/Pages/DynamicPage/ISpace.html?PkActMId=B9EA67FF-8D2B-4BF6-924A-1693B52994E8&frmElementId=B056E54B-5075-4B81-A42B-A6DAED5B95D5&PkPrMId=3A38C0DC-4E9D-465C-B81E-19E61B772A5D&formVersionId=9E98BD88-4998-4946-864E-52A59CDD6B6D&Ver=0.0000"},
                                   {"gv_configurationlandingpage", "~/Pages/DynamicPage/ISpace.html?PkActMId=26434078-80AB-4505-BDCF-314EE46863E8&frmElementId=8A60FAAF-7899-47E7-8462-F76D056C4E66&PkPrMId=1A8AEE90-18EB-449C-B73C-98CBEB6DF729&formVersionId=3779&Ver=0.0000"},
                                   {"gv_facultylandingpage", "~/Pages/DynamicPage/ISpace.html?PkActMId=0786232F-531C-42C1-8E0B-EDEFF9E6384B&frmElementId=A08A6DB0-D4C3-49A5-ADF6-DAE2060F6172&PkPrMId=9521BA98-12E4-48F9-BDB9-AB82247B04D4&formVersionId=6CE27D40-1472-40D7-85A9-3BBCF132C38C&Ver=0.0000"},
                                   {"gv_deanacademicslandingpage", "~/Pages/DynamicPage/ISpace.html?PkActMId=594C7110-9495-4603-9B10-79D967697C2A&frmElementId=DC5EAFE2-7D70-458D-B172-407C823708DA&PkPrMId=8305A58F-0D8C-49C7-8055-B414CB04BB77&formVersionId=892DC5CC-FC26-4A4D-9B9E-0831626A2381&Ver=0.0000"},
                                   {"gv_configuration", "~/Pages/DynamicPage/ISpace.html?PkActMId=26434078-80AB-4505-BDCF-314EE46863E8&frmElementId=8A60FAAF-7899-47E7-8462-F76D056C4E66&PkPrMId=1A8AEE90-18EB-449C-B73C-98CBEB6DF729&formVersionId=3779&Ver=0.0000"},
                                   {"gv_mainlandingpage", "~/Pages/DynamicPage/ISpace.html?PkActMId=60986D0B-5484-4506-A5C6-B8989F07BDF0&frmElementId=9B4DCF6F-0EE2-4471-BF58-8526E6FCF821&PkPrMId=FFBAEB27-9FAB-49C3-895F-C3845702A0C5&formVersionId=0E987F43-BA01-4FFD-84E7-41C2CBC5362D&Ver=0.0000"},
                                   {"gv_coolingdays", "10"},
                                   {"gv_mtrfilepath", "C:\\\\iProofDocs\\\\CopyDocs\\\\"},
                                   {"gv_transactionsreportdays", "7"},
                                   {"gv_sede", "30"},
                                   {"gv_defaultpassword", "Vkm6E7NRsN0ezGDts0HJVQ=="},
                                   {"gv_countryid", "1"},
                                   {"gv_excomdoc", "1"},
                                   {"gv_procomdoc", "3"},
                                   {"gv_financetypeid", "1"},
                                   {"gv_thresholdamt", "1000000.00"},
                                   {"gv_customerroleid", "15"},
                                   {"gv_doccontrolid", "2"},
                                   {"gv_idfloantypeid", "3"},
                                   {"gv_roleidproductcreation", "107"},
                                   {"gv_businesscustomerid", "1"},
                                   {"gv_currencycode", "ZMW"},
                                   {"gv_offerdltmapid", "C04C3BC3-8363-4773-86DB-94C132CB16D8"},
                                   {"gv_authdltmapid", "6A7DCC3B-A0B6-42C9-ADDA-15DDC5EC31B5"},
                                   {"gv_smeccapltdamount", "50000"},
                                   {"gv_smemdapltdamount", "100000"},
                                   {"gv_myresearchstudents", "~/Pages/DynamicPage/ISpace.html?PkActMId=D8736CD6-121B-42F9-BFE5-B4FE2424DC32&frmElementId=47A7E5B3-DED6-487A-B848-9C39DBEBEC82&PkPrMId=933F192F-8652-48B9-97ED-5221D9B55F2F&formVersionId=E1450AA8-CD0E-4A88-97EA-0C6802FBE9FF&Ver=0.0000"},
                                   {"gv_academicofficelandingpage", "~/Pages/DynamicPage/ISpace.html?PkActMId=B8B552B7-C097-414E-8857-B0AAB561CEF5&frmElementId=F1C6AE09-790D-4F9F-80B0-9E38DBFEA9EB&PkPrMId=A8FC08BD-605F-4501-8D31-2DC93CB8A8A5&formVersionId=6CC5CD76-878A-4999-AEE4-CC351F43E78B&Ver=0.0000"},
                                   {"gv_approvalscreen", "~/Pages/DynamicPage/ISpace.html?PkActMId=29ADB6CD-92B9-4D3E-BCEB-A4C9D7172BAF&frmElementId=1C8B61D9-75B8-42A3-A727-D59331B59750&PkPrMId=46610986-0001-4F7C-BADA-5CB0FC6C2D0F&formVersionId=B0D50448-7256-4D5D-AD67-1EE606C5E72C&Ver=0.0000"},
                 		};	


         public Dictionary<string, object> userVariables =new Dictionary<string,object> 
		{
			  		};	


    }
}

