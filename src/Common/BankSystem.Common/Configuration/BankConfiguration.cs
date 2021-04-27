using System.ComponentModel.DataAnnotations;

namespace BankSystem.Common.Configuration
{
    public class BankConfiguration
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}$")]
        public string UniqueIdentifier { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]{3}$")]
        public string First3CardDigits { get; set; }

        [Required]
        public string BankName { get; set; }


        [Required]
        public string Country { get; set; }
    }
}