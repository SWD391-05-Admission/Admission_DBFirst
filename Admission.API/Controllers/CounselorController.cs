using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/counselor")]
    [ApiController]
    public class CounselorController : ControllerBase
    {
        private readonly ICounselorService _iCounselorService;
        public CounselorController(ICounselorService iCounselorService)
        {
            _iCounselorService = iCounselorService;
        }

        [Authorize(Roles = "Student")]
        [HttpGet("getCounselors")]
        public ActionResult GetCounselors([FromQuery] SearchCounselor request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new { message = "Fields '_page', '_limit' cannot be empty or null  AND '_page', '_limit' must be greater than 0" }));

            var result = _iCounselorService.GetCounselorsForUser(request);

            if (result != null) return StatusCode(200, (new
            {
                counselors = result["counselors"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any counselor" }));
        }

        [Authorize(Roles = "Counselor")]
        [HttpGet("getCounselor")]
        public ActionResult GetCounselor()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            int userId = Convert.ToInt32(claim[0].Value);

            var counselor = _iCounselorService.GetCounselor(userId);

            if (counselor == null) return StatusCode(404, (new { message = "Not found account" }));
            return StatusCode(200, (new { counselor }));
        }

        [Authorize(Roles = "Counselor")]
        [HttpPut("updateCounselor")]
        public async Task<ActionResult> UpdateCounselor([FromBody] UpdateCounselor request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var counselor = _iCounselorService.GetCounselor(userId);

            if (counselor == null) return StatusCode(404, (new { message = "Not found account" }));
            if (await _iCounselorService.UpdateCounselor(counselor.Id, request)) return StatusCode(200, (new { message = "Update user successed" }));
            return StatusCode(500, (new { message = "Update user failed" }));
        }
    }
}
