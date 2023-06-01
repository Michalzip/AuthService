using AuthService.Modules.Users.Core.ValueObjects.EmailStatus;

namespace AuthService.Modules.Core.Sender
{
    public interface IEmailSenderService
    {
        Task<EmailStatus> SendEmailAsync(string email, string subject, string plainTextContent, string htmlContent);
    }
}