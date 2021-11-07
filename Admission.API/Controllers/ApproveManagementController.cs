using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/ApproveManagement")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ApproveManagementController : ControllerBase
    {
        private readonly IApproveManagementService _iApproveManagementService;

        public ApproveManagementController(IApproveManagementService iApproveManagementService)
        {
            _iApproveManagementService = iApproveManagementService;
        }

        [HttpGet("getTalkshowNotApprove")]
        public ActionResult GetTalkshowsNotApprove([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var result = _iApproveManagementService.GetTalkshowsNotApprove(request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpPut("approveTalkshow")]
        public async Task<ActionResult> ApproveTalkshow([FromBody] GetById request)
        {
            Talkshow talkshow = _iApproveManagementService.GetTalkshow(request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow canceled" }));
            if (talkshow.IsApprove) return StatusCode(400, (new { message = "Talkshow approved" }));
            if (await _iApproveManagementService.ApproveTalkshow(request.Id)) return StatusCode(200, (new { message = "Approve talkshow successed" }));
            return StatusCode(500, (new { message = "Approve talkshow failed" }));
        }

        [HttpGet("getTalkshowApproved")]
        public ActionResult GetTalkshowsApprove([FromQuery] SearchTalkshow request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                message = "Fields 'page', 'limit' cannot be empty or null " +
               "AND 'page', 'limit' must be greater than 0"
            }));

            var result = _iApproveManagementService.GetTalkshowsApprove(request);

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
