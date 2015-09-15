using System.Web;

namespace Fenton.Capttia
{
    public class BrowserId
    {
        private string Browser { get; set; }

        private string Accepts { get; set; }

        private string Screen { get; set; }

        public BrowserId(HttpContext context)
        {
            Browser = context.Request.Browser.Browser;
            Accepts = string.Join("#", context.Request.AcceptTypes);
            Screen = string.Format("#{0}{1}{2}",
                context.Request.Browser.ScreenBitDepth,
                context.Request.Browser.ScreenPixelsWidth,
                context.Request.Browser.ScreenCharactersWidth);
        }

        public BrowserId(HttpContextBase context)
        {
            Browser = context.Request.Browser.Browser;
            Accepts = string.Join("#", context.Request.AcceptTypes);
            Screen = string.Format("#{0}{1}{2}",
                context.Request.Browser.ScreenBitDepth,
                context.Request.Browser.ScreenPixelsWidth,
                context.Request.Browser.ScreenCharactersWidth);
        }

        public string GetId()
        {
            return Browser + Accepts + Screen;
        }
    }
}
