using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/bannerManagermentController")]    [ApiController]
    public class BannerManagermentController : ControllerBase
    {
        private readonly IBannerService _iBannerService;

        public BannerManagermentController(IBannerService iBannerService)
        {
            _iBannerService = iBannerService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getBannersNotShow")]
        public ActionResult GetBannersNotShow([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null  " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var result = _iBannerService.GetBannersNotShow(request);

            if (result != null) return StatusCode(200, (new
            {
                banners = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any banner" }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("showBanner")]
        public async Task<ActionResult> ShowBannerAsync([FromBody] GetById request)
        {
            Talkshow talkshow = _iBannerService.GetTalkshow(request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (talkshow.IsBanner) return StatusCode(400, (new { message = "Banner has been shown" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow has been canceled" }));
            if (await _iBannerService.ShowBanner(request.Id)) return StatusCode(200, (new { message = "Show banner successed" }));
            return StatusCode(500, (new { message = "Show banner failed" }));
        }

        [Authorize(Roles = "Admin, Student")]
        [HttpGet("getBannersShow")]
        public ActionResult GetBannersShow([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null  " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var result = _iBannerService.GetBannersShow(request);

            if (result != null) return StatusCode(200, (new
            {
                banners = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any banner" }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("removeBanner")]
        public async Task<ActionResult> RemoveBannerAsync([FromBody] GetById request)
        {
            Talkshow talkshow = _iBannerService.GetTalkshow(request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (!talkshow.IsBanner) return StatusCode(400, (new { message = "Banner is not shown" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow has been canceled" }));
            if (await _iBannerService.RemoveBanner(request.Id)) return StatusCode(200, (new { message = "Remove banner successed" }));
            return StatusCode(500, (new { message = "Remove banner failed" }));
        }
    }
}
