using Admission.Bussiness.Response;
using Admission.Data.Models;
using Admission.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IWalletService
    {
        Wallet GetWallet(int studentId);
    }

    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _iWalletRepository;
        public WalletService(IWalletRepository iWalletRepository)
        {
            _iWalletRepository = iWalletRepository;
        }

        public Wallet GetWallet(int studentId)
        {
            return _iWalletRepository.GetWallet(studentId);
        }
    }
}
