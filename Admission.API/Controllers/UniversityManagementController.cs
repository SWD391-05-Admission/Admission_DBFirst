using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/universityManagement")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UniversityManagementController : ControllerBase
    {
        private readonly IUniversityManagementService _iUniversityManagementService;
        public UniversityManagementController(IUniversityManagementService iUniversityManagementService)
        {
            _iUniversityManagementService = iUniversityManagementService;
        }

        [HttpGet("university")]
        public ActionResult GetUniversity([FromQuery] GetById requets)
        {
            var university = _iUniversityManagementService.GetUniversity(requets.Id);

            if (university != null) return StatusCode(200, (new { university }));
            return StatusCode(404, (new { error = "Not found university" }));
        }

        [HttpGet("universities")]
        public ActionResult GetUniversities([FromQuery] SearchUniversity request)
        {
            var result = _iUniversityManagementService.GetUniversities(request);

            if (result != null) return StatusCode(200, (new
            {
                admins = result["universities"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { error = "Not found any university" }));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUniversity([FromBody] CreateUniversity request)
        {
            var university = _iUniversityManagementService.GetUniversity(request.Code);

            if (university != null) return StatusCode(400, (new { message = "University code already exists" }));
            if (await _iUniversityManagementService.CreateUniversity(request))
            {
                int universityId = _iUniversityManagementService.GetUniversity(request.Code).Id;
                return StatusCode(201, (new { universityId, message = "Create university successed" }));
            }
            return StatusCode(500, (new { error = "Create university failed" }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUniversity([FromBody] UpdateUniversity request)
        {
            var university = _iUniversityManagementService.GetUniversity(request.Id);
            if (university == null) return StatusCode(404, (new { error = "Not found university" }));
            if (await _iUniversityManagementService.UpdateUniversity(request)) return StatusCode(201, (new { message = "Update university successed" }));
            return StatusCode(500, (new { error = "Update university failed" }));
        }
    }
}
