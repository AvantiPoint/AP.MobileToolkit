using System.Collections.Generic;
using System.Text;

namespace AP.CrossPlatform.Extensions
{
    public static class StringExtensions
    {
        private static readonly Dictionary<char, string> AsciiConversions = new Dictionary<char, string>
        {
            { '’', "'" },
            { '‘', "'" },
            { '“', "\"" },
            { '”', "\"" },
            { '"', "\"" },
            { '®', "(R)" },
            { '©', "(C)" },
            { '«', "<<" },
            { '»', ">>" },
            { '±', "+/-" },
            { '¢', "c" },
            { '£', "L" },
            { '…', "..." },
            { '•', "*" },
            { '⁄', "/" },
            { '™', "TM" },
            { '←', "<--" },
            { '→', "-->" },
            { '↔', "<-->" },
            { '⇐', "<==" },
            { '⇒', "==>" },
            { '⇔', "<==>" },
            { '−', "-" },
            { '∗', "*" },
            { '∼', "~" },
            { '≅', "~=" },
            { '≈', "~" },
            { '≠', "!=" },
            { '≤', "<=" },
            { '≥', ">=" },
            { '⟨', "<" },
            { '⟩', ">" },
            { '◊', "<>" },
            { '⊕', "(+)" },
            { '⊗', "(x)" },
            { '¼', "1/4" },
            { '½', "1/2" },
            { '¾', "3/4" }
        };

        public static string ToASCII(this string str)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if ((int)c >= 128)
                {
                    var ascii = AsciiConversions.ContainsKey(c) ? AsciiConversions[c] : "?";
                    sb.Append(ascii);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static bool HasNonASCIIChars(this string str)
        {
            foreach (var c in str)
            {
                bool isAscii = c < 128;
                if (!isAscii)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
