
using System.Collections.Generic;

namespace CPS.Proof.DFSExtension
{
    /// <summary>
    /// Represents the class that implements Context Model
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Gets <see cref="string"/> that holds SlotId identifier
        /// </summary>
        public string? SlotId { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> that holds ControlId identifier.
        /// </summary>
        public string? ControlId { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> that holds FormVersionId identifier.
        /// </summary>
        public string? FormVersionId { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> that holds Action identifier.
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> that holds ControlId identifier.
        /// </summary>
        public string? WidgetId { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> that holds PackageProcessMapId identifier.
        /// </summary>
        public string? PackageProcessMapId { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> that holds ProcessActivityMapId identifier.
        /// </summary>
        public string? ProcessActivityMapId { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> that holds ViewPort identifier.
        /// </summary>
        public short? ViewPort { get; set; }

        /// <summary>
        /// Gets <see cref="string"/> that holds FormInstanceId identifier.
        /// </summary>
        public string? FormInstanceId { get; set; }     

        /// <summary>
        /// Gets <see cref="ICollection{T}"/> that holds Dictionary of ControlId and ControlValue
        /// </summary>
        public List<ServiceElementData> Params { get; set; }

        /// <summary>
        /// Gets <see cref="ICollection{T}"/> that holds Dictionary of ControlId and ControlValue
        /// </summary>
        public List<ServiceElementData> FormData { get; set; }

        public short? PageDirection { get; set; }

        public int? PageSize { get; set; }

        public int? PageCount { get; set; }

        public int? RecordsFrom { get; set; }

        public int? RecordsTo { get; set; }

        public long? CurrentRowIndex { get; set; }

        public long? TotalRecords { get; set; }

        public string SearchFilter { get; set; }

    }
}
