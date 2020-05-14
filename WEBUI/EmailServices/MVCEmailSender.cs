using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Prj.WebUI.EmailServices
{
    public class MVCEmailSender : IEmailSender
    {

        private const string pwdGmail = "123456";


        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(subject, htmlMessage, email);
        }

        private Task Execute(string subject, string htmlMessage, string email)
        {
            var fromMailAdress = new MailAddress("example@example.com", "ShopApp Management");
            var toAddress = new MailAddress(email, "Dear Customer");
            var stmp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromMailAdress.Address, pwdGmail)
            };

            var message = new MailMessage(fromMailAdress, toAddress) { Subject = subject, Body = htmlMessage, };
            var result = stmp.SendMailAsync(message);
            return result;



        }


    }
}

