using Eventra.EmailMicroservice.API.DTO;
using Eventra.EmailMicroservice.API.Services.Interfaces;
using Eventra.Shared.DTO.Events;
using MassTransit;

namespace Eventra.EmailMicroservice.API.Consumers
{
    public class RegisterUserEventConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly IEmailSenderService _emailSenderService;

        public RegisterUserEventConsumer(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var message = context.Message;

            var emailRequest = new EmailRequestDto
            {
                Email = message.Email,
                ConfirmationLink = message.ConfirmationLink,
                FullName = message.FullName
            };

            await _emailSenderService.SendConfirmationEmail(emailRequest);
        }
    }
}
