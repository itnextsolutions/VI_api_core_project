using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;

namespace VastraIndiaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUs1Controller : ControllerBase
    {
        [HttpPost]
        [Route("api/ContactUs1Controller/SendEmail")]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("yogigole1824@gmail.com"));
            email.To.Add(MailboxAddress.Parse("yogigole1824@gmail.com"));
            email.Subject = "test";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 465);
            smtp.Authenticate("yogigole1824@gmail.com", "hrwjmbhogwcwhrbo");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();
            //var message = new MailMessage();
            //message.From = new MailAddress("yogigole1824@gmail.com");
            //message.To.Add("yogigole1824@gmail.com");
            //message.Subject = "Test email";
            //message.Body = "This is a test email from my ASP.NET application.";
            //message.IsBodyHtml = true;

            //var client = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            //{
            //    client.Port = 587;
            //    client.Credentials = new NetworkCredential("yogigole1824@gmail.com", "kfpmxkhhyaflodet");
            //    client.EnableSsl = true;
            //}
            //client.Send(message);
            //return Ok();
        }
    }
}
