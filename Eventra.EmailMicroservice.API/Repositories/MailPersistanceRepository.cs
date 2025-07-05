using Eventra.EmailMicroservice.API.Models;
using Eventra.EmailMicroservice.API.Persistance;
using Eventra.EmailMicroservice.API.Repositories.Interfaces;

namespace Eventra.EmailMicroservice.API.Repositories
{
    public class MailPersistanceRepository : IMailPersistanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MailPersistanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveMailLog(EmailLog emailLog)
        {
            if(emailLog == null)
            {
                return false;
            }

            emailLog.Id = Guid.NewGuid();

            await _dbContext.EmailLogs.AddAsync(emailLog);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
