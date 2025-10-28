namespace CPS.Proof.DFSExtension
{
    /// <summary>
    /// Used to hold the Repository Column details and convert as json.
    /// </summary>
    public class RepositoryColumn
    {
        /// <summary>
        /// Gets or Sets a <see cref="string"/>that contains 
        /// the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets a <see cref="DataTypes"/>that contains 
        /// the DataType
        /// </summary>
        public DataTypes DataType { get; set; }

        /// <summary>
        /// Gets or Sets a<see cref="System.Object"/>that contains 
        /// the Value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or Sets a<see cref="System.Boolean"/>that contains 
        /// the IsKey
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// Gets or Sets a<see cref="System.Boolean"/>that contains 
        /// the IsIdentity
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// Gets or Sets a<see cref="System.Boolean"/>that contains 
        /// the encryption required or not.
        /// </summary>
        public bool IEnc { get; set; }


    }
}