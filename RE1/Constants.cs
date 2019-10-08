using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE1
{
    /// <summary>
    /// Defines constants used by the ParserClass, rules and within the Rule Engine.
    /// </summary>
    public static class Constants
    {
        #region Constants

        internal const char ArrayStart = '[';
        internal const char ArrayEnd = ']';

        internal const char ElementSeparator = ',';

        internal const char OjectStart = '{';
        internal const char ObjectEnd = '}';
        internal const char ObjectSeparator = ':';
        internal const char ObjectWrapper = '"';

        #endregion
    }
}
