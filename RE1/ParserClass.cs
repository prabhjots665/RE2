using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE1
{
    public class ParserClass
    {
        private string _inputData;
        
        #region Constants

        private const string fieldSignalName = "signal";
        private const string fieldValueName = "value";
        private const string fieldValueTypeName = "value_type";

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses the specified JSON input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="invalidSignals">The invalid signals.</param>
        /// <param name="rules">The rules.</param>
        /// <returns>
        /// Returns a list of invalid signals according to the rules.
        /// </returns>
        /// <exception cref="ArgumentNullException">input</exception>
        public List<SignalData> TryParse(string input, params IRule[] rules)
        {
            var invalidSignals = new List<SignalData>();
            try
            {
                if (string.IsNullOrEmpty(input)) { throw new ArgumentNullException("input"); }

                

                foreach (var signal in input.Split(Constants.ObjectEnd))
                {
                    var elements = signal.Split(new[] { Constants.ElementSeparator }, StringSplitOptions.RemoveEmptyEntries);
                    if (elements.Length == 3) // Signal, Value and ValueType
                    {
                        string parsedSignal = string.Empty, parsedValue = string.Empty, parsedValueType = string.Empty;

                        foreach (var element in elements)
                        {
                            if (!string.IsNullOrWhiteSpace(element))
                            {
                                int indexOfObjectSeparator = element.IndexOf(Constants.ObjectSeparator);
                                string fieldName = element.Substring(0, indexOfObjectSeparator);
                                string fieldValue = element.Substring(indexOfObjectSeparator + 1);

                                if (!string.IsNullOrWhiteSpace(fieldName) && !string.IsNullOrWhiteSpace(fieldValue))
                                {
                                    fieldName = fieldName.ToLowerInvariant().Split(Constants.ObjectWrapper)[1];
                                    fieldValue = fieldValue.Split(Constants.ObjectWrapper)[1];

                                    switch (fieldName.ToLowerInvariant())
                                    {
                                        case fieldSignalName:
                                            parsedSignal = fieldValue;
                                            continue;

                                        case fieldValueTypeName:
                                            if (fieldValue == "Integer") parsedValueType = "Decimal";
                                            else if (fieldValue == "String") parsedValueType = "String";
                                            else  parsedValueType = "DateTime";
                                            continue;

                                        case fieldValueName:
                                            parsedValue = fieldValue;
                                            continue;

                                        default:
                                            break;
                                    }
                                }

                            }
                        }


                        var typeInfo = Type.GetType($"System.{parsedValueType}");
                        dynamic value = null;
                        if (typeInfo != null)
                        {
                            value = Convert.ChangeType(parsedValue, typeInfo);
                        }
                        else
                        {
                            value = parsedValue;
                        }

                        var signalData = new SignalData(parsedSignal, value, parsedValueType);
                        foreach (var rule in rules)
                        {
                            if (!rule.IsValid(signalData))
                            {
                                invalidSignals.Add(signalData); // Add to invalid signal list
                                break;
                            }
                        }
                        
                    }
                }
                return invalidSignals;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        /// <summary>
        /// Reads the specified JSON input and CSV rule files to generate list of invalid signals.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="invalidSignals">The invalid signals.</param>
        /// <param name="rules">The rules.</param>
        /// <returns>
        /// Returns a list of invalid signals according to the rules.
        /// </returns>
        public List<SignalData> parser(string dataFile,string ruleFile)
        {
            _inputData = File.ReadAllText(dataFile);
            var rules = Rule.GetRules(ruleFile);
            return TryParse(_inputData, rules);
            
        }
    }
}
