using System.Threading.Tasks;

namespace BankSystem.Common.EmailSender.Interface
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string receiver, string subject, string htmlMessage);
        Task<bool> SendEmailAsync(string sender, string receiver, string subject, string htmlMessage);
    }
}