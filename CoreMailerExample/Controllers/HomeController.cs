using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreMailerExample.Models;
using CoreMailer.Interfaces;
using CoreMailer.Models;
using CoreMailerExample.Model;

namespace CoreMailerExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoreMvcMailer _mailer;
        private readonly ITemplateRenderer _render;

        public HomeController(ICoreMvcMailer mailer, ITemplateRenderer render)
        {
            _mailer = mailer;
            _render = render;
        }

        public IActionResult Index()
        {
            MailerModel mdl = new MailerModel("G:\\Home\\emailPickup")//Host name and port number
            {
                User = "Your User Name",
                Key = "Your Key",
                FromAddress = "localhost@localhost.local",// someone@something.com
                IsHtml = true,

            };


            mdl.ViewFile = "Home/email_view";
            mdl.Model = new UserModel
            {
                OrganizationName = "Test Organization"
            };
            mdl.ToAddresses.Add("test@test.com");
            _mailer.Send(mdl);
            return Ok();
        }
        // GET: /<controller>/
        public IActionResult Index_b(bool v = false)
        {
            MailerModel mdl = new MailerModel("Host address", 1234)//Host name and port number
            {
                User = "Your User Name",
                Key = "Your Key",
                FromAddress = "localhost@localhost.local",// someone@something.com
                IsHtml = v,
            };
            mdl.ToAddresses.Add("test@testin.com"); // add receiver as many as you want

            //var view = _render.RenderView("Home/email_view", mdl.Model = new UserModel
            //{
            //    OrganizationName = "Test Organization"
            //});
            //var viewHtml = _render.RenderViewToHtml("Home/email_view", mdl.Model = new UserModel
            //{
            //    OrganizationName = "Test Organization"
            //});

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
