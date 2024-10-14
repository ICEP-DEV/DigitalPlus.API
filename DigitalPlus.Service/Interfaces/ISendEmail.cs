using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface ISendEmail
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
