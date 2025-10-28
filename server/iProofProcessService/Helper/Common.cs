using Newtonsoft.Json.Linq;
using SRA.Proof.Helpers;
using CPS.Proof.DFSExtension;
using System;
using System.Net;
using SRA.Proof.Middleware;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Text.Json;
using System.Globalization;


namespace CPS.Proof.DFSExtension
{
    public class Common:ICommon
    {

        /// <summary>
        /// A <see cref="ILog"/> instance used for application logging.
        /// </summary>
        private readonly ILog _sysLog;

        private string _ProcessMapId;

         private string _SlotId;

        /// <summary>
        /// Property to get and set the SlotToken on every validate function
        /// If exception occurs after validation operatio,
        /// updating the exception in useractivitylog using this token
        /// </summary>
        protected SlotToken SToken
        {
            get;
            set;
        }


        /// <summary>
        /// Represents a method to Decrypt and Deserialize
        /// Slot token
        /// </summary>
        /// <param name="strslotToken">
        /// <see cref="SlotToken"/>contain the current slot token
        /// </param>
        /// <param name="activity">
        /// <see cref="UserActivities"/>contain the activityContext
        /// </param>
        /// <returns>
        /// The <see cref="SlotToken"/>SlotToken
        /// </returns>
        public SlotToken Authorize(string strslotToken, UserActivities activity)
        {
            this._SlotId = strslotToken;
           

            if (string.IsNullOrEmpty(strslotToken))
            {
                throw new Exception("Invalid Slot");
            }

            try
            {

                var isStaySignedIn = strslotToken.Substring(0, 1);

                strslotToken = strslotToken.Remove(0, 1);

                var SID = strslotToken.Substring(0, 36);

                SToken = GetSlotTokenfromDB(strslotToken, activity, isStaySignedIn);
                

               

                if (SToken.UID <= 0 || SToken.UMID <= 0 || string.IsNullOrEmpty(SToken.RlMIds))
                {
                    // Set the response status code as Not authorised 
                    //Response.StatusCode = 401;

                    throw new Exception(String.Format("Invalid Slot Execution - {0}", SToken.UserName));
                }



                return SToken;
            }

            finally
            {
               

            }
        }

        /// <summary>
        /// Represents the GetSlotTokenfromDB Method in case of ChangePassword
        /// </summary>
        /// <param name="strslotToken" cref="string">
        /// holds the SlotId
        /// </param>
        /// <param name="activity" cref="UserActivities">
        /// holds the UserActivities Details
        /// </param>
        /// <returns>
        /// returns SlotToken
        /// </returns>
        private SlotToken GetSlotTokenfromDB
            (string strslotToken, UserActivities activity, string isStaySignedIn)
        {
            DFSExtension.ISlotMonitor slotMonitor = null;

            try
            {
                SlotToken token = new SlotToken();                

                token.SID = strslotToken.Substring(0, 36);

                token.ConnectionName = strslotToken.Substring(36, 36);

                token.RepConnectionName = strslotToken.Substring(72, 36);

                token.IsRoleBased = strslotToken.Substring(108, 1);

                token.RemoteUrl = isStaySignedIn;

                string DbType = "0";

                DbType = strslotToken.Substring(109, 1);

                if (strslotToken.Length > 110)
                    token.RemoteUrl = strslotToken.Substring(110);

                if (DbType == "1")
                    token.DbType = DbTypes.MySql;

                slotMonitor = ObjectManager.Acquire<DFSExtension.ISlotMonitor>();


                //Check if the user is authenticated to login.
                // Gets the default role.
                var isValid = slotMonitor.IsSlotValid(ref token, activity);


                if (!isValid || token.UID <= 0 || token.UMID <= 0 || string.IsNullOrEmpty(token.RlMIds))
                {
                    // Set the response status code as Not authorised 
                    //Response.StatusCode = 401;

                    throw new Exception("Invalid Slot");
                }

                return token;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (slotMonitor != null)
                    ObjectManager.Release(slotMonitor);
            }
        }

       

        public void GetAllVariables(SlotToken token, 
            ref Dictionary<string,ServiceElementData> dicParams, 
            out Dictionary<string, object>  globalVariables, out Dictionary<string, object> userVariables )
        {
            _sysLog.Debug("Entering GetVariables");

           

             globalVariables = null;

             userVariables = null;

            try
            {           
             

             
             
              //   status = iISpaceStoreHandler.LoadVariableDetails
               //     (token,true,false,out globalVariables,out userVariables);

                UpdateGlobalVariables(token,ref globalVariables,ref dicParams);

                if (userVariables != null)
                {

                    foreach (var item in userVariables)
                    {
                        dicParams.Add(item.Key, new ServiceElementData { Value = item.Value.ToString() });
                    }
                }

              


            }
            catch (Exception ex)
            {
                _sysLog.Error("Error in GetVariables",ex);

            }
            finally
            {
                _sysLog.Debug("Exiting GetVariables");

                
            }
        }

