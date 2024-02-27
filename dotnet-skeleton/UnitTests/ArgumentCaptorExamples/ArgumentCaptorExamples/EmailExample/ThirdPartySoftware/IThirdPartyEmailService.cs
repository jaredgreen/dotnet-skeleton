namespace dotnet_skeleton.UnitTests.ArgumentCaptorExamples.ArgumentCaptorExamples.EmailExample.ThirdPartySoftware
{
    public interface IThirdPartyEmailService
    {
        Task Send(ThirdPartyEmailContent emailContent);
    }
}