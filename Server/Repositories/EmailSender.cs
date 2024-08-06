using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace News.Server.Repositories
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSender = "audaisam5@gmail.com";
            var password = "Aud@i_446_R#m#_826";

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailSender);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"<html><body>{htmlMessage}</body></html>";            

            using (SmtpClient _smtpClient = new SmtpClient())
            {
                _smtpClient.EnableSsl = true;
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential(emailSender, password);
                _smtpClient.Host = "smtp.gmail.com";
                _smtpClient.Port = 587;
                _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;                              
                _smtpClient.Send(mailMessage);
            }
        }
    }
}
