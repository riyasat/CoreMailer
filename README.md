
# CoreMailer
**CoreMailer has been updated to .NET Core 3.1 and doesn't required Razor View renderer**

Send email from .NET Core 3.1 with model binding email template Check the exmple code for details :) happy coding.

*How to Use:*

**To Install**

    npm install CoreMailer -Version 3.0.0

**In Startup.cs add**

    services.AddScoped<ICoreMvcMailer, CoreMvcMailer>();
    services.AddRazorPages();
    
Create html template under any views folder e.g. ***Notice extension of file***

    Views/Emails/_EmailLayout.html
    Views/Emails/EmailContent.html
**The Layout Content Must be in HTML (If you have it)**
HTML Layout **or** Message body  must have parameter **{EMAILCONTENT}** present where you want to insert your email content. **e.g.**

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml" lang="en-GB">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
        <title></title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />    
        <style type="text/css">
            a[x-apple-data-detectors] {
                color: inherit !important;
            }
        </style>
    </head>
    <body style="margin: 0; padding: 0;">
       {EMAILCONTENT}
    </body>
    </html>

**The content must be in HTML (If you have it)** *OR* **Message Body** with parameter name like **{PARAMETERNAME}** 

For example: UserModel has property UserName you should mention that as **{UserName}** within email content

    Hello <strong>{UserName}</strong> you are <strong>Awxam</strong> Regards, {OrganizationName}

**NOTE: For emails you have to use inline styling.**

in the controller use following:

**Constructor**

    private readonly ICoreMvcMailer _mailer;
    public HomeController(ICoreMvcMailer mailer)
    {
      _mailer = mailer;
    }

**ActionMethod**

*HTML Content With Layout and Pickupdirectory*

    public IActionResult Index()
        {
	        MailerModel mdl = new MailerModel("d:\\others\\ES_Emails", @"Views\Email\_EmailLayout.html")//Host name and port number
            {
                User = "Your User Name",
                Key = "Your Key",
                FromAddress = "localhost@localhost.local",// someone@something.com
                IsHtml = true,
                ViewFile = @"Views\Email\EmailContent.html"
            };
             mdl.Model = new UserModel
            {
                UserName = "Riyasat",
                OrganizationName = "Riy Technologies AB"
            };
            mdl.ToAddresses.Add("test@test.com");
            _coreMailer.Send(mdl);
      }  

*Text Content With Layout and Pickupdirectory*

    public IActionResult Index()
    {

            MailerModel mdl = new MailerModel
            {
                User = "Your User Name",
                Key = "Your Key",
                FromAddress = "localhost@localhost.local",// someone@something.com
                IsHtml = false,
                PickupPath = "d:\\others\\ES_Emails",
                UsePickupDirectory = true,
                Message = "Hi {UserName},"+Environment.NewLine+
                          $"This is test email {Environment.NewLine}" +
                          $"Regards, {Environment.NewLine}" +
                          "{OrganizationName}"


            };

            mdl.Model = new UserModel
            {
                UserName = "Riyasat",
                OrganizationName = "Riy Technologies AB"
            };
            mdl.ToAddresses.Add("test@test.com");
            _coreMailer.Send(mdl);
            return View();
     }


**LICENSE**

**CORE MAILER**

Copyright (C) Since 2017, Riy Technologies AB.  

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
