namespace CPS.Proof.DFSExtension
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.Serialization;      
    using log4net;    
    using System.Text.Json;
    using System.Globalization;

    [DataContract]
    [Serializable]
    public class ServiceElementData
    {
        private bool _isDfs;

       
        private dynamic _eValue;


        /// <summary>
        /// Represents the <see cref="ILog"/> that holds log instance.
        /// </summary>
        protected readonly ILog _logger = LogManager.GetLogger(typeof(ServiceElementData));

        #region Properties

        /// <summary>
        /// Gets <see cref="string"/> that holds Element identifier.
        /// </summary>  
        [DataMember(Name = "ElementId", EmitDefaultValue = false)]
        public string ElementId { get; set; }

         /// <summary>
        /// Gets <see cref="string"/> that holds Element identifier.
        /// </summary>  
        [DataMember(Name = "ElementName", EmitDefaultValue = false)]
        public string ElementName { get; set; }

        /// <summary>
        /// Represents the property that holds the 
        /// element value.
        /// </summary>
        [DataMember(Name = "Value", IsRequired = false)]
        public dynamic Value
        {
            get
            {
                return _eValue;
            }

            set
            {
               
                    _eValue = value;              


                //if (IsDenyStringExists(_eValue, ECT, RType, out string denyVal))
                //    throw new ProofException(SuspendedReason.IllegalUserInput.ToString() + " - " + denyVal);

            }

        }


        /// <summary>
        /// Gets <see cref="string"/> contains ShowDialog.
        /// </summary>
        [DataMember(Name = "ShowDialog", IsRequired = false)]
        public bool ShowDialog { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> contains HideDialog.
        /// </summary>
        [DataMember(Name = "HideDialog", IsRequired = false)]
        public bool HideDialog { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> contains ShowModal.
        /// </summary>
        [DataMember(Name = "ShowModal", IsRequired = false)]
        public bool ShowModal { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> contains element control type.
        /// </summary>
        [DataMember(Name = "ECT", EmitDefaultValue = false)]
        public string ECT { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> contains element data type.
        /// </summary>
        [DataMember(Name = "EDT", EmitDefaultValue = false)]
        public int EDT { get; set; }

        /// <summary>
        /// Gets <see cref="System.Int32"/> contains sequence .
        /// </summary>
        [DataMember(Name = "SEQ", EmitDefaultValue = false)]
        public int SEQ { get; set; }      


        /// <summary>
        /// Gets <see cref="System.boolean"/> that holds checked status 
        /// </summary>  
        [DataMember(Name = "Chk", IsRequired = false, EmitDefaultValue = false)]
        public bool? Chk { get; set; }

      

        /// <summary>
        /// Gets <see cref="System.boolean"/> that holds visible status 
        /// </summary> 
        [DataMember(Name = "Visible", IsRequired = false)]
        public string? Visible { get; set; }

     
        /// <summary>
        /// Represents the property that holds the 
        /// back color of the element.
        /// </summary>
        [DataMember(Name = "BaCol", IsRequired = false, EmitDefaultValue = false)]
        public string BaCol { get; set; }
              

        /// <summary>
        /// Represents the property that holds the 
        /// Fore color of the element.
        /// </summary>
        [DataMember(Name = "FCol", IsRequired = false, EmitDefaultValue = false)]
        public string FCol { get; set; }

        /// <summary>
        /// Represents the property that holds the 
        ///BorderColor . 
        /// </summary>
        [DataMember(Name = "BrCl", IsRequired = false, EmitDefaultValue = false)]
        public string BrCl { get; set; }

        /// <summary>
        /// Represents the property that holds the 
        /// Css of the element.
        /// </summary>
        [DataMember(Name = "DCss", IsRequired = false, EmitDefaultValue = false)]
        public string DCss { get; set; }

        /// <summary>
        /// Represents the property that holds the 
        /// Css of the element.
        /// </summary>
        [DataMember(Name = "CSS", IsRequired = false, EmitDefaultValue = false)]
        public string CSS { get; set; }



        /// <summary>
        /// Represents the property that holds the 
        /// key and value for the element.
        /// </summary>
        [DataMember(Name = "DVl", IsRequired = false, EmitDefaultValue = false)]
        public Dictionary<string, string> DVl { get; set; }
     

        /// <summary>
        /// Represents the property that holds the 
        /// row id of the element
        /// </summary>
        [DataMember(Name = "RwId", IsRequired = false, EmitDefaultValue = false)]
        public string RwId { get; set; }

        /// <summary>
        /// Represents the property that holds the 
        /// Parent row id of the element
        /// </summary>
        [DataMember(Name = "PRwId", IsRequired = false, EmitDefaultValue = false)]
        public string PRwId { get; set; }

        /// <summary>
        /// Represents the property that is used to get and set the 
        /// is mandatory property of the control.
        /// </summary>
        [DataMember(Name = "Rwstate", IsRequired = false)]
        public short? Rwstate { get; set; }

        /// <summary>
        /// Represents the property that holds the 
        /// parent id of the element
        /// </summary>
        [DataMember(Name = "PEId", IsRequired = false, EmitDefaultValue = false)]
        public string PEId { get; set; }

      
        /// <summary>
        /// Represents the tree structure
        /// </summary>
        [DataMember(Name = "Child", IsRequired = false, EmitDefaultValue = false)]
        public List<ServiceElementData> Child { get; set; }
              


        /// <summary>
        /// Gets <see cref="string"/> that holds url
        /// </summary>  
        [DataMember(Name = "PURL", IsRequired = false, EmitDefaultValue = false)]
        public string PURL { get; set; }

     

        /// <summary>
        /// Gets <see cref="string"/> that holds value that determines
        /// whether element is enabled or not.
        /// </summary>
        [DataMember(Name = "Enbl", IsRequired = false)]
        public string Enbl { get; set; }

        /// <summary>
        /// Represents the property that is used to get and set the 
        /// is mandatory property of the control.
        /// </summary>
        [DataMember(Name = "Man", IsRequired = false)]
        public bool Man { get; set; }
       

        /// <summary>
        /// Represents the property that is used to get and set the 
        /// is mandatory property of the control.
        /// </summary>
        [DataMember(Name = "RedirectType", IsRequired = false)]
        public short? RedirectType { get; set; }

        /// <summary>
        /// Represents the property that is used to get and set RelodableElements .
        /// </summary>
        [DataMember(Name = "rElemData", IsRequired = false, EmitDefaultValue = false)]
        public List<Dictionary<string, object>> rElemData { get; set; }

        /// <summary>
        /// Represents the property that is used to get and set ControlId .
        /// </summary>
        [DataMember(Name = "ControlId", IsRequired = false, EmitDefaultValue = false)]
        public string ControlId { get; set; }

      /// <summary>
        /// Represents the property that is used to get and set PageCount .
        /// </summary>
        [DataMember(Name = "PageCount", IsRequired = false, EmitDefaultValue = false)]
        
        public int? PageCount { get; set; }

        /// <summary>
        /// Represents the property that is used to get and set RecordsFrom .
        /// </summary>
        [DataMember(Name = "RecordsFrom", IsRequired = false, EmitDefaultValue = false)]
        public int? RecordsFrom { get; set; }

        /// <summary>
        /// Represents the property that is used to get and set RecordsTo .
        /// </summary>
        [DataMember(Name = "RecordsTo", IsRequired = false, EmitDefaultValue = false)]
        public int? RecordsTo { get; set; }

        /// <summary>
        /// Represents the property that is used to get and set TotalRecords .
        /// </summary>
        [DataMember(Name = "TotalRecords", IsRequired = false, EmitDefaultValue = false)]
        public long? TotalRecords { get; set; }

        /// <summary>
        /// Represents the property that is use to get the combobox value.
        /// </summary>
        public string ComboSelectedValue
        {
            get
            {
                if (ECT == "ComboBox" &&
                    DVl != null && DVl.Keys.Count > 0)
                    return DVl.Keys.FirstOrDefault();

                return null;
            }
        }

      

        /// <summary>
        /// Represents the property that is used to get and set RenderType.
        /// </summary>
        [DataMember(Name = "RType", IsRequired = false, EmitDefaultValue = false)]
        public byte RType { get; set; }            


        #endregion      

        #region Private Methods

        [OnSerializing()]
        private void OnSerializing(StreamingContext c)
        {          
             //if(_eValue is not null)
             //   _eValue= GetObjectValue(_eValue, EDT);
           
        }         
              
        private object? GetObjectValue(object? obj, int dataType)
        {
        try
        {
         //8 represents DateTime
         if (dataType == 8)
         {
             if (obj != null && obj.ToString() != "")
                 return DateTime.Parse(obj.ToString());
             else
                 return new DateTime();
         }

        //9 represents string
        else if (dataType == 9)
        {
            if (obj != null && obj.ToString() != "")
                return obj.ToString();
            else
                return null;
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
         else if (dataType == 3)
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
        #endregion
    }

}
