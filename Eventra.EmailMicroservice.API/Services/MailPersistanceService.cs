using Eventra.EmailMicroservice.API.DTO;
using Eventra.EmailMicroservice.API.Models;
using Eventra.EmailMicroservice.API.Repositories.Interfaces;
using Eventra.EmailMicroservice.API.Services.Interfaces;

namespace Eventra.EmailMicroservice.API.Services
{
    public class MailPersistanceService : IMailPersistanceService
    {
        private readonly IMailPersistanceRepository _mailRepository;

        public MailPersistanceService(IMailPersistanceRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        public async Task<bool> SaveMailLog(EmailLogDto emailLogDto)
        {
            if (emailLogDto == null)
            {
                return false;
            }

            var emailLog = new EmailLog
            {
                SentAt = DateTime.Now,
                Status = emailLogDto.Status,
                ToEmail = emailLogDto.ToEmail,
                Type = emailLogDto.Type
            };

            return await _mailRepository.SaveMailLog(emailLog);
        }
    }
}
