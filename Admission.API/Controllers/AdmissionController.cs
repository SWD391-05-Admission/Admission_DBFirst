using Admission.Bussiness.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmisstionService _iAdmisstionService;

        public AdmissionController(IAdmisstionService iAdmisstionService)
        {
            _iAdmisstionService = iAdmisstionService;
        }

        [HttpGet("getAdmissionForms")]
        public ActionResult GetAdmissionForms()
        {
            var addmissionForms = _iAdmisstionService.GetAdmissionForms();
            if (addmissionForms != null) return StatusCode(200, (new { addmissionForms }));
            return StatusCode(404, (new { message = "Not found any admission form" }));
        }
    }
}
