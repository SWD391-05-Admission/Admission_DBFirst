using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers
{
    [Route("api/v1/oldSchool")]
    [Authorize]
    [ApiController]
    public class OldSchoolController : ControllerBase
    {
        private readonly IOldSchoolService _iOldSchoolService;

        public OldSchoolController(IOldSchoolService iOldSchoolService)
        {
            _iOldSchoolService = iOldSchoolService;
        }

        [HttpGet("oldSchools")]
        public ActionResult GetOldSchools()
        {
            var oldSchools = _iOldSchoolService.GetOldSchools();
            if (oldSchools != null) return StatusCode(200, (new { oldSchools }));
            return StatusCode(404, (new { message = "Not found any old school" }));
        }
    }
}
