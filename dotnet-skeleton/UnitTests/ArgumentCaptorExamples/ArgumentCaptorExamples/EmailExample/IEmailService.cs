namespace dotnet_skeleton.UnitTests.ArgumentCaptorExamples.ArgumentCaptorExamples.EmailExample
{
    public interface IEmailService
    {
        Task Send(string subject, string content, string from, string to);
    }
}