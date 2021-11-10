using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Admission.API.Controllers
{
    [Route("api/v1/talkshow")]
    [Authorize(Roles = "Student")]
    [ApiController]
    public class TalkshowController : ControllerBase
    {
        private readonly ITalkshowService _iTalkshowService;

        public TalkshowController(ITalkshowService iTalkshowService)
        {
            _iTalkshowService = iTalkshowService;
        }

        [HttpGet("talkshow")]
        public ActionResult GetTalkshow([FromQuery] GetById request)
        {
            var result = _iTalkshowService.GetTalkshow(request.Id);

            if (result != null) return StatusCode(200, (new { talkshow = result }));
            return StatusCode(404, (new { message = "Not found talkshow" }));
        }

        [HttpGet("unbookedTalkshows")]
        public ActionResult GetUnbookedTalkshows([FromQuery] SearchTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowService.GetUnbookedTalkshows(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("waitingStartTalkshows")]
        public ActionResult GetWaitingStartTalkshows([FromQuery] SearchTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowService.GetWaitingStartTalkshows(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("bookedTalkshows")]
        public ActionResult GetBookedTalkshows([FromQuery] SearchTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowService.GetBookedTalkshows(userId, request);

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
