using System;
using System.Web;

namespace Fenton.Capttia
{
    public static class AnonymousIdentifier
    {
        private const string SEPARATOR = "ಠ⌣ಠ";

        public static string GetContextId(HttpContextBase context)
        {
            var browserId = new BrowserId(context);
            return $"C{DateTime.UtcNow.Ticks}{SEPARATOR}{browserId.GetId()}";
        }

        public static string GetBrowserStampFromId(string id)
        {
            if (id.Contains(SEPARATOR))
            {
                id = id.Substring(id.IndexOf(SEPARATOR) + SEPARATOR.Length);
            }

            return id;
        }
    }
}
