using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
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
    [Route("api/v1/talkshowManagement")]
    [Authorize(Roles = "Counselor")]
    [ApiController]
    public class TalkshowManagementController : ControllerBase
    {
        private readonly ITalkshowManagementService _iTalkshowManagementService;
        public TalkshowManagementController(ITalkshowManagementService iTalkshowManagementService)
        {
            _iTalkshowManagementService = iTalkshowManagementService;
        }

        [HttpGet("talkshow")]
        public ActionResult GetTalkshow([FromQuery] GetById request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetTalkshowSQL(userId, request.Id);

            if (result != null) return StatusCode(200, (new { talkshow = result }));
            return StatusCode(404, (new { message = "Not found talkshow" }));
        }

        [HttpGet("waitingApproveTalkshows")]
        public ActionResult GetTalkshowsWaiting([FromQuery] SearchTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetWaitingApproveTalkshows(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("approvedTalkshows")]
        public ActionResult GetApprovedTalkshows([FromQuery] SearchTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetApprovedTalkshows(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("unapprovedTalkshows")]
        public ActionResult GetUnapprovedTalkshows([FromQuery] SearchTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetUnapprovedTalkshows(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("canceledTalkshows")]
        public ActionResult GetCanceledTalkshows([FromQuery] SearchTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetCanceledTalkshows(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("completedTalkshows")]
        public ActionResult GetCompletedTalkshows([FromQuery] SearchTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetCompletedTalkshows(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpPost]
        public async Task<ActionResult> CreateTalkshow([FromBody] CreateTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            if (request.Price <= 0) return StatusCode(400, (new { message = "Price must be greater 0" }));
            DateTime startDate = request.StartDate.AddHours(-7);
            if (DateTime.Now >= startDate) return StatusCode(400, (new { message = "Start date must be greater than current date" }));
            if (await _iTalkshowManagementService.CreateTalkshow(userId, request)) return StatusCode(201, (new { message = "Create talkshow successed" }));
            return StatusCode(500, (new { message = "Create talkshow failed" }));

        }

        [HttpPut]
        public async Task<ActionResult> UpdateTalkshow([FromBody] UpdateTalkshow request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            Talkshow talkshow = _iTalkshowManagementService.GetTalkshow(userId, request.Id);

            if (request.Price <= 0) return StatusCode(400, (new { message = "Price must be greater 0" }));
            DateTime startDate = request.StartDate.AddHours(-7);
            if (DateTime.Now >= startDate) return StatusCode(400, (new { message = "Start date must be greater than current date" }));

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow canceled" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (talkshow.IsApprove) return StatusCode(400, (new { message = "Talkshow approved" }));
            if (await _iTalkshowManagementService.UpdateTalkshow(userId, request)) return StatusCode(200, (new { message = "Update talkshow successed" }));
            return StatusCode(500, (new { message = "Update talkshow failed" }));
        }
    }
}
