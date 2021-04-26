using BankSystem.Services.Models.CashTransfer;

namespace BankSystem.Services.CashTransfer
{
    using System.Threading.Tasks;

    public interface ICashTransferService
    {

        Task<bool> CreateCashTransferAsync<T>(T model)
            where T : CashTransferCreateServiceModel;
    }
}
