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

        [HttpGet("getTalkshow")]
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

        [HttpGet("getTalkshowsWaiting")]
        public ActionResult GetTalkshowsWaiting([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetTalkshowsWaiting(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("getTalkshowsApproved")]
        public ActionResult GetTalkshowsApproved([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetTalkshowsApproved(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("getTalkshowsFinish")]
        public ActionResult GetTalkshowsFinish([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetTalkshowsFinish(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpGet("getTalkshowsCancel")]
        public ActionResult GetTalkshowsCancel([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var result = _iTalkshowManagementService.GetTalkshowsCancel(userId, request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpPost("createTalkshow")]
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

        // dien gia cap nhat talk show
        [HttpPut("updateTalkshow")]
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
            if (talkshow.IsApprove) return StatusCode(400, (new { message = "Talkshow approved" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow canceled" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (await _iTalkshowManagementService.UpdateTalkshow(userId, request)) return StatusCode(200, (new { message = "Update talkshow successed" }));
            return StatusCode(500, (new { message = "Update talkshow failed" }));
        }

        [HttpPut("cancelTalkshow")]
        public async Task<ActionResult> CancelTalkshow([FromBody] GetById request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            Talkshow talkshow = _iTalkshowManagementService.GetTalkshow(userId, request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow canceled" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (await _iTalkshowManagementService.CancelTalkshow(userId, request.Id)) return StatusCode(200, (new { message = "Cancel talkshow successed" }));
            return StatusCode(500, (new { message = "Cancel talkshow failed" }));
        }
    }
}
