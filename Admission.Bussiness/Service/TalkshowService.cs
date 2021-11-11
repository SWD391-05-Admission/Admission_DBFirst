using Admission.Bussiness.Request;
using Admission.Data.Repository;
using Admission.Data.SQLModels;
using System.Collections;
using System.Linq;

namespace Admission.Bussiness.Service
{
    public interface ITalkshowService
    {
        TalkshowSQL GetTalkshow(int talkshowId);
        Hashtable GetUnbookedTalkshows(int studentId, SearchTalkshow search);
        Hashtable GetWaitingStartTalkshows(int studentId, SearchTalkshow search);
        Hashtable GetBookedTalkshows(int studentId, SearchTalkshow search);
    }

    public class TalkshowService : ITalkshowService
    {
        private readonly ITalkshowRepository _iTalkshowRepository;
        private readonly ISlotRepository _iSlotRepository;

        public TalkshowService(ITalkshowRepository iTalkshowRepository, ISlotRepository iSlotRepository)
        {
            _iTalkshowRepository = iTalkshowRepository;
            _iSlotRepository = iSlotRepository;
        }

        public TalkshowSQL GetTalkshow(int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshowSQL(null, talkshowId
                , false, true);
        }

        public Hashtable GetUnbookedTalkshows(int studentId, SearchTalkshow search)
        {
            var talkshowsId = _iSlotRepository.GetTalkshowId(studentId);
            if (talkshowsId != null && talkshowsId.Any())
            {
                return _iTalkshowRepository.GetTalkshows(null
                    , search.Page, search.Limit
                    , talkshowsId, false, false, false, true, null);

            }
            return null;
        }

        public Hashtable GetWaitingStartTalkshows(int studentId, SearchTalkshow search)
        {
            var talkshowsId = _iSlotRepository.GetTalkshowId(studentId);
            if (talkshowsId != null && talkshowsId.Any())
            {
                return _iTalkshowRepository.GetTalkshows(null
                    , search.Page, search.Limit
                    , talkshowsId, true, false, false, true, null);
            }
            else
            {
                return _iTalkshowRepository.GetTalkshows(null
                    , search.Page, search.Limit
                    , null, null, false, false, true, null);
            }
        }

        public Hashtable GetBookedTalkshows(int studentId, SearchTalkshow search)
        {
            var talkshowsId = _iSlotRepository.GetTalkshowId(studentId);

            if (talkshowsId != null && talkshowsId.Any())
            {
                return _iTalkshowRepository.GetTalkshows(null
                    , search.Page, search.Limit
                    , talkshowsId, true, true, false, true, null);
            }
            return null;
        }
    }
}
