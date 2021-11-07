using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Data.IRepository;
using Admission.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
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

        public Hashtable GetTalkshowsNotApprove(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, false, false, false, null);
        }

        public async Task<bool> ApproveTalkshow(int talkshowId)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(null, talkshowId);
            talkshow.IsApprove = true;
            return await _iTalkshowRepository.UpdateTalkshow(talkshow, false);
        }

        public Hashtable GetTalkshowsApprove(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, false, false, true, null);
        }
    }
}
