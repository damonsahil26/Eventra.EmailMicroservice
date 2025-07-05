using Eventra.EmailMicroservice.API.Enums;

namespace Eventra.EmailMicroservice.API.DTO
{
    public class EmailLogDto
    {
        public Guid Id { get; set; }
        public string ToEmail { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public Status Status { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public MailType Type { get; set; }
    }
}
