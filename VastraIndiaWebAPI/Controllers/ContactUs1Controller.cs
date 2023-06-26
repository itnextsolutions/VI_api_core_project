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
          
        }
    }
}
