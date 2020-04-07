using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CoreMailer.Models
{
	public class MailerModel
	{
		[Required(ErrorMessage = "Host Name is Required")]
		public string Host { get; set; }
		[Required(ErrorMessage = "Port Number is Required")]
		public int Port { get; set; }
		[Required(ErrorMessage = "User for the host is Required")]
		public string User { get; set; }
		[Required(ErrorMessage = "Key / password for host user is Required")]
		public string Key { get; set; }
		public bool IsHtml { get; set; }
		[Required(ErrorMessage = "Sender email address is required")]
		[EmailAddress(ErrorMessage = "Please enter valid sender email")]
		public string FromAddress { get; set; }
		[Required(ErrorMessage = "Reciver email address is require")]
		public IList<string> ToAddresses { get; set; }

		internal string To => string.Join(",", ToAddresses);

		public string Subject { get; set; }
		public string ViewFile { get; set; }

		public bool HasViewName => !string.IsNullOrEmpty(ViewFile);

		public string Message { get; set; }

		public List<Attachment> Attachments { get; set; }
		public List<string> ReplyTo { get; set; }
		public List<string> CC { get; set; }
		public List<string> BCC { get; set; }

		public bool UsePickupDirectory { get; set; }

		public string PickupPath { get; set; }

		public Object Model { get; set; }
		public MailerModel(string host, int port)
		{
			Host = host;
			Port = port;
			ToAddresses = new List<string>();
			Attachments = new List<Attachment>();
			ReplyTo = new List<string>();
			CC = new List<string>();
			BCC = new List<string>();

		}
		public MailerModel(string pickupDirectory)
		{
			ToAddresses = new List<string>();
			Attachments = new List<Attachment>();
			ReplyTo = new List<string>();
			CC = new List<string>();
			BCC = new List<string>();
			Host = "localhost";
			Port = 80;
			UsePickupDirectory = true;
			PickupPath = pickupDirectory;

		}
		public bool IsValid()
		{
			if (UsePickupDirectory)
			{
				return true;
			}
			var context = new ValidationContext(this);
			var result = new List<ValidationResult>();
			var valid = Validator.TryValidateObject(this, context, result, true);
			if (valid == false)
			{
				var exceptionMessage = string.Join(",", result.Select(x => x.ErrorMessage));
				throw new Exception(exceptionMessage);
			}

			return true;
		}

	}
}
