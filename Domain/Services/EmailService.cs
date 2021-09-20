
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Helper;
using Domain.Interface;
using Microsoft.Extensions.Options;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void Send(string to, string subject, string html, string @from = null)
        {
            // create message
            var email = new MimeMessage();
            //   email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailFrom));
            email.From.Add(new MailboxAddress(from ?? _appSettings.EmailFrom, _appSettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            Send(email);

            //// send email
            //using var smtp = new SmtpClient();
            ////smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, true);
            ////smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            //smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, false);
            //smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            //smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
            //smtp.Send(email);
            //smtp.Disconnect(true);
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 465, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);

                    client.Send(mailMessage);
                }
                catch(Exception e)
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }


        }
    }
}
