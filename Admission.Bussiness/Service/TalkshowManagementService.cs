using Admission.Bussiness.IService;
using Admission.Bussiness.NotiModels;
using Admission.Bussiness.Request;
using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using Admission.Data.SQLModels;
using CorePush.Google;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Admission.Bussiness.NotiModels.GoogleNotification;

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

        private readonly FcmNotificationSetting _fcmNotificationSetting;

        public TalkshowManagementService(ITalkshowRepository iTalkshowRepository, ISlotRepository iSlotRepository, IWalletRepository iWalletRepository
            , ITransactionRepository iTransactionRepository, ICounselorRepository iCounselorRepository
            , AdmissionsDBContext admissionsDBContext, IOptions<FcmNotificationSetting> settings)
        {
            _iTalkshowRepository = iTalkshowRepository;
            _iSlotRepository = iSlotRepository;
            _iWalletRepository = iWalletRepository;
            _iTransactionRepository = iTransactionRepository;
            _iCounselorRepository = iCounselorRepository;

            _admissionsDBContext = admissionsDBContext;


            _fcmNotificationSetting = settings.Value;
        }

        public TalkshowManagementService()
        {
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


        public Hashtable GetTalkshowsWaiting(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
                , search.Page, search.Limit
                , null, null, false, false, false, null);
        }

        public Hashtable GetTalkshowsApproved(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
                , search.Page, search.Limit
                , null, null, false, false, true, null);
        }

        public Hashtable GetTalkshowsFinish(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
               , search.Page, search.Limit
               , null, null, true, false, true, null);
        }

        public Hashtable GetTalkshowsCancel(int counselorId, SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(counselorId
               , search.Page, search.Limit
               , null, null, false, true, null, null);
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

            return await _iTalkshowRepository.UpdateTalkshow(talkshow, false);
        }



        public async Task<bool> CancelTalkshow(int counselorId, int talkshowId)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(counselorId, talkshowId);
            talkshow.IsCancel = true;

            if (await _iTalkshowRepository.UpdateTalkshow(talkshow, false))
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
                                if (await _iWalletRepository.UpdateWallet(wallet, true))
                                {
                                    NotificationModel notificationModel = new()
                                    {
                                        DeviceId = "",
                                        IsAndroiodDevice = true,
                                        Title = "Thông báo giao dịch",
                                        Body = slot.StudentId + "-Talkshow của " + counselor.FullName + "đã bị hủy. Bạn được hoàn lại " + slot.Price + " dưa hấu",
                                    };
                                    return await SendNotification(notificationModel);
                                }
                            }
                        }
                    }
                }
                return await _admissionsDBContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> SendNotification(NotificationModel notificationModel)
        {
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
