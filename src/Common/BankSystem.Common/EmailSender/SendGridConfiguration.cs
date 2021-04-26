using System.ComponentModel.DataAnnotations;

namespace BankSystem.Common.EmailSender
{
    public class SendGridConfiguration
    {
        [Required]
        public string ApiKey { get; set; }
    }
}