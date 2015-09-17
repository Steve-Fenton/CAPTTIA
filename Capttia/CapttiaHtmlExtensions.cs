using Fenton.Capttia;
using System.Configuration;
using System.Text;
using Yahoo.Yui.Compressor;

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

            var ids = new ScriptIds(config.ModuleName);

            return MvcHtmlString.Create(GetHoneyPot(ids) + GetScriptElement(ids, token));
        }

        public static string GetHoneyPot(ScriptIds ids) {
            return @"<div id=""" + ids.HoneyPotContainerId + @"""><input type=""text"" name=""" + ids.HoneyPotInputId + @""" value=""""></div>";
        }

        public static string GetScriptElement(ScriptIds ids, string token)
        {
            return @"<script id=""" + ids.ScriptId + @""">" + GetScript(ids, token) + "</script>";
        }

        public static string GetScript(ScriptIds ids, string token)
        {
            var script = @"(function (v, t) {
                if (!t) t = 'hidden';
                var chk = function () {
                    var elem = document.getElementById('" + ids.ScriptId + @"');
                    document.getElementById('" + ids.HoneyPotContainerId + @"').style.display = 'none';
                    if (elem) {
                        var newElem = document.createElement('div');
                        newElem.innerHTML = '<input type=""' + t + '"" name=""" + ids.TokenId + @""" value=""' + v + '"" />';
                        elem.parentNode.appendChild(newElem);
                        elem.parentNode.removeChild(elem);
                    } else {
                        window.setTimeout(chk, 1000);
                    }
                };
                window.setTimeout(chk, 1000);
                document.getElementById('" + ids.HoneyPotContainerId + @"').style.display = 'none';
                }('" + token + @"'));";

            var compressor = new JavaScriptCompressor
            {
                Encoding = Encoding.UTF8,
                DisableOptimizations = false,
                ObfuscateJavascript = true,
                PreserveAllSemicolons = true,
                IgnoreEval = true,
                ThreadCulture = Globalization.CultureInfo.InvariantCulture
            };

            var example = compressor.Compress(script);

            return example;
        }
    }
}
