using Admission.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.IRepository
{
    public interface IWalletRepository
    {
        Wallet GetWallet(int studentId);
        Task<bool> InsertWallet(Wallet wallet);
        Task<bool> UpdateWallet(Wallet newWallet, bool isLoop);
    }
}
