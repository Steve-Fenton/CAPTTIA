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
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var config = ConfigurationManager.GetSection("capttia") as CapttiaSection;

            if (!IsValidToken(config, filterContext))
            {
                filterContext.Controller.ViewData.ModelState.AddModelError(config.ModuleName, "The user does not appear to be human.");
            }

            base.OnActionExecuting(filterContext);
        }

        private static bool IsValidToken(CapttiaSection config, ActionExecutingContext filterContext)
        {
            bool result = false;

            try
            {
                var anonymousId = filterContext.HttpContext.Request.AnonymousID;
                var encryptedCapttia = filterContext.HttpContext.Request.Params[config.ModuleName];

                if (anonymousId != null && encryptedCapttia != null)
                {
                    var decryptedCapttia = Encryption.Decrypt(encryptedCapttia, config.PassPhrase);
                    result = anonymousId.Equals(decryptedCapttia);
                }
            }
            catch (Exception) { }

            return result;
        }
    }
}