        private void UpdateGlobalVariables( SlotToken token,
            ref Dictionary<string, object> globalVariables,
            ref Dictionary<string,ServiceElementData> dictParams)
        {

            if (globalVariables == null)
            {
                _sysLog.Error("Global Variables is null");

                return;
            }

            var keyList = globalVariables.Keys.ToList();

           

            foreach (var data in keyList)
            {
                switch (data)
                {

                    case "gv_slotid":
                        globalVariables[data] = token.SID;
                        break;

                    case "gv_usermapid":
                        globalVariables[data] = token.UMID;
                        break;

                    case "gv_userid":
                        globalVariables[data] = token.UID;
                        break;

                    case "gv_loggedinrolemapid":
                        globalVariables[data] = token.RMId;
                        break;

                    case "gv_displayname":
                        globalVariables[data] = token.DisplayName;
                        break;

                    case "gv_username":
                        globalVariables[data] = token.UserName;
                        break;

                    case "gv_loggedinrolename":
                        globalVariables[data] = token.LoggedInRoleName;
                        break;

                    case "gv_processmapid":
                        globalVariables[data] = this._ProcessMapId;
                        break;

                    //case "gv_processname":
                    //    globalVariables[data] = _objectFactory.ProcessInstance.ProcessName;
                    //    break;

                    //case "gv_packageid":
                    //    globalVariables[data] = _objectFactory.ProcessInstance.PackageId;
                    //    break;

                    //case "gv_activityname":
                    //    if (activityData != null)
                    //        globalVariables[data] = activityData.ActivityName;
                    //    break;

                    //case "gv_activitytype":
                    //    if (activityData != null)
                    //        globalVariables[data] = activityData.ActivityType;
                    //    break;

                    //case "gv_activitymapid":
                    //    globalVariables[data] = _dynaSet.InsInfo.PamId;
                    //    break;



                    ////case "gv_formversion":
                    ////    globalVariables[data] = _dynaSet.InsInfo.FV;
                    ////    break;

                    //case "gv_instanceid":
                    //    globalVariables[data] = _dynaSet.InsInfo.InsId;
                    //    break;

                    //case "gv_packagename":
                    //    globalVariables[data] = _objectFactory.ProcessInstance.PackageName;
                    //    break;

                    //case "gv_processtype":
                    //    globalVariables[data] = _objectFactory.ProcessInstance.ProcessType;
                    //    break;

                    ////case "gv_destinationactivity":
                    ////    globalVariables[data] = _dynaSet.InsInfo.DActNm;
                    ////    break;

                    //case "gv_tenantid":
                    //    globalVariables[data] = _dynaSet.InsInfo.SlId.TenId;
                    //    break;

                    //case "gv_elapsedtime":
                    //    globalVariables[data] = _dynaSet.InsInfo.ElapsedTime;
                    //    break;

                    case "gv_token":


                        globalVariables[data]
                           = _SlotId;

                        //   globalVariables[data] = GetGlobalVariableSlotToken(_dynaSet.InsInfo.SlId);
                        break;

                }

                dictParams.Add(data, new ServiceElementData { Value = globalVariables[data]==null?null:globalVariables[data].ToString() });
            }

        }

         public void RemoveVariables(ref Dictionary<string, ServiceElementData> dicParams,
             Dictionary<string, object> globalVariables,  Dictionary<string, object> userVariables)
        {
            try
            {
               foreach(var item in globalVariables)
                {
                    dicParams.Remove(item.Key);
                }

                if (userVariables != null)

                foreach (var item in userVariables)
                {
                    dicParams.Remove(item.Key);
                }

            }
            catch
            {
                throw new Exception();
            }
        }



        public  object? GetObjectValue(object? obj,int dataType)
        {
            try
            {
                //8 represents DateTime
                if (dataType == 8)
                {
                    if (obj != null && obj.ToString() != "")
                        return DateTime.Parse(obj.ToString());
                    else
                        return new  DateTime(1753, 1, 1);
                }
                //0 represents Boolean               
                else if (dataType == 0)
                {
                    if (obj != null && obj.ToString() != "")
                        return bool.Parse(obj.ToString());
                    else
                        return null;
                }
                //5 represents Integer               
                else if (dataType == 5)
                {
                    if (obj != null && obj.ToString() != "")
                        return int.Parse(obj.ToString());
                    else
                        return null;
                }
                //6 represents Long               
                else if (dataType == 6)
                {
                    if (obj != null && obj.ToString() != "")
                        return long.Parse(obj.ToString());
                    else
                        return null;
                }
                //7 represents Short
                else if (dataType == 7)
                {
                    if (obj != null && obj.ToString() != "")
                        return short.Parse(obj.ToString());
                    else
                        return null;
                }
                //3 represents Decimal
                else if (dataType==3)
                {
                    if (obj != null && obj.ToString() != "")
                        return Decimal.Parse(obj.ToString());
                    else
                        return null;
                }
                //4 represents Double
                else if (dataType == 4)
                {
                    if (obj != null && obj.ToString() != "")
                        return Double.Parse(obj.ToString());
                    else
                        return null;
                }
                switch (obj)
                {
                    case null:
                        return null;
                    case "":
                        return string.Empty;
                    case JsonElement jsonElement:
                        {
                            var typeOfObject = jsonElement.ValueKind;
                            var rawText = jsonElement.GetRawText(); // Retrieves the raw JSON text for the element.

                            return typeOfObject switch
                            {
                                JsonValueKind.Number => int.Parse(rawText, CultureInfo.InvariantCulture),
                                JsonValueKind.String => obj.ToString(), // Directly gets the string.
                                JsonValueKind.True => true,
                                JsonValueKind.False => false,
                                JsonValueKind.Null => null,
                                JsonValueKind.Undefined => null, // Undefined treated as null.
                                JsonValueKind.Object => rawText, // Returns raw JSON for objects.
                                JsonValueKind.Array => rawText, // Returns raw JSON for arrays.
                                _ => rawText // Fallback to raw text for any other kind.
                            };
                        }
                    default:
                        throw new ArgumentException("Expected a JsonElement object", nameof(obj));
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


    }
}
