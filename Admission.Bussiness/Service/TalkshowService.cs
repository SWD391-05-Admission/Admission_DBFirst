using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Data.IRepository;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
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

        public Hashtable GetTalkshowsAvailable(int studentId, SearchTalkshow search)
        {
            // lấy danh sách tất cả các dánh sách talkshows chưa đăng kí, chưa complete, chưa cancel
            var talkshowsId = _iSlotRepository.GetTalkshowId(studentId);

            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , talkshowsId, false, false, false, true, null);
        }

        public Hashtable GetTalkshowsPending(int studentId, SearchTalkshow search)
        {
            var talkshowsId = _iSlotRepository.GetTalkshowId(studentId);
            if (talkshowsId != null && talkshowsId.Any())
            {
                return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , talkshowsId, true, false, false, true, null);
            }
            return null;
        }

        public Hashtable GetTalkshowsHistory(int studentId, SearchTalkshow search)
        {
            var talkshowsId = _iSlotRepository.GetTalkshowId(studentId);
            if (talkshowsId != null && talkshowsId.Any())
            {
                // lấy danh sách tất cả các dánh sách talkshows đã đăng kí, đã complete, chưa cancel
                return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , talkshowsId, true, true, false, true, null);
            }
            return null;
        }
    }
}
