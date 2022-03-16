using System.Threading.Tasks;

namespace Fabrikafa.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
