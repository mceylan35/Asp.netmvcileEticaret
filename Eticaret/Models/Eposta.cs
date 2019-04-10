using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Eticaret.Models
{
    public class Eposta
    {
        public static void SendMail(string body)
        {
            var fromAddress = new MailAddress("yedekhesap.188@gmail.com", "Leaf Solutions Geribildirim");
            var toAddress = new MailAddress("yedekhesap.188@gmail.com");
            const string subject = "Leaf Solutions Geribildirim";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "furkan456")
                //trololol kısmı e-posta adresinin şifresi
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}