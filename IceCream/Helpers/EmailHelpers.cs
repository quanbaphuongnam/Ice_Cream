using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;


namespace IceCream.Helpers
{
    public class EmailHelpers
    {
        public static bool Send(string fromAddress, string toAddress, string subject, string content)
        {
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                var host = configuration["Gmail:Host"];
                var post = int.Parse(configuration["Gmail:Post"]);
                var username = configuration["Gmail:Username"];
                var password = configuration["Gmail:Password"];
                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);
                var smtpClient = new SmtpClient()
                {
                    Host = host,
                    Port = post,
                    EnableSsl = enable,
                    Credentials = new NetworkCredential(username, password)
                };
                var message = new MailMessage(fromAddress, toAddress);
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
