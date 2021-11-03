using Admission.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface ISlotService
    {
        Talkshow GetTalkshow(int talkshowId);
        Slot GetSlot(int studentId, int talkshowId);
        Wallet GetWallet(int studentId);
        Task<bool> BookingTalkshow(int studentId, int talkshowId);
        Task<bool> CancelTalkshow(int studentId, int talkshowId);
    }
}
