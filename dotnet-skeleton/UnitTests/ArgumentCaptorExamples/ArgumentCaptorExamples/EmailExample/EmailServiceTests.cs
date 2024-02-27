#pragma warning disable NUnit2045
using dotnet_skeleton.UnitTests.ArgumentCaptorExamples.ArgumentCaptorExamples.EmailExample.ThirdPartySoftware;
using dotnet_skeleton.UnitTests.Helpers;
using NSubstitute;

namespace dotnet_skeleton.UnitTests.ArgumentCaptorExamples.ArgumentCaptorExamples.EmailExample
{
    [TestFixture]
    public class EmailServiceTests
    {
        private EmailService _sut = null!;
        private IThirdPartyEmailService _thirdPartyEmailService = null!;
        
        private const string Subject = "Earn free cash!!!";
        private const string Content = "Sign up a this random link! Click HERE!";
        private const string From = "spam@outlook.com";
        private const string To = "unsuspecting_person@gmail.com";

        [SetUp]
        public void SetUp()
        {
            _thirdPartyEmailService = Substitute.For<IThirdPartyEmailService>();
            _sut = new EmailService(_thirdPartyEmailService);
        }

        // Exact object matching (this does not work)
        [Test]
        public async Task WhenSend_ThenSendsEmailWithCorrectDetails()
        {
            _thirdPartyEmailService.Send(Arg.Any<ThirdPartyEmailContent>()).Returns(Task.CompletedTask);
            
            await _sut.Send(Subject, Content, From, To);

            var expectedEmailContent = new ThirdPartyEmailContent
            {
                Subject = Subject,
                Content = Content,
                From = From,
                To = new List<Recipient> { new() { EmailAddress = To } }
            };
            await _thirdPartyEmailService.Received().Send(expectedEmailContent);
        }

        // Argument Matchers
        [Test]
        public async Task WhenSend_ThenSendsEmailWithCorrectDetails2()
        {
            _thirdPartyEmailService.Send(Arg.Any<ThirdPartyEmailContent>()).Returns(Task.CompletedTask);
            
            await _sut.Send(Subject, Content, From, To);

            await _thirdPartyEmailService.Received().Send(Arg.Is<ThirdPartyEmailContent>(email => 
                email.Subject == Subject &&
                email.Content == Content &&
                email.From == From &&
                email.To.Any(x => x.EmailAddress == To))
            );
        }

        // Argument Captor
        [Test]
        public async Task WhenSend_ThenSendsEmailWithCorrectDetails3()
        {
            var emailCaptor = new ArgumentCaptor<ThirdPartyEmailContent>();
            _thirdPartyEmailService.Send(emailCaptor.Capture()).Returns(Task.CompletedTask);

            await _sut.Send(Subject, Content, From, To);

            var sentEmail = emailCaptor.Value;
            Assert.That(sentEmail.Subject, Is.EqualTo(Subject));
            Assert.That(sentEmail.Content, Is.EqualTo(Content));
            Assert.That(sentEmail.From, Is.EqualTo(From));
            var emailRecipients = sentEmail.To.Select(x => x.EmailAddress);
            Assert.That(emailRecipients, Does.Contain(To));
        }
        
        // Argument Captor (same concept except we capture after code has executed)
        [Test]
        public async Task WhenSend_ThenSendsEmailWithCorrectDetails4()
        {;
            _thirdPartyEmailService.Send(Arg.Any<ThirdPartyEmailContent>()).Returns(Task.CompletedTask);
            
            await _sut.Send(Subject, Content, From, To);

            var emailCaptor = new ArgumentCaptor<ThirdPartyEmailContent>();
            await _thirdPartyEmailService.Received().Send(emailCaptor.Capture());
            
            var sentEmail = emailCaptor.Value;
            Assert.That(sentEmail.Subject, Is.EqualTo(Subject));
            Assert.That(sentEmail.Content, Is.EqualTo(Content));
            Assert.That(sentEmail.From, Is.EqualTo(From));
            var emailRecipients = sentEmail.To.Select(x => x.EmailAddress);
            Assert.That(emailRecipients.Contains(To), Is.True);
        }
    }
}

#pragma warning restore NUnit2045