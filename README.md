# CAPTTIA

CAPTTIA stands for "Completely Automated Public Turing test That Isn't Annoying".

Unlike a traditional CAPTCHA, which asks the user to decode text from an image or solve a puzzle, CAPTTIA is invisible to end users.

## NuGet

    PM> Install-Package Fenton.Capttia 

## Implementing CAPTTIA

CAPTTIA can be easily included in your MVC project. Here are the changes you need to wire it up.

### Configuration

There are two additions to make to your Web.config.

 - Add the capttia section to the `configSections` element
 - Add the capttia element, **ensuring you create your own pass phrases and salt** (salt must be 16 characters, pass phrases are unlimited)

Anonymous identification is used to tie request and response tokens as part of the CAPTTIA process.

You can change the module name to disguise the phrase "capttia" in the output. Just ensure that the names won't collide in your application (i.e. you have no HTML elements with the ID you supply here).

    <configSections>
        <section name="capttia" type="Fenton.Capttia.CapttiaSection,Fenton.Capttia" />
        <!-- ... -->
    </configSections>
    <capttia ModuleName="capttia" 
             PassPhrase="{F89DBE7E-F3A4-44CD-8152-E21FD6FD532D}"
             PassPhraseB="{0D310A86-5FF8-4D1D-A054-656F39B145E3}"
             Salt="bD84Ae8g7f15cF9B" />

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
    }

## How it Works

CAPTTIA makes a number of checks to ensure the request is from a human. It also checks for requests that were originally created by a human, but are now being re-played by a robot.

When CAPTTIA detects a problem, it invalidates the ViewState and adds an error to ModelErrors.
