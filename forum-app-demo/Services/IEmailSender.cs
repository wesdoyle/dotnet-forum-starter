using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forum_app_demo.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
