using Admission.Data.Models;
using Admission.Data.Repository;
using System;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface ISlotService
    {
        Talkshow GetTalkshow(int talkshowId);
        Slot GetSlot(int studentId, int talkshowId);
        Wallet GetWallet(int studentId);
        Task<bool> BookingTalkshow(int studentId, int talkshowId);
        Task<bool> CancelTalkshow(int studentId, int talkshowId);
    }

    public class SlotService : ISlotService
    {

        private readonly ITalkshowRepository _iTalkshowRepository;
        private readonly ISlotRepository _iSlotRepository;
        private readonly IWalletRepository _iWalletRepository;
        private readonly ITransactionRepository _iTransactionRepository;
        private readonly ICounselorRepository _iCounselorRepository;

        public SlotService(ITalkshowRepository iTalkshowRepository, ISlotRepository iSlotRepository, IWalletRepository iWalletRepository
            , ITransactionRepository iTransactionRepository, ICounselorRepository iCounselorRepository)
        {
            _iTalkshowRepository = iTalkshowRepository;
            _iSlotRepository = iSlotRepository;
            _iWalletRepository = iWalletRepository;
            _iTransactionRepository = iTransactionRepository;
            _iCounselorRepository = iCounselorRepository;
        }

        public Talkshow GetTalkshow(int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshow(null, talkshowId);
        }

        public Slot GetSlot(int studentId, int talkshowId)
        {
            return _iSlotRepository.GetSlot(studentId, talkshowId);
        }

        public Wallet GetWallet(int studentId)
        {
            return _iWalletRepository.GetWallet(studentId);
        }

        public async Task<bool> BookingTalkshow(int studentId, int talkshowId)
        {
            var talkshow = _iTalkshowRepository.GetTalkshow(null, talkshowId);
            var wallet = _iWalletRepository.GetWallet(studentId);
            var counselor = _iCounselorRepository.GetCounselor(talkshow.CounselorId);

            var slot = new Slot()
            {
                Price = talkshow.Price,
                StudentId = studentId,
                TalkshowId = talkshowId
            };
            if (await _iSlotRepository.InsertSlot(slot))
            {
                var transaction = new Transaction()
                {
                    Amount = -1 * slot.Price,
                    CreatedDate = DateTime.Now,
                    Desciption = "Booking talkshow of " + counselor.FullName,
                    WalletId = wallet.Id
                };

                if (await _iTransactionRepository.InsertTransaction(transaction, false))
                {
                    wallet.Amount += transaction.Amount;
                    if (await _iWalletRepository.UpdateWallet(wallet, false))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> CancelTalkshow(int studentId, int talkshowId)
        {
            var slot = _iSlotRepository.GetSlot(studentId, talkshowId);
            var wallet = _iWalletRepository.GetWallet(studentId);
            var talkshow = _iTalkshowRepository.GetTalkshow(null, talkshowId);
            var counselor = _iCounselorRepository.GetCounselor(talkshow.CounselorId);

            var transaction = new Transaction()
            {
                Amount = slot.Price,
                CreatedDate = DateTime.Now,
                Desciption = "Cancel talkshow of " + counselor.FullName,
                WalletId = wallet.Id
            };

            if (await _iSlotRepository.DeleteSlot(studentId, talkshowId, false))
            {
                if (await _iTransactionRepository.InsertTransaction(transaction, false))
                {
                    wallet.Amount += transaction.Amount;
                    if (await _iWalletRepository.UpdateWallet(wallet, false))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
