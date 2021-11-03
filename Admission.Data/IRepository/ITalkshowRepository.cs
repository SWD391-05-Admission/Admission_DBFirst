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
        Talkshow GetTalkshow(int talkshowId);
        Talkshow GetTalkshow(int counselorId, int talkshowId);
        TalkshowSQL GetTalkshowSQL(int counselorId, int talkshowId, bool isShowAllComplete, bool isShowAllCancel);
        Hashtable GetTalkshows(int counselorId, int page, int limit, IEnumerable<int> talkshowsId, bool? isBooking, bool? isFinish, bool? isCancel, bool? isBanner);
        Task<bool> InsertTalkshow(Talkshow talkshow);
        Task<bool> UpdateTalkshow(Talkshow newTalkshow);
    }
}
