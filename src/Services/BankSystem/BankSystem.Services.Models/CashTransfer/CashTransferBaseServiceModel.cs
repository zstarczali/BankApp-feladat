using BankSystem.Services.Models.MoneyTransfer;

namespace BankSystem.Services.Models.CashTransfer
{
    using BankSystem.Models;
    using Common.AutoMapping.Interfaces;

    public abstract class CashTransferBaseServiceModel : IMapWith<MoneyTransfer>
    {
    }
}