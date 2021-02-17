using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreMailer.Extensions;
using CoreMailer.Interfaces;
using CoreMailer.Models;

namespace CoreMailer.Implementation
{
    public class CoreMvcMailer : ICoreMvcMailer
    {
        private readonly SmtpClient _client;

        public CoreMvcMailer()
        {
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
                var messageBody = "";
                if (mailer.HasLayout)
                {
                    messageBody = mailer.Layout.GetLayout();
                }

                if (mailer.IsHtml)
                {
                    if (mailer.HasViewName)
                    {
                        var emailContent = mailer.Model.GetHtmlEmailContent(mailer.ViewFile);
                        messageBody = Regex.Replace(messageBody, @"(?<![\w]){EMAILCONTENT}(?![\w])", emailContent ?? "");
                    }
                    else
                    {
                        var emailContent = mailer.Model.GetTextEmailContent(mailer.Message);
                        messageBody = string.IsNullOrWhiteSpace(messageBody) == false ? Regex.Replace(messageBody, @"(?<![\w]){EMAILCONTENT}(?![\w])", emailContent ?? "") : emailContent;
                    }
                }
                else
                {
                    var emailContent = mailer.Model.GetTextEmailContent(mailer.Message);
                    messageBody = string.IsNullOrWhiteSpace(messageBody) == false ? Regex.Replace(messageBody, @"(?<![\w]){EMAILCONTENT}(?![\w])", emailContent ?? "") : emailContent;
                }
                var emailMessage =
                    new MailMessage()
                    {
                        From = new MailAddress(mailer.FromAddress),
                        IsBodyHtml = mailer.IsHtml,
                        Subject = mailer.Subject,
                        Body = messageBody
                    };

                foreach (var toAddress in mailer.ToAddresses)
                {
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

                if (mailer.UsePickupDirectory)
                {
                    _client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    if (!Directory.Exists(mailer.PickupPath))
                    {
                        Directory.CreateDirectory(mailer.PickupPath);
                    }
                    _client.PickupDirectoryLocation = mailer.PickupPath;
                }
                else
                {
                    _client.Host = mailer.Host;
                    _client.Port = mailer.Port;
                    _client.Credentials = new NetworkCredential(mailer.User, mailer.Key);
                }
                _client.Send(emailMessage);

            }
        }


        public async Task SendAsync(MailerModel mailer)
        {
            await Task.Run(() =>
            {
                Send(mailer);
            });
        }
    }
}
