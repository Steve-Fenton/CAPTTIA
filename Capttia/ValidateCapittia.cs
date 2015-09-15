using System;
using System.Configuration;
using System.Web.Mvc;

namespace Fenton.Capttia
{
    /// <summary>
    /// Validates that the token returned on the form is valid and invalidates the ModelState if not.
    /// </summary>
    public class ValidateCapttiaAttribute : ActionFilterAttribute
    {
        private readonly Encryption _encryption;

        public ValidateCapttiaAttribute()
        {
            _encryption = new Encryption();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var config = ConfigurationManager.GetSection("capttia") as CapttiaSection;

            if (!IsValidRequest(config, filterContext))
            {
                filterContext.Controller.ViewData.ModelState.AddModelError(config.ModuleName, config.ErrorMessage);
            }

            base.OnActionExecuting(filterContext);
        }

        private bool IsValidRequest(CapttiaSection config, ActionExecutingContext filterContext)
        {
            try
            {
                var honeyPot = filterContext.HttpContext.Request.Params[config.ModuleName + "Value"];
                if (!string.IsNullOrWhiteSpace(honeyPot))
                {
                    return false;
                }

                return IsValidToken(config, filterContext);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsValidToken(CapttiaSection config, ActionExecutingContext filterContext)
        {
            string decryptedCookieId = GetCookieId(config, filterContext);

            string decryptedFormId = GetFormId(config, filterContext);

            string actualBrowserId = GetBrowserId(filterContext);

            if (!string.IsNullOrWhiteSpace(decryptedCookieId) && !string.IsNullOrWhiteSpace(decryptedFormId))
            {
                var cookieBrowserId = AnonymousIdentifier.GetBrowserStampFromId(decryptedCookieId);
                var formBrowserId = AnonymousIdentifier.GetBrowserStampFromId(decryptedFormId);

                bool cookieMatchesForm = decryptedCookieId == decryptedFormId;
                bool browserMatches = (actualBrowserId == cookieBrowserId);

                return (cookieMatchesForm && browserMatches);
            }

            return false;
        }

        private string GetCookieId(CapttiaSection config, ActionExecutingContext filterContext)
        {
            var cookieId = filterContext.HttpContext.Request.Cookies[config.CookieName].Value;
            if (string.IsNullOrWhiteSpace(cookieId))
            {
                return string.Empty;
            }
            var decryptedCookieId = _encryption.Decrypt(cookieId, config.PassPhraseB);
            return decryptedCookieId;
        }

        private string GetFormId(CapttiaSection config, ActionExecutingContext filterContext)
        {
            var formId = filterContext.HttpContext.Request.Params[config.ModuleName];
            if (string.IsNullOrWhiteSpace(formId))
            {
                return string.Empty;
            }
            var decryptedFormId = _encryption.Decrypt(formId, config.PassPhrase);
            return decryptedFormId;
        }

        private string GetBrowserId(ActionExecutingContext filterContext)
        {
            return new BrowserId(filterContext.HttpContext).GetId();
        }
    }
}
