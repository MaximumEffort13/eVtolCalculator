namespace Infrastructure.InfrastructureLayer.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}