using BankSystem.Services.Models.CashTransfer;

namespace BankSystem.Web.Areas.CashTransfer.Models
{
    using Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Web.Models.BankAccount;

    public class CashTransferCreateBindingModel : CashTransferCreateBindingModelBase
    {
        [MaxLength(ModelConstants.MoneyTransfer.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.MoneyTransfer.MinStartingPrice,
            ModelConstants.MoneyTransfer.MaxStartingPrice, ErrorMessage =
                "The amount cannot be lower than 0.01")]
        public decimal Amount { get; set; }

        public IEnumerable<OwnBankAccountListingViewModel> OwnAccounts { get; set; }

        [Required]
        [Display(Name = "Source account")]
        public string AccountId { get; set; }

    }
}