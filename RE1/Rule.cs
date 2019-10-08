using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE1
{
    public class Rule : IRule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule" /> class.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <exception cref="System.ArgumentNullException">signalName
        /// or
        /// value</exception>
        public Rule(string signal, dynamic value, string comparisonType)
        {
            if (string.IsNullOrWhiteSpace(signal)) { throw new ArgumentNullException("signal"); }
            if (value == null) { throw new ArgumentNullException("value"); }

            Signal = signal;
            Value = value;
            ComparisonType = comparisonType;
            ValueType = value.GetType().ToString().Split('.')[1];
        }

        #endregion

        #region Fields & Properties

        private static List<Rule> _rules;

        /// <summary>
        /// Gets the signal.
        /// </summary>
        /// <value>
        /// The name of the signal.
        /// </value>
        public string Signal { get; private set; }

        /// <summary>
        /// Gets the type of the comparison.
        /// </summary>
        /// <value>
        /// The type of the comparison.
        /// </value>
        public string ComparisonType { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public dynamic Value { get; private set; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public string ValueType { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns true if the input signal doesn't match with this rule.
        /// </summary>
        /// <param name="input">The input signal.</param>
        /// <returns>
        ///   <c>true</c> if the input signal doesn't match with this rule; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(SignalData input)
        {
            bool result = true;

            if (input != null)
            {
                if (Signal == input.Signal && ValueType.ToLowerInvariant() == input.ValueType.ToLowerInvariant())// && Value.GetType() == input.Value.GetType())
                {

                    switch (ComparisonType)
                    {
                        case "Equal":
                            result = ( input.Value == Value);
                            break;

                        case "NotEqual":
                            result = (input.Value != Value);
                            break;

                        case "GreaterThan":
                            result = ( input.Value > Value);
                            break;

                        case "GreaterThanOrEqual":
                            result = ( input.Value >= Value);
                            break;

                        case "LessThan":
                            result = ( input.Value < Value);
                            break;

                        case "LessThanOrEqual":
                            result = (input.Value <= Value);
                            break;

                        default:
                            return result;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the built-in rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        public static Rule[] GetRules(string ruleFile)
        {
            if (_rules == null)
            {
                LoadRulesFromCsv(ruleFile);
            }

            return _rules.ToArray();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the rules from CSV.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private static void LoadRulesFromCsv(string filename)
        {
            try
            {
                if (_rules == null) _rules = new List<Rule>();
                if (File.Exists(filename))
                {
                    var rules = File.ReadAllLines(filename);
                    if (rules != null)
                    {
                        bool isHeader = true;
                        foreach (var rule in rules)
                        {
                            if (isHeader) { isHeader = false; continue; } // Skip header
                            var data = rule.Split(Constants.ElementSeparator);

                            var signal = data[0].Split(Constants.ObjectWrapper)[1];
                            var comparisonType = data[1].Split(Constants.ObjectWrapper)[1];
                            var valueType = data[3].Split(Constants.ObjectWrapper)[1];

                            if (valueType == "Int32") valueType = "Decimal";

                            var typeInfo = Type.GetType($"System.{valueType}");
                            dynamic value = null;
                            if (typeInfo != null)
                            {
                                value = Convert.ChangeType(data[2].Split(Constants.ObjectWrapper)[1], typeInfo);
                            }
                            else
                            {
                                value = data[2].Split('"')[1];
                            }
                            _rules.Add(new Rule(signal, value, comparisonType));
                        }
                    }
                }
            }
            catch (Exception)
            {
                //TODO: Log Error
                throw;
            }
        }

        #endregion
    }
}
