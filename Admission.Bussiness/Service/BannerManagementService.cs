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
    public class BannerManagementService : IBannerManagementService
    {
        private readonly ITalkshowRepository _iTalkshowRepository;
        public BannerManagementService(ITalkshowRepository iTalkshowRepository)
        {
            _iTalkshowRepository = iTalkshowRepository;
        }

        public Talkshow GetTalkshow(int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshow(null, talkshowId);
        }

        public Hashtable GetBannersNotShow(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, false, false, true, true);
        }

        public async Task<bool> ShowBanner(int talkshowId)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(null, talkshowId);
            talkshow.IsBanner = true;
            return await _iTalkshowRepository.UpdateTalkshow(talkshow, false);
        }

        public Hashtable GetBannersShow(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, false, false, true, false);
        }

        public async Task<bool> RemoveBanner(int talkshowId)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(null, talkshowId);
            talkshow.IsBanner = false;
            return await _iTalkshowRepository.UpdateTalkshow(talkshow, false);
        }
    }
}
