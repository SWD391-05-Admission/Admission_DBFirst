using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/uniMajor")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UniMajorController : ControllerBase
    {
        private readonly IUniMajorService _iUniMajorService;
        public UniMajorController(IUniMajorService iUniMajorService)
        {
            _iUniMajorService = iUniMajorService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUniMajor([FromBody] CreateUniMajor request)
        {
            var uniMajor = _iUniMajorService.GetUniMajor(request.UniversityId, request.MajorId);

            if (uniMajor != null) return StatusCode(400, (new { message = "UniMajor already exists" }));
            if (await _iUniMajorService.CreateUniMajor(request)) return StatusCode(201, (new { message = "Create uniMajor successed" }));
            return StatusCode(500, (new { message = "Create uniMajor failed" }));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUniMajor([FromBody] CreateUniMajor request)
        {
            var uniMajor = _iUniMajorService.GetUniMajor(request.UniversityId, request.MajorId);

            if (uniMajor == null) return StatusCode(400, (new { message = "UniMajor does not exists" }));
            if (await _iUniMajorService.DeleteUniMajor(uniMajor)) return StatusCode(200, (new { message = "Delete uniMajor successed" }));
            return StatusCode(500, (new { message = "Delete uniMajor failed" }));
        }
    }
}
