using Admission.Bussiness.IService;
using Admission.Bussiness.NotiModels;
using Admission.Data.IRepository;
using Admission.Data.Models;
using CorePush.Google;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Admission.Bussiness.NotiModels.GoogleNotification;

namespace Admission.Bussiness.Service
{
    public class SlotService : ISlotService
    {
        private readonly ITalkshowRepository _iTalkshowRepository;
        private readonly ISlotRepository _iSlotRepository;
        private readonly IWalletRepository _iWalletRepository;
        private readonly ITransactionRepository _iTransactionRepository;
        private readonly ICounselorRepository _iCounselorRepository;

        private readonly FcmNotificationSetting _fcmNotificationSetting;

        public SlotService(ITalkshowRepository iTalkshowRepository, ISlotRepository iSlotRepository, IWalletRepository iWalletRepository
            , ITransactionRepository iTransactionRepository, ICounselorRepository iCounselorRepository, IOptions<FcmNotificationSetting> settings)
        {
            _iTalkshowRepository = iTalkshowRepository;
            _iSlotRepository = iSlotRepository;
            _iWalletRepository = iWalletRepository;
            _iTransactionRepository = iTransactionRepository;
            _iCounselorRepository = iCounselorRepository;

            _fcmNotificationSetting = settings.Value;
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
                    if (await _iWalletRepository.UpdateWallet(wallet, false)){
                        NotificationModel notificationModel = new()
                        {
                            DeviceId = "",
                            IsAndroiodDevice = true,
                            Title = "Thông báo giao dịch",
                            Body = studentId + "-Tham gia talkshow thành công. Bạn đã thanh toán " + slot.Price + " dưa hấu",
                        };
                        return await SendNotification(notificationModel);
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
                    if (await _iWalletRepository.UpdateWallet(wallet, false))
                    {
                        NotificationModel notificationModel = new()
                        {
                            DeviceId = "",
                            IsAndroiodDevice = true,
                            Title = "Thông báo giao dịch",
                            Body = studentId + "-Hủy talkshow thành công. Bạn được hoàn lại " + slot.Price + " dưa hấu",
                        };
                        return await SendNotification(notificationModel);
                    }
                }
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
