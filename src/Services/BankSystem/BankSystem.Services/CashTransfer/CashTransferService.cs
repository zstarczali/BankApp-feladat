using BankSystem.Services.Models.CashTransfer;

namespace BankSystem.Services.CashTransfer
{
    using AutoMapper;

    using BankSystem.Models;

    using Common;
    using Common.EmailSender.Interface;

    using Data;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CashTransferService : BaseService, ICashTransferService
    {
        private readonly IEmailSender emailSender;
        private readonly IMapper mapper;

        public CashTransferService(BankSystemDbContext context, IEmailSender emailSender, IMapper mapper)
            : base(context)
        {
            this.emailSender = emailSender;
            this.mapper = mapper;
        }

        public async Task<bool> CreateCashTransferAsync<T>(T model)
            where T : CashTransferCreateServiceModel
        {
            if (!this.IsEntityStateValid(model))
            {
                return false;
            }

            var dbModel = this.mapper.Map<MoneyTransfer>(model);
            var userAccount = await this.Context
                .Accounts
                .Include(u => u.User)
                .Where(u => u.Id == dbModel.AccountId)
                .SingleOrDefaultAsync();
            if (userAccount == null)
            {
                return false;
            }

            userAccount.Balance += dbModel.Amount;

            dbModel.MadeOn = DateTime.UtcNow;
            this.Context.Update(userAccount);

            await this.Context.Transfers.AddAsync(dbModel);
            await this.Context.SaveChangesAsync();

            if (dbModel.Amount > 0)
            {
                await this.emailSender.SendEmailAsync(dbModel.Account.User.Email, EmailMessages.ReceiveMoneySubject,
                    string.Format(EmailMessages.ReceiveMoneyMessage, dbModel.Amount));
            }
            else
            {
                await this.emailSender.SendEmailAsync(dbModel.Account.User.Email, EmailMessages.SendMoneySubject,
                    string.Format(EmailMessages.SendMoneyMessage, Math.Abs(dbModel.Amount)));
            }

            return true;
        }

    }
}