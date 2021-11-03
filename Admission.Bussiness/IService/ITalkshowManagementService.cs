using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.SQLModels;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface ITalkshowManagementService
    {
        Talkshow GetTalkshow(int counselorId, int talkshowId);
        TalkshowSQL GetTalkshowSQL(int counselorId, int talkshowId);
        Hashtable GetTalkshows(int counselorId, SearchTalkshow search);
        Task<bool> CreateTalkshow(int counselorId, CreateTalkshow createTalkshow);
        Task<bool> UpdateTalkshow(int counselorId, UpdateTalkshow updateTalkshow);
        Task<bool> FinishTalkshow(int counselorId, int talkshowId);
        Task<bool> CancelTalkshow(int counselorId, int talkshowId);
    }
}
