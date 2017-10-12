# CoreMailer

Send email from .NET Core 2.0 with razor template Check the exmple code for details :) happy coding

How to Use:

**To Install**

    npm install CoreMailer -Version 1.0.0

**In Startup.cs add**

    services.AddScoped<ITemplateRenderer, TemplateRenderer>();
    services.AddScoped<ICoreMailer, CoreMailer.Implementation.CoreMailer>();

Create cshtml template under any views folder e.g.

    Views/Emails/Registration.cshtml

**The content of cshtml can be**

    @model UserInfo
    Hello <strong>@Model.UserName</strong> you are <strong>Awxam</strong>

**NOTE: For emails you have to use inline styling.**

in the controller use following:

**Constructor**

    private readonly ICoreMailer _mailer;
    public HomeController(ICoreMailer mailer)
    {
      _mailer = mailer;
    }

**ActionMethod**

    public IActionResult About()
        {
            MailerModel mdl = new MailerModel("YourHostName",1234)
            {
                FromAddress = "Your Address",
                IsHtml = true,
                User = "YourUserName",
                Key ="YourKey",
                ViewFile = "Emails/Register.cshtml",
                Subject = "Registration",
                Model = new // Your actual class model
                {
                }
            };
            
            _mailer.Send(mdl);
            return View();
        }
