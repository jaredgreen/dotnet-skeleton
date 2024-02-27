using dotnet_skeleton.UnitTests.ArgumentCaptorExamples.ArgumentCaptorExamples.EmailExample.ThirdPartySoftware;

namespace dotnet_skeleton.UnitTests.ArgumentCaptorExamples.ArgumentCaptorExamples.EmailExample
{
    public class EmailService : IEmailService
    {
        private readonly IThirdPartyEmailService _thirdPartyEmailService;

        public EmailService(IThirdPartyEmailService thirdPartyEmailService)
        {
            _thirdPartyEmailService = thirdPartyEmailService;
        }

        public async Task Send(string subject, string content, string from, string to)
        {
            var email = new ThirdPartyEmailContent
            {
                Subject = subject,
                Content = content,
                From = from,
                To = new List<Recipient> { new() { EmailAddress = to } },
            };

            await _thirdPartyEmailService.Send(email);
        }
    }
}