using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE1
{
    public interface IRule
    {
        /// <summary>
            /// Gets the signal.
            /// </summary>
            /// <value>
            /// The signal.
            /// </value>
            string Signal { get; }

            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            dynamic Value { get; }

            /// <summary>
            /// Gets the type of the comparison.
            /// </summary>
            /// <value>
            /// The type of the comparison.
            /// </value>
            string ComparisonType { get; }

            /// <summary>
            /// Returns true if the input signal doesn't match with this rule.
            /// </summary>
            /// <param name="input">The input signal.</param>
            /// <returns>
            ///   <c>true</c> if the input signal doesn't match with this rule; otherwise, <c>false</c>.
            /// </returns>
            bool IsValid(SignalData input);
        }
}
