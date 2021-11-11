using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.Repository;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IBannerManagementService
    {
        Talkshow GetTalkshow(int talkshowId);
        Hashtable GetUnshownBanners(SearchTalkshow search);
        Hashtable GetShownBanners(SearchTalkshow search);
        Task<bool> UpdateBanner(UpdateBanner updateBanner);
    }
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

        public Hashtable GetUnshownBanners(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, false, false, true, false);
        }

        public Hashtable GetShownBanners(SearchTalkshow search)
        {
            return _iTalkshowRepository.GetTalkshows(null
                , search.Page, search.Limit
                , null, null, false, false, true, true);
        }

        public async Task<bool> UpdateBanner(UpdateBanner updateBanner)
        {
            Talkshow talkshow = _iTalkshowRepository.GetTalkshow(null, updateBanner.Id);
            talkshow.IsBanner = updateBanner.IsBanner;
            return await _iTalkshowRepository.UpdateTalkshow(talkshow);
        }
    }
}
