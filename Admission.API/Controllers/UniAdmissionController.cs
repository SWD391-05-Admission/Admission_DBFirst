using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Admission.API.Controllers
{
    [Route("api/v1/uniAdmission")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UniAdmissionController : ControllerBase
    {
        private readonly IUniAdmissionService _iUniAdmissionService;
        public UniAdmissionController(IUniAdmissionService iUniAdmissionService)
        {
            _iUniAdmissionService = iUniAdmissionService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUniAdmission([FromBody] CreateUniAdmission request)
        {
            var uniAdmission = _iUniAdmissionService.GetUniAdmission(request.UniversityId, request.AdmissionId);

            if (uniAdmission != null) return StatusCode(400, (new { message = "UniAdminssion already exists" })); 
            if (await _iUniAdmissionService.CreateUniAdmission(request)) return StatusCode(201, (new { message = "Create uniAdmission successed" }));
            return StatusCode(500, (new { message = "Create uniAdmission failed" }));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUniAdmission([FromBody] CreateUniAdmission request)
        {
            var uniAdmission = _iUniAdmissionService.GetUniAdmission(request.UniversityId, request.AdmissionId);

            if (uniAdmission == null) return StatusCode(400, (new { message = "UniAdminssion does not exists" }));
            if (await _iUniAdmissionService.DeleteUniAdmission(uniAdmission)) return StatusCode(200, (new { message = "Delete uniAdmission successed" }));
            return StatusCode(500, (new { message = "Delete uniAdmission failed" }));
        }
    }
}
