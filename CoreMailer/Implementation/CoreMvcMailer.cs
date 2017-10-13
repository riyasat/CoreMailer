using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CoreMailer.Interfaces;
using CoreMailer.Models;

namespace CoreMailer.Implementation
{
    public class CoreMvcMailer : ICoreMailer
    {
        private readonly ITemplateRenderer _renderer;

        private readonly SmtpClient _client;

        public CoreMvcMailer(ITemplateRenderer renderer)
        {
            _renderer = renderer;
            _client = new SmtpClient();
        }

        public void Send(MailerModel mailer)
        {
            if (mailer == null)
            {
                throw new Exception("No valid mailer model found");
            }

            if (mailer.IsValid())
            {
                string messageBody;
                if (_renderer != null)
                {
                    messageBody = mailer.HasViewName
                        ? _renderer.RenderView(mailer.ViewFile, mailer.Model)
                        : mailer.Message;
                }
                else
                {
                    messageBody = mailer.Message;
                }
                    

                var emailMessage =
                    new MailMessage()
                    {
                        From = new MailAddress(mailer.FromAddress),
                        IsBodyHtml = mailer.IsHtml,
                        Subject = mailer.Subject,
                        Body = messageBody
                    };

                foreach (var toAddress in mailer.ToAddresses){
                    emailMessage.To.Add(toAddress);
                }

                foreach (var replyTo in mailer.ReplyTo)
                {
                    emailMessage.ReplyToList.Add(replyTo);
                }

                foreach (var attachment in mailer.Attachments)
                {
                    emailMessage.Attachments.Add(attachment);
                }

                foreach (var ccAddress in mailer.CC)
                {
                    emailMessage.CC.Add(ccAddress);
                }

                foreach (var bccAddress in mailer.BCC)
                {
                    emailMessage.Bcc.Add(bccAddress);
                }
                _client.Host = mailer.Host;
                _client.Port = mailer.Port;
                _client.Credentials = new NetworkCredential(mailer.User, mailer.Key);

                _client.Send(emailMessage);

            }
        }


        public async Task SendAsyn(MailerModel mailer)
        {
            await Task.Run(() =>
            {
                Send(mailer);
            });
        }
    }
}
