using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
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
    [Route("api/v1/slot")]
    [Authorize(Roles = "Student")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISlotService _iSlotService;

        public SlotController(ISlotService iSlotService)
        {
            _iSlotService = iSlotService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSlot([FromBody] GetById request)
        {
            var talkshow = _iSlotService.GetTalkshow(request.Id);

            if (talkshow == null) return StatusCode(404, (new { message = "Not found talkshow" }));
            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (talkshow.IsCancel) return StatusCode(400, (new { message = "Talkshow canceled" }));
            if (!talkshow.IsApprove) return StatusCode(400, (new { message = "Talkshow unpproved" }));

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            int userId = Convert.ToInt32(claim[0].Value);

            var slot = _iSlotService.GetSlot(userId, request.Id);

            if (slot != null) return StatusCode(400, (new { message = "Booked this talkshow" }));

            var wallet = _iSlotService.GetWallet(userId);

            if (talkshow.Price > wallet.Amount) return StatusCode(400, (new { message = "Not enought watermelon to booking this tallshow" }));
            if (await _iSlotService.BookingTalkshow(userId, request.Id)) return StatusCode(201, (new { message = "Booking talkshow successed" }));
            return StatusCode(500, (new { message = "Booking talkshow failed" }));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSlot([FromBody] GetById request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            int userId = Convert.ToInt32(claim[0].Value);

            var slot = _iSlotService.GetSlot(userId, request.Id);

            if (slot == null) return StatusCode(400, (new { message = "This talkshow you have not booked" }));

            var talkshow = _iSlotService.GetTalkshow(request.Id);

            if (talkshow.IsFinish) return StatusCode(400, (new { message = "Talkshow finished" }));
            if (await _iSlotService.CancelTalkshow(userId, request.Id)) return StatusCode(200, (new { message = "Cancel talkshow successed" }));
            return StatusCode(500, (new { message = "Cancel talkshow failed" }));
        }
    }
}
