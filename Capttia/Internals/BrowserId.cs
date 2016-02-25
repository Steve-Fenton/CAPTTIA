using System.Linq;
using System.Web;

namespace Fenton.Capttia
{
    public class BrowserId
    {
        private string Browser { get; set; }

        private string UserHostAddress { get; set; }

        private string Screen { get; set; }

        public BrowserId(HttpContext context)
        {
            Browser = context.Request.Browser.Browser;
            UserHostAddress = string.Join("#", GetIP(context));
            Screen = string.Format("#{0}{1}{2}",
                context.Request.Browser.ScreenBitDepth,
                context.Request.Browser.ScreenPixelsWidth,
                context.Request.Browser.ScreenCharactersWidth);
        }

        public BrowserId(HttpContextBase context)
        {
            Browser = context.Request.Browser.Browser;
            UserHostAddress = string.Join("#", GetIP(context));
            Screen = string.Format("#{0}{1}{2}",
                context.Request.Browser.ScreenBitDepth,
                context.Request.Browser.ScreenPixelsWidth,
                context.Request.Browser.ScreenCharactersWidth);
        }

        public string GetId()
        {
            return Browser + UserHostAddress + Screen;
        }

        private string GetIP(HttpContext context)
        {
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            else {
                // Using X-Forwarded-For last address
                ipAddress = ipAddress.Split(',').First().Trim();
            }

            return ipAddress;
        }

        private string GetIP(HttpContextBase context)
        {
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            else {
                // Using X-Forwarded-For last address
                ipAddress = ipAddress.Split(',').First().Trim();
            }

            return ipAddress;
        }
    }
}
