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
    [Route("api/talkshowManagement")]
    [Authorize(Roles = "Counselor")]
    [ApiController]
    public class TalkshowManagementController : ControllerBase
    {
        private readonly ITalkshowManagementService _iTalkshowManagementService;
        public TalkshowManagementController(ITalkshowManagementService iTalkshowManagementService)
        {
            _iTalkshowManagementService = iTalkshowManagementService;
        }

        // dien gia xem detail taklshow
        [HttpGet("chuaxongxaiduoc/getTalkshow")]
        public ActionResult GetTalkshow([FromQuery] GetById request)
        {
            if (request.Id <= 0) return StatusCode(400, (new { message = "Fields 'id' cannot be enpty or null, must be greater than 0" }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetTalkshowSQL(userId, request.Id);

            if (result != null) return StatusCode(200, (new { talkshow = result }));
            return StatusCode(404, (new { message = "Not found talkshow" }));
        }

        // dien gia xem danh sach taklshow cua minh
        [HttpGet("getTalkshows")]
        public ActionResult GetTalkshows([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null  " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetTalkshows(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        // dien gia tao talk show
        [HttpPost("createTalkshow")]
        public async Task<ActionResult> CreateTalkshowAsync([FromBody] CreateTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            if (await _iTalkshowManagementService.CreateTalkshow(userId, request)) return StatusCode(201, (new { message = "Create talkshow successed" }));
            return StatusCode(500, (new { message = "Create talkshow failed" }));

        }

        // dien gia cap nhat talk show
        [HttpPut("updateTalkshow")]
        public async Task<ActionResult> UpdateTalkshowAsync([FromBody] UpdateTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            Talkshow talkshow = _iTalkshowManagementService.GetTalkshow(userId, request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (await _iTalkshowManagementService.UpdateTalkshow(userId, request)) return StatusCode(200, (new { message = "Update talkshow successed" }));
            return StatusCode(500, (new { message = "Update talkshow failed" }));
        }

        // dien gia cap nhat talk show
        [HttpPut("finishTalkshow")]
        public async Task<ActionResult> FinishTalkshowAsync([FromBody] GetById request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            Talkshow talkshow = _iTalkshowManagementService.GetTalkshow(userId, request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow has been canceled" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (await _iTalkshowManagementService.FinishTalkshow(userId, request.Id)) return StatusCode(200, (new { message = "Finish talkshow successed" }));
            return StatusCode(500, (new { message = "Finish talkshow failed" }));
        }

        // dien gia cap nhat talk show
        [HttpPut("cancelTalkshow")]
        public async Task<ActionResult> CancelTalkshowAsync([FromBody] GetById request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            Talkshow talkshow = _iTalkshowManagementService.GetTalkshow(userId, request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow has been canceled" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (await _iTalkshowManagementService.CancelTalkshow(userId, request.Id)) return StatusCode(200, (new { message = "Cancel talkshow successed" }));
            return StatusCode(500, (new { message = "Cancel talkshow failed" }));
        }

    }
}
