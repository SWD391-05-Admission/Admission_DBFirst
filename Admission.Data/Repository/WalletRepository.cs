using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public WalletRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public Wallet GetWallet(int studentId)
        {
            return _admissionsDBContext.Wallets.Where(wallet => wallet.StudentId == studentId).FirstOrDefault();
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
            Wallet wallet = _admissionsDBContext.Wallets.Where(talkshow => talkshow.Id == newWallet.Id).FirstOrDefault();
            if (wallet == null) return false;
            wallet = newWallet;
            if (isLoop) return true;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
