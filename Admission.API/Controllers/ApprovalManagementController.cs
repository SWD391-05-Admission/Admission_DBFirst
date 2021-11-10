using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Admission.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/approvalManagement")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ApprovalManagementController : ControllerBase
    {
        private readonly IApproveManagementService _iApproveManagementService;

        public ApprovalManagementController(IApproveManagementService iApproveManagementService)
        {
            _iApproveManagementService = iApproveManagementService;
        }

        [HttpGet("talkshow")]
        public ActionResult GetTalkshow([FromQuery] GetById request)
        {
            var result = _iApproveManagementService.GetTalkshowSQL(request.Id);

            if (result != null) return StatusCode(200, (new { talkshow = result }));
            return StatusCode(404, (new { message = "Not found talkshow" }));
        }

        [HttpGet("waitingApproveTalkshows")]
        public ActionResult GetWaitingApproveTalkshows([FromQuery] SearchTalkshow request)
        {
            var result = _iApproveManagementService.GetWaitingApproveTalkshows(request);

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
            var result = _iApproveManagementService.GetApprovedTalkshows(request);

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
            var result = _iApproveManagementService.GetUnapprovedTalkshows(request);

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
            var result = _iApproveManagementService.GetCanceledTalkshows(request);

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
            var result = _iApproveManagementService.GetCompletedTalkshows(request);

            if (result != null) return StatusCode(200, (new
            {
                talkshows = result["talkshows"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any talkshow" }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateApproveTalkshow([FromBody] UpdateApproveTalkshow request)
        {
            Talkshow talkshow = _iApproveManagementService.GetTalkshow(request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow canceled" }));
            if (request.IsApprove)
            {
                if (talkshow.IsApprove) return StatusCode(400, (new { message = "Talkshow approved" }));
            }
            else
            {
                return StatusCode(400, (new { message = "Cannot unapprove talkshow" }));
            }
            if (await _iApproveManagementService.UpdateApproveTalkshow(request)) return StatusCode(200, (new { message = "Update talkshow successed" }));
            return StatusCode(500, (new { message = "Update talkshow failed" }));
        }
    }
}
