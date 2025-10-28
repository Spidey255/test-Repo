using System;
namespace CPS.Proof.DFSExtension
{
 public enum ElementAttributeType
    {
         /// <summary>
        /// Represents the default automatic attribute type, which
        /// performs no action on the data elements.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents the attribute, to set the fore 
        /// color for the data element.
        /// </summary>
        ForeColor = 1,

        /// <summary>
        /// Represents the attribute, to set the back 
        /// color for the data element.
        /// </summary>
        BackColor = 2,

        /// <summary>
        /// Represents the attribute, to set the border 
        /// color for the data element.
        /// </summary>
        BorderColor = 3,

        /// <summary>
        /// Represents the attribute, to set the visibility 
        /// for the data element.
        /// </summary>
        Visible = 4,

        /// <summary>
        /// Represents the attribute, to enable the data element.
        /// </summary>
        Enable = 5,

        /// <summary>
        /// Represents the attribute, that specifies the value expression.
        /// </summary>
        Value = 6,

        /// <summary>
        /// Represents an enum item for SelectedRowId
        /// </summary>
        SelectedRowId = 7,

        /// <summary>
        /// Represents an enum item for Display format.
        /// </summary>
        DisplayFormat = 8,

        /// <summary>
        /// Represents an enum item for Mandatory
        /// </summary>
        Mandatory = 9,

        /// <summary>
        /// Represents an enum item for show dialog
        /// </summary>
        ShowDialog = 10,

        /// <summary>
        /// Represents an enum item for hide dialog
        /// </summary>
        HideDialog = 11,
		
        /// <summary>
        /// Represents an enum item for CSS
        /// </summary>
        CSS = 12,

        /// <summary>
        /// Represents an enum item for show modal
        /// </summary>
        ShowModal=13,


        /// <summary>
        /// Represents an enum item for Pattern
        /// </summary>
        Pattern = 14,


        /// <summary>
        /// Represents an enum item for Pattern
        /// </summary>
        DataKey = 15,
    }

    public enum ViewportTypes : short
    {
        /// <summary>
        /// Enum that represents the FormVersionStyles Mobile object
        /// </summary>
        Mobile = 1,

        /// <summary>
        /// Enum that represents the FormVersionStyles Tab object
        /// </summary>
        Tab = 2,

        /// <summary>
        /// Enum that represents the FormVersionStyles Medium object
        /// </summary>
        Medium = 3,

        /// <summary>
        /// Enum that represents the FormVersionStyles Large object
        /// </summary>
        Large = 4
    }
     /// <summary>
    /// Represents the enum that holds the different
    /// types of a process.
    /// </summary>
    [Serializable]
    public enum ProcessType : byte
    {
        /// <summary>
        /// Represents the none processes.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents the rule based processes.
        /// </summary>
        RuleBased = 1,

        /// <summary>
        /// Represents the case based processes.
        /// </summary>
        CaseBased = 2,

        /// <summary>
        /// Represents the RSpace processes.
        /// </summary>
        RSpace = 4,

        /// <summary>
        /// Represents the atom UI processes.
        /// </summary>
        AtomUI = 5,

        /// <summary>
        /// Represents the MetaModel processes.
        /// </summary>
        MetaModel = 6,

        /// <summary>
        /// Represents the atom funcional processes.
        /// </summary>
        AtomFunctional = 7,

        /// <summary>
        /// Represents the MasterDetail.
        /// </summary>
        MasterDetail = 8,

        /// <summary>
        /// Represents the API.
        /// </summary>
        API = 9,

        /// <summary>
        /// Represents the ExeorService.
        /// </summary>
        ExeorService = 10,

        /// <summary>
        /// Represents the ReportsorDashboard.
        /// </summary>
        ReportsorDashboard = 11,

        /// <summary>
        /// Represents the Widget.
        /// </summary>
        Widget = 12,

        /// <summary>
        /// Represents the Composite.
        /// </summary>
        Composite = 13

    }

    /// <summary>
    /// Represents an enum for Data Type
    /// </summary>
    public enum DataTypes
    {
        Boolean = 0,
        Byte = 1,
        Character = 13,
        Decimal = 3,
        Double = 4,
        Integer = 5,
        Long = 6,
        Short = 7,
        DateTime = 8,
        String = 9,
        Others = 10,
        ComplexOnetoOne = 11,
        ComplexOnetoMany = 12,
        StringArray = 2,


    }
}