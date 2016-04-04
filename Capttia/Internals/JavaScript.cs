using System.Text;

namespace Fenton.Capttia
{
    internal static class JavaScript
    {
        internal static string EncodeForSingleQuotes(string s)
        {
            var safeString = new StringBuilder();

            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        safeString.Append("\\\'");
                        break;
                    case '\\':
                        safeString.Append("\\\\");
                        break;
                    case '\b':
                        safeString.Append("\\b");
                        break;
                    case '\f':
                        safeString.Append("\\f");
                        break;
                    case '\n':
                        safeString.Append("\\n");
                        break;
                    case '\r':
                        safeString.Append("\\r");
                        break;
                    case '\t':
                        safeString.Append("\\t");
                        break;
                    default:
                        int i = c;
                        if (i < 32 || i > 127)
                        {
                            safeString.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            safeString.Append(c);
                        }
                        break;
                }
            }

            return safeString.ToString();
        }
    }
}
