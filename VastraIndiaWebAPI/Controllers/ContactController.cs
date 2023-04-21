
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using VastraIndiaWebAPI.Models;

namespace VastraIndiaWebAPI.Controllers
{
    public class ContactController : ControllerBase
    {
        [HttpPost]
        [Route("api/ContactController/SendEmail")]
        public IActionResult SendEmail(ContactUsFormData data)
        {
            // Send email to registered email ID
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("your-gmail-usern", "your-gmail-password");
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(data.Email);
            mailMessage.To.Add("registered-email-id@example.com");
            mailMessage.Subject = data.Subject;
            mailMessage.Body = data.Message;

            smtpClient.Send(mailMessage);

            return Ok();
        }
    }

  
}
