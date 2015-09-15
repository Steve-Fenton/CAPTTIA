using System;
using System.Web.Security;

namespace Fenton.Capttia
{
    public static class AnonymousIdentifier
    {
        /// <summary>
        /// <para>The <c>SetIdentifier</c> method generates an Anonymous ID if one isn't set.</para>
        /// <para>Add the following code to your Global.asax code file:</para>
        /// <example>
        /// <code>public void AnonymousIdentification_Creating(object sender, AnonymousIdentificationEventArgs e)
        /// {
        ///     AnonymousIdentifier.SetIdentifier(e);
        /// }</code>
        /// </example>
        /// </summary>
        /// <param name="eventArgs">A <see cref="AnonymousIdentificationEventArgs"/> event.</param>
        public static void SetIdentifier(AnonymousIdentificationEventArgs eventArgs)
        {
            if (eventArgs.AnonymousID == null)
            {
                eventArgs.AnonymousID = "CAPTTIA_ID_" + DateTime.UtcNow.Ticks;
            }
        }
    }
}
