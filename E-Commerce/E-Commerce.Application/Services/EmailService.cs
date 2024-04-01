using E_Commerce.Application.Settings;
using MailKit.Net.Smtp;
using MimeKit;
namespace E_Commerce.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _emailConfig;
        public EmailService(MailSettings emailConfig) => _emailConfig = emailConfig;
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("2B Website", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <html>
                <body>
                    
                    <img src='https://smhttp-ssl-73217.nexcesscdn.net/pub/media/logo/stores/2/logo.png'>
                       <p>{message.Content}</p>
                </body>
                </html>";
            ///string logoPath = "";
            // Attach the logo as a Content-ID attachment
            //if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
            //{
            //    var image = bodyBuilder.LinkedResources.Add(logoPath);
            //    image.ContentId = $"<{Guid.NewGuid()}>";
            //}

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.Smtpserver, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
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
