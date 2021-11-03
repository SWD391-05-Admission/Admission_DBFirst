using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers
{
    [Route("api/university")]
    [Authorize(Roles = "Counselor, Student")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _iUniversityService;
        public UniversityController(IUniversityService iUniversityService)
        {
            _iUniversityService = iUniversityService;
        }

        [HttpGet("chuaxongxaiduoc/getUniversity")]
        public ActionResult GetUniversity([FromQuery] GetById requets)
        {
            var university = _iUniversityService.GetUniversity(requets.Id);

            if (university != null) return StatusCode(200, (new { university }));
            return StatusCode(404, (new { error = "Not found university" }));
        }

        [HttpGet("chuaxongxaiduoc/getUniversities")]
        public ActionResult GetUniversities([FromQuery] SearchUniversity request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new
            {
                error = "Fields '_page', '_limit' cannot be empty or null  " +
                "AND '_page', '_limit' must be greater than 0"
            }));

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
