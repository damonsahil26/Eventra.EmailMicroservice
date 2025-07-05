using Eventra.EmailMicroservice.API.Models;

namespace Eventra.EmailMicroservice.API.Repositories.Interfaces
{
    public interface IMailPersistanceRepository
    {
        public Task<bool> SaveMailLog(EmailLog emailLog);
    }
}
