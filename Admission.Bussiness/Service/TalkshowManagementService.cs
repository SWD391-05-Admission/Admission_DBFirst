using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public class TalkshowManagementService : ITalkshowManagementService
    {
        private readonly ITalkshowRepository _iTalkshowRepository;
        private readonly ISlotRepository _iSlotRepository;
        private readonly IWalletRepository _iWalletRepository;
        private readonly ITransactionRepository _iTransactionRepository;
        private readonly ICounselorRepository _iCounselorRepository;

        private readonly AdmissionsDBContext _admissionsDBContext;

        public TalkshowManagementService(ITalkshowRepository iTalkshowRepository, ISlotRepository iSlotRepository, IWalletRepository iWalletRepository, ITransactionRepository iTransactionRepository, ICounselorRepository iCounselorRepository
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
                , true, true);
        }

        public Hashtable GetTalkshows(int counselorId, SearchTalkshow search)
        {
            // lấy danh sách tất cả các talkshow của counselor
            var talkshowHash = _iTalkshowRepository.GetTalkshows(counselorId
                , search.Page, search.Limit
                , null, null, null, null, null);
            if (talkshowHash != null)
            {
                Hashtable result = new();

                result.Add("talkshows", talkshowHash["talkshows"]);
                result.Add("numPage", talkshowHash["numPage"]);

                return result;
            }
            return null;
        }

        public async Task<bool> CreateTalkshow(int counselorId, CreateTalkshow createTalkshow)
        {
            DateTime datenow = DateTime.Now;
            //datenow = datenow.AddHours(7);
            DateTime startDate = createTalkshow.StartDate;
            startDate = startDate.AddHours(-7);

            var talkshow = new Talkshow()
            {
                Description = createTalkshow.Description,
                Image = createTalkshow.Image,
                UrlMeet = createTalkshow.UrlMeet,
                Price = createTalkshow.Price,
                CreatedDate = datenow,
                StartDate = startDate,
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
            DateTime startDate = updateTalkshow.StartDate;
            startDate = startDate.AddHours(-7);

            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(counselorId, updateTalkshow.Id);
            talkshow.Description = updateTalkshow.Description;
            talkshow.Image = updateTalkshow.Image;
            talkshow.UrlMeet = updateTalkshow.UrlMeet;
            talkshow.Price = updateTalkshow.Price;
            talkshow.StartDate = startDate;
            talkshow.MajorId = updateTalkshow.MajorId;
            talkshow.UniversityId = updateTalkshow.UniversityId;

            return await _iTalkshowRepository.UpdateTalkshow(talkshow);
        }

        public async Task<bool> FinishTalkshow(int counselorId, int talkshowId)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(counselorId, talkshowId);
            talkshow.IsFinish = true;

            return await _iTalkshowRepository.UpdateTalkshow(talkshow);
        }

        public async Task<bool> CancelTalkshow(int counselorId, int talkshowId)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(counselorId, talkshowId);
            talkshow.IsCancel = true;

            // tra dau cho student
            if (await _iTalkshowRepository.UpdateTalkshow(talkshow))
            {
                var counselor = _iCounselorRepository.GetCounselor(talkshow.CounselorId);
                var slots = _iSlotRepository.GetSlots(talkshowId);
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

                        if (await _iSlotRepository.DeleteSlot(slot.StudentId, talkshowId, true))
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
            return false;
        }
    }
}
