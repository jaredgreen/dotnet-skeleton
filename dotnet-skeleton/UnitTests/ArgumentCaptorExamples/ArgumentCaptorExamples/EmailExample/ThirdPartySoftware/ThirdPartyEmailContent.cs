namespace dotnet_skeleton.UnitTests.ArgumentCaptorExamples.ArgumentCaptorExamples.EmailExample.ThirdPartySoftware
{
    public class ThirdPartyEmailContent
    {
        public string From;
        public IList<Recipient> To;
        public IList<Recipient> Cc;
        public IList<Recipient> Bcc;
        public string Subject;
        public string Content;
    }
}