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

            var token = JavaScript.EncodeForSingleQuotes(Encryption.Encrypt(request.AnonymousID, config.PassPhrase));

            return MvcHtmlString.Create("<script id=\"" + config.ModuleName + "-script\">" +
                "(function (v, t) {" +
                "if (!t) t = 'hidden';" +
                "window.setTimeout(function() {" +
                "    var newElem = document.createElement('div');" +
                "    newElem.innerHTML = '<input type=\"' + t + '\" name=\"" + config.ModuleName + "\" value=\"' + v + '\" />';" +
                "    var elem = document.getElementById('" + config.ModuleName + "-script');" +
                "    elem.parentNode.appendChild(newElem);" +
                "    elem.parentNode.removeChild(elem);" +
                "}, 1000);" +
                "}('" + token + "'));" +
                "</script>"); }
    }
}
