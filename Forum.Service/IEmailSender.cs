using System.Threading.Tasks;

namespace forum_app_demo.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
