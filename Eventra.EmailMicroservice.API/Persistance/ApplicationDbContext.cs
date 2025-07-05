using Eventra.EmailMicroservice.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventra.EmailMicroservice.API.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
