using Admission.Data.Models;
using Admission.Data.Models.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface IWalletRepository
    {
        Wallet GetWallet(int studentId);
        Task<bool> InsertWallet(Wallet wallet);
        Task<bool> UpdateWallet(Wallet newWallet, bool isLoop);
    }

    public class WalletRepository : IWalletRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public WalletRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public Wallet GetWallet(int studentId)
        {
            return _admissionsDBContext.Wallets
                .Where(wallet => wallet.StudentId == studentId)
                .Select(wallet => new Wallet
                {
                    Id = wallet.Id,
                    Amount = wallet.Amount,
                    StudentId = wallet.StudentId,
                    Transactions = _admissionsDBContext.Transactions
                    .Where(transaction => transaction.WalletId == wallet.Id)
                    .ToList(),
                }).FirstOrDefault();
        }

        public async Task<bool> InsertWallet(Wallet wallet)
        {
            if (wallet == null) return false;
            await _admissionsDBContext.Wallets.AddAsync(wallet);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateWallet(Wallet newWallet, bool isLoop)
        {
            if (newWallet == null) return false;
            Wallet wallet = _admissionsDBContext.Wallets.Where(wallet => wallet.Id == newWallet.Id).FirstOrDefault();
            if (wallet == null) return false;
            wallet.Amount = newWallet.Amount;
            if (isLoop) return true;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
