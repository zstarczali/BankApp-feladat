namespace BankSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;



    public class BankAccount
    {
        public enum AccountType
        {
            NormalAccount,
            SavingAccount,
            Other
        }

        public enum CurrencyType
        {
            HUF = 348,
            EUR = 978 // and so on and so forth
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public CurrencyType Currency { get; set; }

        [Required]
        public float InterestRate { get; set; }

        [Required]
        public decimal Credit { get; set; }

        [Required]
        public AccountType TypeId { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.UniqueIdMaxLength)]
        public string UniqueId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string UserId { get; set; }

        public BankUser User { get; set; }

        public ICollection<MoneyTransfer> Transfers { get; set; }
    }
}