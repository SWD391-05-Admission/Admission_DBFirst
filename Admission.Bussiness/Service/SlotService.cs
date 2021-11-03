using Admission.Bussiness.IService;
using Admission.Data.IRepository;
using Admission.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public class SlotService : ISlotService
    {
        private readonly ITalkshowRepository _iTalkshowRepository;
        private readonly ISlotRepository _iSlotRepository;
        private readonly IWalletRepository _iWalletRepository;
        private readonly ITransactionRepository _iTransactionRepository;
        private readonly ICounselorRepository _iCounselorRepository;

        public SlotService(ITalkshowRepository iTalkshowRepository, ISlotRepository iSlotRepository, IWalletRepository iWalletRepository, ITransactionRepository iTransactionRepository, ICounselorRepository iCounselorRepository)
        {
            _iTalkshowRepository = iTalkshowRepository;
            _iSlotRepository = iSlotRepository;
            _iWalletRepository = iWalletRepository;
            _iTransactionRepository = iTransactionRepository;
            _iCounselorRepository = iCounselorRepository;
        }

        public Talkshow GetTalkshow(int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshow(talkshowId);
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
            var talkshow = _iTalkshowRepository.GetTalkshow(talkshowId);
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
                DateTime datenow = DateTime.Now;
                //datenow = datenow.AddHours(7);

                var transaction = new Transaction()
                {
                    Amount = -1 * slot.Price,
                    CreatedDate = datenow,
                    Desciption = "Booking talkshow of " + counselor.FullName,
                    WalletId = wallet.Id
                };

                if (await _iTransactionRepository.InsertTransaction(transaction, false))
                {
                    wallet.Amount += transaction.Amount;
                    return await _iWalletRepository.UpdateWallet(wallet, false);
                }
            }
            return false;
        }

        public async Task<bool> CancelTalkshow(int studentId, int talkshowId)
        {
            var slot = _iSlotRepository.GetSlot(studentId, talkshowId);
            var wallet = _iWalletRepository.GetWallet(studentId);
            var talkshow = _iTalkshowRepository.GetTalkshow(talkshowId);
            var counselor = _iCounselorRepository.GetCounselor(talkshow.CounselorId);

            DateTime datenow = DateTime.Now;
            //datenow = datenow.AddHours(7);


            var transaction = new Transaction()
            {
                Amount = slot.Price,
                CreatedDate = datenow,
                Desciption = "Cancel talkshow of " + counselor.FullName,
                WalletId = wallet.Id
            };

            if (await _iSlotRepository.DeleteSlot(studentId, talkshowId, false))
            {
                if (await _iTransactionRepository.InsertTransaction(transaction, false))
                {
                    wallet.Amount += transaction.Amount;
                    return await _iWalletRepository.UpdateWallet(wallet, false);
                }
            }
            return false;
        }
    }
}
