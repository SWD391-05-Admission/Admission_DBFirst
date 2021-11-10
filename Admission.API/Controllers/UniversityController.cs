using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers
{
    [Route("api/v1/university")]
    [Authorize(Roles = "Counselor, Student")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _iUniversityService;
        public UniversityController(IUniversityService iUniversityService)
        {
            _iUniversityService = iUniversityService;
        }

        [HttpGet("getUniversity")]
        public ActionResult GetUniversity([FromQuery] GetById requets)
        {
            var university = _iUniversityService.GetUniversity(requets.Id);

            if (university != null) return StatusCode(200, (new { university }));
            return StatusCode(404, (new { error = "Not found university" }));
        }

        [HttpGet("getUniversities")]
        public ActionResult GetUniversities([FromQuery] SearchUniversity request)
        {
            var result = _iUniversityService.GetUniversities(request);

            if (result != null) return StatusCode(200, (new
            {
                admins = result["universities"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { error = "Not found any university" }));
        }
    }
}
