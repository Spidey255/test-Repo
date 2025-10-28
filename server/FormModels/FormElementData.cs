using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using log4net;
using SRA.Proof.Middleware;

namespace CPS.Proof.DFSExtension
{

[Serializable]
[DataContract]
public class FormElementData
{
    private bool _isDfs;

    private string _eValue;

    protected readonly ILog _logger = LogManager.GetLogger(typeof(FormElementData));

    [DataMember(Name = "ElementId", EmitDefaultValue = false)]
    public string ElementId { get; set; }

    [DataMember(Name = "ElementName", EmitDefaultValue = false)]
    public string ElementName { get; set; }

    [DataMember(Name = "Value", IsRequired = false)]
    public string Value
    {
        get
        {
            return _eValue;
        }
        set
        {
            _eValue = value;
        }
    }

    [DataMember(Name = "ShowDialog", IsRequired = false)]
    public bool ShowDialog { get; set; }

    [DataMember(Name = "HideDialog", IsRequired = false)]
    public bool HideDialog { get; set; }

    [DataMember(Name = "ShowModal", IsRequired = false)]
    public bool ShowModal { get; set; }

    [DataMember(Name = "ECT", EmitDefaultValue = false)]
    public string ECT { get; set; }

    [DataMember(Name = "SEQ", EmitDefaultValue = false)]
    public int SEQ { get; set; }

    [DataMember(Name = "Chk", IsRequired = false, EmitDefaultValue = false)]
    public bool? Chk { get; set; }

    [DataMember(Name = "Visible", IsRequired = false)]
    public string? Visible { get; set; }

    [DataMember(Name = "BaCol", IsRequired = false, EmitDefaultValue = false)]
    public string BaCol { get; set; }

    [DataMember(Name = "FCol", IsRequired = false, EmitDefaultValue = false)]
    public string FCol { get; set; }

    [DataMember(Name = "BrCl", IsRequired = false, EmitDefaultValue = false)]
    public string BrCl { get; set; }

    [DataMember(Name = "DCss", IsRequired = false, EmitDefaultValue = false)]
    public string DCss { get; set; }

    [DataMember(Name = "DVl", IsRequired = false, EmitDefaultValue = false)]
    public Dictionary<string, string> DVl { get; set; }

    [DataMember(Name = "RwId", IsRequired = false, EmitDefaultValue = false)]
    public string RwId { get; set; }

    [DataMember(Name = "Rwstate", IsRequired = false)]
    public short? Rwstate { get; set; }

    [DataMember(Name = "PEId", IsRequired = false, EmitDefaultValue = false)]
    public string PEId { get; set; }

    [DataMember(Name = "Child", IsRequired = false, EmitDefaultValue = false)]
    public List<FormElementData> Child { get; set; }

    [DataMember(Name = "PURL", IsRequired = false, EmitDefaultValue = false)]
    public string PURL { get; set; }

    [DataMember(Name = "Enbl", IsRequired = false)]
    public string Enbl { get; set; }

    [DataMember(Name = "Man", IsRequired = false)]
    public bool Man { get; set; }

    [DataMember(Name = "RedirectType", IsRequired = false)]
    public short? RedirectType { get; set; }

    public string ComboSelectedValue
    {
        get
        {
            if (ECT == "ComboBox" && DVl != null && DVl.Keys.Count > 0)
            {
                return DVl.Keys.FirstOrDefault();
            }

            return null;
        }
    }

    [DataMember(Name = "RType", IsRequired = false, EmitDefaultValue = false)]
    public byte RType { get; set; }

    [OnSerializing]
    private void OnSerializing(StreamingContext c)
    {
        SetDefaultNullValue();
    }

    private void SetDefaultNullValue()
    {
    }

    private void SetNullValue()
    {
    }

   
}
}