using Admission.Bussiness.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/oldSchool")]
    [Authorize]
    [ApiController]
    public class OldSchoolController : ControllerBase
    {
        private readonly IOldSchoolService _iOldSchoolService;

        public OldSchoolController(IOldSchoolService iOldSchoolService)
        {
            _iOldSchoolService = iOldSchoolService;
        }

        [HttpGet("getOldSchools")]
        public ActionResult GetAdmissionForms()
        {
            var oldSchools = _iOldSchoolService.GetOldSchools();
            if (oldSchools != null) return StatusCode(200, (new { oldSchools }));
            return StatusCode(404, (new { message = "Not found any old school" }));
        }
    }
}
