using Eventra.EmailMicroservice.API.DTO;

namespace Eventra.EmailMicroservice.API.Services.Interfaces
{
    public interface IEmailSenderService
    {
        public Task<bool> SendConfirmationEmail(EmailRequestDto emailRequest);
    }
}
