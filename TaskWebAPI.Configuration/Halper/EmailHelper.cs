using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebAPI.Configuration.Halper
{
	public class EmailHelper
	{
		private readonly IConfiguration _configuration;
		private readonly MailSettingElement _mailSetting;
		private SmtpClient _client;
		private string _ccAddress;

		public EmailHelper(IConfiguration configuration)
		{
			_configuration = configuration;
			_mailSetting = _configuration.GetSection("mailSettings").Get<MailSettingElement>();
			_client = new SmtpClient(_mailSetting?.Host)
			{
				UseDefaultCredentials = false,
				Credentials = new System.Net.NetworkCredential(_mailSetting.UserName, _mailSetting.Password),
				Port = 587,
				EnableSsl = true,
				TargetName = "hello",
			};
			_ccAddress = _configuration["DefaultVariables:ccAddress"] ?? "";
			_ccAddress = _configuration["DefaultVariables:bccAddress"] ?? "";
		}

		public bool OTPEmail(string email, string otp)
		{
			try
			{
				var mailRequest = new MailRequest();
				mailRequest.ToEmail = email;
				mailRequest.Subject = "Welcome to XYZ - OTP Information";
				mailRequest.Body = "Dear Sir, <br><br>"

				+ "Your OTP is: " + otp + ". Please do not share your OTP with others - DiscountLagbe.";

				var mailResult = SendEmailAsync(mailRequest);

				return true;
			}
			catch (Exception ex)
			{
				var errorMessage = ex.Message;
				return false;
			}
		}

		private async Task<bool> SendEmailAsync(MailRequest mailRequest)
		{
			try
			{
				#region WebMail SMTP
				// Set up SMTP client
				System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("Host Address", 25);
				client.EnableSsl = true;
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential("Your mail", "Password");

				// Create email message
				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress("Your mail");
				mailMessage.To.Add(mailRequest.ToEmail ?? "");
				mailMessage.Subject = mailRequest.Subject;
				mailMessage.IsBodyHtml = true;
				mailMessage.Body = mailRequest.Body;

				// Send email
				client.Send(mailMessage);
				#endregion

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}