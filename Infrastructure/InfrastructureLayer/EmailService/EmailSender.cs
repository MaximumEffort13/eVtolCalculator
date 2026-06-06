using MecalcEmailService.Models;

namespace Infrastructure.InfrastructureLayer.EmailService;

public sealed class EmailSender : IEmailSender
{
    private readonly IEmailService _emailService;

    public EmailSender(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        List<string> emailAddresses = new List<string>() { email };

        var emailModel = new EmailModel
        {
            ToAddresses = emailAddresses,
            Body = htmlMessage,
            Subject = subject,
        };

        await _emailService.SendEmailsAsync(emailModel);
    }
}
