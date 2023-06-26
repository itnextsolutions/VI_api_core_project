using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;

namespace VastraIndiaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        [HttpPost]
        [Route("api/ContactUsController/SendEmail")]
        public IActionResult SendEmail()
        {
          
            var message = new MailMessage();
            message.From = new MailAddress("yogigole1824@gmail.com");
            message.To.Add("yogigole1824@gmail.com");
            message.Subject = "Test email";
            message.Body = "This is a test email from my ASP.NET application.";
            message.IsBodyHtml = true;

            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            {
                client.Port = 465;
                client.Credentials = new NetworkCredential("yogigole1824@gmail.com", "kfpmxkhhyaflodet");
                client.EnableSsl = true;
            }
            client.Send(message);
            return Ok("sucess");
        }
    }
}
