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
    public class BannerService : IBannerService
    {
        private readonly ITalkshowRepository _iTalkshowRepository;
        public BannerService(ITalkshowRepository iTalkshowRepository)
        {
            _iTalkshowRepository = iTalkshowRepository;
        }

        public Talkshow GetTalkshow(int talkshowId)
        {
            return _iTalkshowRepository.GetTalkshow(talkshowId);
        }

        public Hashtable GetBannersNotShow(SearchTalkshow search)
        {
            var talkshowHash = _iTalkshowRepository.GetTalkshows(-1
                , search.Page, search.Limit
                , null, null, false, false, false);
            if (talkshowHash != null)
            {
                Hashtable result = new();

                result.Add("talkshows", talkshowHash["talkshows"]);
                result.Add("numPage", talkshowHash["numPage"]);

                return result;
            }
            return null;
        }

        public async Task<bool> ShowBanner(int talkshowId)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(talkshowId);
            talkshow.IsBanner = true;

            return await _iTalkshowRepository.UpdateTalkshow(talkshow);
        }
        
        public Hashtable GetBannersShow(SearchTalkshow search)
        {
            var talkshowHash = _iTalkshowRepository.GetTalkshows(-1
                , search.Page, search.Limit
                , null, null, false, false, true);
            if (talkshowHash != null)
            {
                Hashtable result = new();

                result.Add("talkshows", talkshowHash["talkshows"]);
                result.Add("numPage", talkshowHash["numPage"]);

                return result;
            }
            return null;
        }

        public async Task<bool> RemoveBanner(int talkshowId)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(talkshowId);
            talkshow.IsBanner = false;

            // tra xu cho student

            return await _iTalkshowRepository.UpdateTalkshow(talkshow);
        }
    }
}
