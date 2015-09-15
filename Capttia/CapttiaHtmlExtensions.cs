using Fenton.Capttia;
using System.Configuration;

namespace System.Web.Mvc.Html
{
    public static class CapttiaHtmlExtensions
    {
        /// <summary>
        /// Writes a CAPTTIA element to the form.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static MvcHtmlString Capttia(this HtmlHelper html, HttpRequestBase request)
        {
            var config = ConfigurationManager.GetSection("capttia") as CapttiaSection;
            var encyption = new Encryption();

            var contextId = AnonymousIdentifier.GetContextId(request.RequestContext.HttpContext);

            // Place it in a cookie
            var cookieContextId = encyption.Encrypt(contextId, config.PassPhraseB);
            request.RequestContext.HttpContext.Response.SetCookie(new HttpCookie(config.CookieName, cookieContextId));

            // Place it on the form
            var formId = encyption.Encrypt(contextId, config.PassPhrase);
            var token = JavaScript.EncodeForSingleQuotes(formId);

            return MvcHtmlString.Create("<div id=\"" + config.ModuleName + "Input\"><input type=\"text\" name=\"" + config.ModuleName + "Value\" value=\"\"></div>" +
                "<script id=\"" + config.ModuleName + "-script\">" +
                "(function (v, t) {" +
                "if (!t) t = 'hidden';" +
                "window.setTimeout(function() {" +
                "    var newElem = document.createElement('div');" +
                "    newElem.innerHTML = '<input type=\"' + t + '\" name=\"" + config.ModuleName + "\" value=\"' + v + '\" />';" +
                "    var elem = document.getElementById('" + config.ModuleName + "-script');" +
                "    elem.parentNode.appendChild(newElem);" +
                "    elem.parentNode.removeChild(elem);" +
                "}, 1000);" +
                "document.getElementById('" + config.ModuleName + "Input').style.display = 'none';" +
                "}('" + token + "'));" +
                "</script>"); }
    }
}
