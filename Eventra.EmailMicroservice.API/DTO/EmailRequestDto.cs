using Eventra.EmailMicroservice.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Eventra.EmailMicroservice.API.DTO
{
    public class EmailRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Must be a valid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required")]
        public string ConfirmationLink { get; set; } = string.Empty;
    }
}
