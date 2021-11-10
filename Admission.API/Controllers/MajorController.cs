using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers
{
    [Route("api/v1/major")]
    [Authorize]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly IMajorService _iMajorService;
        public MajorController(IMajorService iMajorService)
        {
            _iMajorService = iMajorService;
        }

        [HttpGet("majors")]
        public ActionResult GetMajors()
        {
            var majors = _iMajorService.GetMajors();
            if (majors != null) return StatusCode(200, (new { majors }));
            return StatusCode(404, (new { message = "Not found any major" }));
        }
    }
}
