using CoreMailer.Interfaces;
using CoreMailerExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreMailerExample.Model;
using CoreMailer.Models;

namespace CoreMailerExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreMvcMailer _coreMailer;

        public HomeController(ILogger<HomeController> logger,ICoreMvcMailer coreMailer)
        {
            _logger = logger;
            _coreMailer = coreMailer;
        }

        public IActionResult Index()
        {
            MailerModel mdl = new MailerModel("d:\\others\\ES_Emails",@"Views\Email\_EmailLayout.html")//Host name and port number
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
