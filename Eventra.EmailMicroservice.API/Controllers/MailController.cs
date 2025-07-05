using Eventra.EmailMicroservice.API.DTO;
using Eventra.EmailMicroservice.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventra.EmailMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailSenderService _emailSender;

        public MailController(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> SendMailConfirmation(EmailRequestDto emailRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .ToList();

                return BadRequest(ModelState);
            }

            await _emailSender.SendConfirmationEmail(emailRequestDto);

            return Ok("Confirmation email sent successfully!");
        }
    }
}
