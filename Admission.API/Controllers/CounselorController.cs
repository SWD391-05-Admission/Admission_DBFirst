using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/counselor")]
    [ApiController]
    public class CounselorController : ControllerBase
    {
        private readonly ICounselorService _iCounselorService;
        public CounselorController(ICounselorService iCounselorService)
        {
            _iCounselorService = iCounselorService;
        }

        [Authorize(Roles = "Counselor")]
        [HttpGet("counselor")]
        public ActionResult GetCounselor()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            int userId = Convert.ToInt32(claim[0].Value);

            var counselor = _iCounselorService.GetCounselor(userId);

            if (counselor == null) return StatusCode(404, (new { message = "Not found counselor" }));
            return StatusCode(200, (new { counselor }));
        }

        [Authorize(Roles = "Student")]
        [HttpGet("counselors")]
        public ActionResult GetCounselors([FromQuery] SearchCounselor request)
        {
            var result = _iCounselorService.GetCounselors(request);

            if (result != null) return StatusCode(200, (new
            {
                counselors = result["counselors"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any counselor" }));
        }

        [Authorize(Roles = "Counselor")]
        [HttpPut]
        public async Task<ActionResult> UpdateCounselor([FromBody] UpdateCounselor request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var counselor = _iCounselorService.GetCounselor(userId);

            if (counselor == null) return StatusCode(404, (new { message = "Not found counselor" }));
            if (await _iCounselorService.UpdateCounselor(counselor.Id, request)) return StatusCode(200, (new { message = "Update user successed" }));
            return StatusCode(500, (new { message = "Update user failed" }));
        }
    }
}
