using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using Admission.Data.Repository;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface ITalkshowManagementService
    {
        Talkshow GetTalkshow(int counselorId, int talkshowId);
        TalkshowSQL GetTalkshowSQL(int counselorId, int talkshowId);

        Hashtable GetWaitingApproveTalkshows(int counselorId, SearchTalkshow search);
        Hashtable GetApprovedTalkshows(int counselorId, SearchTalkshow search);
        Hashtable GetUnapprovedTalkshows(int counselorId, SearchTalkshow search);
        Hashtable GetCanceledTalkshows(int counselorId, SearchTalkshow search);
        Hashtable GetCompletedTalkshows(int counselorId, SearchTalkshow search);

        Task<bool> CreateTalkshow(int counselorId, CreateTalkshow createTalkshow);
        Task<bool> UpdateTalkshow(int counselorId, UpdateTalkshow updateTalkshow);
    }
    public class TalkshowManagementService : ITalkshowManagementService
    {
        private readonly ITalkshowRepository _iTalkshowRepository;
        private readonly ISlotRepository _iSlotRepository;
        private readonly IWalletRepository _iWalletRepository;
        private readonly ITransactionRepository _iTransactionRepository;
        private readonly ICounselorRepository _iCounselorRepository;

        private readonly AdmissionsDBContext _admissionsDBContext;

        public TalkshowManagementService(ITalkshowRepository iTalkshowRepository, ISlotRepository iSlotRepository, IWalletRepository iWalletRepository
            , ITransactionRepository iTransactionRepository, ICounselorRepository iCounselorRepository
            , AdmissionsDBContext admissionsDBContext)
        {
            _iTalkshowRepository = iTalkshowRepository;
            _iSlotRepository = iSlotRepository;
            _iWalletRepository = iWalletRepository;
            _iTransactionRepository = iTransactionRepository;
            _iCounselorRepository = iCounselorRepository;

            _admissionsDBContext = admissionsDBContext;
        }

        public Talkshow GetTalkshow(int counselorId, int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshow(counselorId, talkshowId);
        }

        public TalkshowSQL GetTalkshowSQL(int counselorId, int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshowSQL(counselorId, talkshowId
                , null, null);
        }

        public Hashtable GetWaitingApproveTalkshows(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
                , search.Page, search.Limit
                , null, null, false, false, false, null);
        }

        public Hashtable GetApprovedTalkshows(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
                , search.Page, search.Limit
                , null, null, false, false, true, null);
        }

        public Hashtable GetUnapprovedTalkshows(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
               , search.Page, search.Limit
               , null, null, true, false, false, null);
        }

        public Hashtable GetCanceledTalkshows(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
               , search.Page, search.Limit
               , null, null, null, true, null, null);
        }

        public Hashtable GetCompletedTalkshows(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
               , search.Page, search.Limit
               , null, null, true, false, true, null);
        }

        public async Task<bool> CreateTalkshow(int counselorId, CreateTalkshow createTalkshow)
        {
            var talkshow = new Talkshow()
            {
                Description = createTalkshow.Description,
                Image = createTalkshow.Image,
                UrlMeet = createTalkshow.UrlMeet,
                Price = createTalkshow.Price,
                CreatedDate = DateTime.Now,
                StartDate = createTalkshow.StartDate.AddHours(-7),
                IsFinish = false,
                IsCancel = false,
                IsBanner = false,
                CounselorId = counselorId,
                MajorId = createTalkshow.MajorId,
                UniversityId = createTalkshow.UniversityId
            };

            return await _iTalkshowRepository.InsertTalkshow(talkshow);
        }

        public async Task<bool> UpdateTalkshow(int counselorId, UpdateTalkshow updateTalkshow)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(counselorId, updateTalkshow.Id);
            talkshow.Description = updateTalkshow.Description;
            talkshow.Image = updateTalkshow.Image;
            talkshow.UrlMeet = updateTalkshow.UrlMeet;
            talkshow.Price = updateTalkshow.Price;
            talkshow.StartDate = updateTalkshow.StartDate.AddHours(-7);
            talkshow.MajorId = updateTalkshow.MajorId;
            talkshow.UniversityId = updateTalkshow.UniversityId;

            if (updateTalkshow.IsCancel)
            {
                talkshow.IsCancel = true;

                if (await _iTalkshowRepository.UpdateTalkshow(talkshow, false))
                {
                    var counselor = _iCounselorRepository.GetCounselor(talkshow.CounselorId);
                    var slots = _iSlotRepository.GetSlots(talkshow.Id);
                    if (slots != null)
                    {
                        foreach (Slot slot in slots)
                        {
                            var wallet = _iWalletRepository.GetWallet(slot.StudentId);

                            DateTime datenow = DateTime.Now;

                            var transaction = new Transaction()
                            {
                                Amount = slot.Price,
                                CreatedDate = datenow,
                                Desciption = "Talkshow of " + counselor.FullName + " has been canceled",
                                WalletId = wallet.Id
                            };
                            if (await _iSlotRepository.DeleteSlot(slot.StudentId, talkshow.Id, true))
                            {
                                if (await _iTransactionRepository.InsertTransaction(transaction, true))
                                {
                                    wallet.Amount += transaction.Amount;
                                    await _iWalletRepository.UpdateWallet(wallet, true);
                                }
                            }
                        }
                    }
                    return await _admissionsDBContext.SaveChangesAsync() > 0;
                }
            }
            return await _iTalkshowRepository.UpdateTalkshow(talkshow, false);
        }
    }
}
