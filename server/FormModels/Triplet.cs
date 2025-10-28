namespace CPS.Proof.DFSExtension
{
using System;

    /// <summary>
    /// Represents a generic placeholder class that can hold three
    /// data elements.
    /// </summary>
    /// <typeparam name="I">
    /// A generic type that holds the first value.
    /// </typeparam>
    /// <typeparam name="J">
    /// A generic type that holds the second value.
    /// </typeparam>
    /// <typeparam name="K">
    /// A generic type that holds the third value.
    /// </typeparam>
    [Serializable]
    public class Triplet<I, J, K>
    {
        /// <summary>
        /// The first value I.
        /// </summary>
        private I _firstValue   = default(I);

        /// <summary>
        /// The second value J.
        /// </summary>
        private J _secondValue  = default(J);

        /// <summary>
        /// The third value K.
        /// </summary>
        private K _thirdValue   = default(K);

        /// <summary>
        /// Overloaded constructor that takes three values.
        /// </summary>
        /// <param name="first">
        /// First value.
        /// </param>
        /// <param name="second">
        /// Second value.
        /// </param>
        /// <param name="third">
        /// Third value.
        /// </param>
        public Triplet(I first, J second, K third)
        {
            _firstValue     = first;
            _secondValue    = second;
            _thirdValue     = third;
        }

        /// <summary>
        /// Gets the first value.
        /// </summary>
        public I FirstValue
        {
            get
            {
                return _firstValue;
            }
        }

        /// <summary>
        /// Gets the second value.
        /// </summary>
        public J SecondValue
        {
            get
            {
                return _secondValue;
            }
        }

        /// <summary>
        /// Gets the third value.
        /// </summary>
        public K ThirdValue
        {
            get
            {
                return _thirdValue;
            }
        }
    }
}