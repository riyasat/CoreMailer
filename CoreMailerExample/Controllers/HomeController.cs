using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreMailer.Interfaces;
using CoreMailer.Models;
using CoreMailerExample.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMailerExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoreMailer _mailer;

        public HomeController(ICoreMailer mailer)
        {
            _mailer = mailer;
        }
        // GET: /<controller>/
        public IActionResult Index(bool v = false)
        {
            MailerModel mdl = new MailerModel("Host address", 1234)//Host name and port number
            {
                User = "Your User Name",
                Key = "Your Key",
                FromAddress = "Sender email",// someone@something.com
                IsHtml = v,
            };
            mdl.ToAddresses.Add("receiver email"); // add receiver as many as you want

            if (v)
            {
                mdl.ViewFile = "Home/email_view";
                mdl.Model = new UserModel
                {
                    OrganizationName = "Test Organization"
                };
            }
            else
            {
                mdl.Message = "This is test message with only text";
            }
            _mailer.Send(mdl);
            return View();
        }
    }
}
