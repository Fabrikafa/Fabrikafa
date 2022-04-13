using System.Threading.Tasks;

namespace Fabrikafa.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string emails, string subject, string message);
    Task SendEmailAsync(string emailsTO, string emailsCC, string emailsBCC, string subject, string htmlMessage);
}
