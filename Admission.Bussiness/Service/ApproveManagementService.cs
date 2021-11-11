using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.Repository;
using Admission.Data.SQLModels;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IApproveManagementService
    {
        Talkshow GetTalkshow(int talkshowId);
        TalkshowSQL GetTalkshowSQL(int talkshowId);

        Hashtable GetWaitingApproveTalkshows(SearchTalkshow search);
        Hashtable GetApprovedTalkshows(SearchTalkshow search);
        Hashtable GetUnapprovedTalkshows(SearchTalkshow search);
        Hashtable GetCanceledTalkshows(SearchTalkshow search);
        Hashtable GetCompletedTalkshows(SearchTalkshow search);

        Task<bool> UpdateApproveTalkshow(UpdateApproveTalkshow updateApproveTalkshow);
    }

    public class ApproveManagementService : IApproveManagementService
    {
        private readonly ITalkshowRepository _iTalkshowRepository;
        public ApproveManagementService(ITalkshowRepository iTalkshowRepository)
        {
            _iTalkshowRepository = iTalkshowRepository;
        }

        public Talkshow GetTalkshow(int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshow(null, talkshowId);
        }

        public TalkshowSQL GetTalkshowSQL(int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshowSQL(null, talkshowId
                , null, null);
        }

        public Hashtable GetWaitingApproveTalkshows(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, false, false, false, null);
        }

        public Hashtable GetApprovedTalkshows(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, false, false, true, null);
        }

        public Hashtable GetUnapprovedTalkshows(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, true, false, false, null);
        }

        public Hashtable GetCanceledTalkshows(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, null, true, null, null);
        }

        public Hashtable GetCompletedTalkshows(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, true, false, true, null);
        }

        public async Task<bool> UpdateApproveTalkshow(UpdateApproveTalkshow updateApproveTalkshow)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(null, updateApproveTalkshow.Id);
            talkshow.IsApprove = updateApproveTalkshow.IsApprove;
            return await _iTalkshowRepository.UpdateTalkshow(talkshow);
        }
    }
}
