using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Admission.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/bannerManagerment")]
    [ApiController]
    public class BannerManagermentController : ControllerBase
    {
        private readonly IBannerManagementService _iBannerManagementService;

        public BannerManagermentController(IBannerManagementService iBannerManagementService)
        {
            _iBannerManagementService = iBannerManagementService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("unshownBanners")]
        public ActionResult GetUnshownBanners([FromQuery] SearchTalkshow request)
        {
            var result = _iBannerManagementService.GetUnshownBanners(request);

            if (result != null) return StatusCode(200, (new
            {
                banners = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any banner" }));
        }

        [Authorize(Roles = "Admin, Student")]
        [HttpGet("shownBanners")]
        public ActionResult GetShownBanners([FromQuery] SearchTalkshow request)
        {
            var result = _iBannerManagementService.GetShownBanners(request);

            if (result != null) return StatusCode(200, (new
            {
                banners = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any banner" }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateBanner([FromBody] UpdateBanner request)
        {
            Talkshow talkshow = _iBannerManagementService.GetTalkshow(request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (!talkshow.IsApprove) return StatusCode(400, (new { message = "Talkshow unapproved" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow canceled" }));
            if (request.IsBanner)
            {
                if (talkshow.IsBanner) return StatusCode(400, (new { message = "Banner has been shown" }));
            }
            else
            {
                if (!talkshow.IsBanner) return StatusCode(400, (new { message = "Banner is not shown" }));
            }
            if (await _iBannerManagementService.UpdateBanner(request)) return StatusCode(200, (new { message = "Update banner successed" }));
            return StatusCode(500, (new { message = "Update banner failed" }));
        }
    }
}
