using Admission.Data.Models;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.IRepository
{
    public interface ITalkshowRepository
    {
        Talkshow GetTalkshow(int? counselorId, int talkshowId);
        TalkshowSQL GetTalkshowSQL(int? counselorId, int talkshowId
            , bool? isCancel, bool? isApprove);
        Hashtable GetTalkshows(int? counselorId, int page, int limit, IEnumerable<int> talkshowsId, bool? isBooking
            , bool? isFinish, bool? isCancel, bool? isApprove, bool? isBanner);
        IEnumerable<Talkshow> GetTalkshows();
        Task<bool> InsertTalkshow(Talkshow talkshow);
        Task<bool> UpdateTalkshow(Talkshow newTalkshow, bool isLoop);
    }
}
