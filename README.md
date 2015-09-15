# CAPTTIA

CAPTTIA stands for "Completely Automated Public Turing test That Isn't Annoying".

Unlike a traditional CAPTCHA, which asks the user to decode text from an image or solve a puzzle, CAPTTIA is invisible to end users.

## Implementing CAPTTIA

CAPTTIA can be easily included in your MVC project. Here the small changes you need to wire it up.

### Global.asax

Add a call to `Fenton.Capttia.AnonymousIdentifier.SetIdentifier` to the `AnonymousIdentification_Creating` event handler in your Global.asax.

    public void AnonymousIdentification_Creating(object sender, AnonymousIdentificationEventArgs e)
    {
        AnonymousIdentifier.SetIdentifier(e);
    }

### Configuration

There are three additions to make to your Web.config.

 - Add the capttia section to the `configSections` element
 - Add the capttia element, ensuring you create your own pass phrase
 - Enable `anonymousIdentification` in the `system.web` element

Anonymous identification is used to tie request and response tokens as part of the CAPTTIA process.

    <configSections>
        <section name="capttia" type="Fenton.Capttia.CapttiaSection,Fenton.Capttia" />
        <!-- ... -->
    </configSections>
    <capttia PassPhrase="{F89DBE7E-F3A4-44CD-8152-E21FD6FD532D}" ModuleName="capttia-test" />
    <system.web>
        <anonymousIdentification enabled="true" cookieSlidingExpiration="true" cookieTimeout="120" />
        <!-- ... -->
    </system.web>

### Adding CAPTTIA to a Form

To add CAPTTIA to a form in your MVC application, just add the following HTML Helper call to your view, inside of the form.

    @Html.Capttia(Request)

### Validating CAPTTIA

To validate a form with a CAPPTIA on, add the `ValidateCapttia` attribute to the controller action:

    [HttpPost]
    [ValidateCapttia()]
    public ActionResult Index(HomeModel model)
    {
        // ...
        return View();
    }
