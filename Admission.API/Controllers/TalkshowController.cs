using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Admission.API.Controllers
{
    [Route("api/talkshow")]
    [Authorize(Roles = "Student")]
    [ApiController]
    public class TalkshowController : ControllerBase
    {
        private readonly ITalkshowService _iTalkshowService;

        public TalkshowController(ITalkshowService iTalkshowService)
        {
            _iTalkshowService = iTalkshowService;
        }

        // hoc sinh xem detail taklshow
        [HttpGet("chuaxongxaiduoc/getTalkshow")]
        public ActionResult GetTalkshow([FromQuery] GetById request)
        {
            if (request.Id <= 0) return StatusCode(400, (new { message = "Fields 'id' cannot be enpty or null, must be greater than 0" }));

            var result = _iTalkshowService.GetTalkshow(request.Id);

            if (result != null) return StatusCode(200, (new { talkshow = result }));
            return StatusCode(404, (new { message = "Not found talkshow" }));
        }

        // hoc sinh xem danh sach talkshow chưa booking, chưa finish, chưa cancel
        [HttpGet("getTalkshowsAvailable")]
        public ActionResult GetTalkshowsAvailable([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null  " +
                "AND 'page', 'limit' must be greater than 0"
            }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowService.GetTalkshowsAvailable(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        // hoc sinh xem danh sach talkshow đã đăng kí slot
        [HttpGet("getTalkshowsPending")]
        public ActionResult GetTalkshowsPending([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null  " +
                "AND 'page', 'limit' must be greater than 0"
            }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowService.GetTalkshowsPending(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        // hoc sinh xem danh sach talkshow đã đăng kí slot nhưng đã hoàng thành
        [HttpGet("getTalkshowsHistory")]
        public ActionResult GetTalkshowsHistory([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null  " +
                "AND 'page', 'limit' must be greater than 0"
            }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowService.GetTalkshowsHistory(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }
    }
}
