using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE1
{
    public class SignalData
    {
        #region Constants

        private const string _valueTypeInteger = "integer";
        private const string _valueTypeDatetime = "datetime";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalData"/> class.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="value">The value.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <exception cref="System.ArgumentNullException">
        /// signal
        /// or
        /// value
        /// or
        /// valueType
        /// </exception>
        public SignalData(string signal, dynamic value, string valueType)
        {
            Signal = signal;
            ValueType = valueType;
            Value = value;

        }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// Gets the signal.
        /// </summary>
        /// <value>
        /// The signal.
        /// </value>
        public string Signal { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public dynamic Value { get; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public string ValueType { get; set; }

        #endregion

   }
}
