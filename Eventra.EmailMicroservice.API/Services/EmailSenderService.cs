using Eventra.EmailMicroservice.API.DTO;
using Eventra.EmailMicroservice.API.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Eventra.EmailMicroservice.API.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _config;
        private readonly IMailPersistanceService _mailPersistanceService;

        public EmailSenderService(IConfiguration config, IMailPersistanceService mailPersistanceService)
        {
            _config = config;
            _mailPersistanceService = mailPersistanceService;
        }

        public async Task<bool> SendConfirmationEmail(EmailRequestDto emailRequest)
        {
            var mailLog = new EmailLogDto
            {
                SentAt = DateTime.Now,
                Status = Enums.Status.Success,
                ToEmail = emailRequest.Email,
                Type = Enums.MailType.Confirmation
            };

            try
            {
                using var smtp = new SmtpClient(_config["Email:SmtpHost"])
                {
                    Port = int.Parse(_config["Email:SmtpPort"]),
                    Credentials = new NetworkCredential(_config["Email:Username"], _config["Email:Password"]),
                    EnableSsl = true
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(_config["Email:From"]),
                    Subject = "Confirm Your Email",
                    Body = GetEmailBodyHtml(emailRequest),
                    IsBodyHtml = true
                };
                mail.To.Add(emailRequest.Email);

                await smtp.SendMailAsync(mail);

                return await _mailPersistanceService.SaveMailLog(mailLog);
            }
            catch (Exception ex)
            {
                mailLog.Status = Enums.Status.Failed;
                mailLog.ErrorMessage = ex.Message;
                return false;
            }
        }

        private static string GetEmailBodyHtml(EmailRequestDto emailRequest)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <style>
    body {{
      font-family: 'Segoe UI', sans-serif;
      background-color: #f5f7fa;
      padding: 20px;
    }}
    .container {{
      max-width: 600px;
      margin: auto;
      background: #ffffff;
      border-radius: 8px;
      padding: 30px;
      box-shadow: 0 2px 8px rgba(0,0,0,0.05);
    }}
    .btn {{
      display: inline-block;
      padding: 12px 24px;
      background-color: #4CAF50;
      color: white;
      text-decoration: none;
      font-weight: bold;
      border-radius: 6px;
      margin-top: 20px;
    }}
    .footer {{
      margin-top: 40px;
      font-size: 12px;
      color: #888888;
      text-align: center;
    }}
  </style>
</head>
<body>
  <div class='container'>
    <h2>Hello {emailRequest.FullName},</h2>
    <p>Thanks for signing up with <strong>Eventra</strong>!</p>
    <p>Please confirm your email address to activate your account:</p>
    <a class='btn' href='{emailRequest.ConfirmationLink}'>Confirm Email</a>
    <p style='margin-top: 30px;'>If you did not create an account, you can safely ignore this email.</p>
    <div class='footer'>
      &copy; {DateTime.UtcNow.Year} Eventra. All rights reserved.
    </div>
  </div>
</body>
</html>
";

        }
    }
}
