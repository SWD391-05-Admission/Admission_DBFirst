using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers
{
    [Route("api/v1/district")]
    [Authorize]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _iDistrictService;

        public DistrictController(IDistrictService iDistrictService)
        {
            _iDistrictService = iDistrictService;
        }

        [HttpGet("districts")]
        public ActionResult GetDistricts()
        {
            var districts = _iDistrictService.GetDistricts();
            if (districts != null) return StatusCode(200, (new { districts }));
            return StatusCode(404, (new { message = "Not found any district" }));
        }
    }
}
