using Eventra.EmailMicroservice.API.DTO;

namespace Eventra.EmailMicroservice.API.Services.Interfaces
{
    public interface IMailPersistanceService
    {
        public Task<bool> SaveMailLog(EmailLogDto emailLogDto);
    }
}
