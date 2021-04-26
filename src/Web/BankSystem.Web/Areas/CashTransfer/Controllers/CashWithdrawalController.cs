using AutoMapper;

using BankSystem.Common;
using BankSystem.Services.BankAccount;
using BankSystem.Services.CashTransfer;
using BankSystem.Services.Models.BankAccount;
using BankSystem.Web.Areas.CashTransfer.Models;
using BankSystem.Web.Infrastructure;

using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;
using BankSystem.Services.Models.CashTransfer;

namespace BankSystem.Web.Areas.CashTransfer.Controllers
{
    public class CashWithdrawalController : BaseCashTransferController
    {
        private readonly IBankAccountService bankAccountService;
        private readonly ICashTransferService moneyTransferService;

        public CashWithdrawalController(
            IBankAccountService bankAccountService,
            ICashTransferService moneyTransferService, 
            IMapper mapper) :
            base(bankAccountService, mapper)
        {
            this.bankAccountService = bankAccountService;
            this.moneyTransferService = moneyTransferService;
        }

        public async Task<IActionResult> Create()
        {
            var userId = this.GetCurrentUserId();
            var userAccounts = await this.GetAllAccountsAsync(userId);

            if (!userAccounts.Any())
            {
                this.ShowErrorMessage(NotificationMessages.NoAccountsError);

                return this.RedirectToHome();
            }

            var model = new CashTransferCreateBindingModel
            {
                OwnAccounts = userAccounts
            };

            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CashTransferCreateBindingModel model)
        {
            var userId = this.GetCurrentUserId();

            if (!this.ModelState.IsValid)
            {
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var account = await this.bankAccountService.GetByIdAsync<BankAccountDetailsServiceModel>(model.AccountId);
            if (account == null || account.UserId != userId)
            {
                return this.Forbid();
            }


            if (account.Balance < model.Amount)
            {
                this.ShowErrorMessage(NotificationMessages.InsufficientFunds);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var referenceNumber = ReferenceNumberGenerator.GenerateReferenceNumber();
            var sourceServiceModel = Mapper.Map<CashTransferCreateServiceModel>(model);

            sourceServiceModel.Amount *= -1;
            sourceServiceModel.ReferenceNumber = referenceNumber;
            sourceServiceModel.Description = model.Description;

            sourceServiceModel.SenderName = account.Name;
            sourceServiceModel.RecipientName = account.UserFullName;
            sourceServiceModel.Destination = sourceServiceModel.RecipientName;
            sourceServiceModel.Source = sourceServiceModel.SenderName;

            if (!await this.moneyTransferService.CreateCashTransferAsync(sourceServiceModel))
            {
                this.ShowErrorMessage(NotificationMessages.TryAgainLaterError);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            this.ShowSuccessMessage(NotificationMessages.SuccessfulMoneyTransfer);

            return this.RedirectToPage("/Account/Login");
        }

    }
}
