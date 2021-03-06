using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/student")]
    [Authorize(Roles = "Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _iStudentService;
        public StudentController(IStudentService iStudentService)
        {
            _iStudentService = iStudentService;
        }

        [HttpGet("student")]
        public ActionResult GetStudent()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            int userId = Convert.ToInt32(claim[0].Value);

            var student = _iStudentService.GetStudent(userId);

            if (student == null) return StatusCode(404, (new { message = "Not found account" }));
            return StatusCode(200, (new { student }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudent request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            int userId = Convert.ToInt32(claims[0].Value);

            var student = _iStudentService.GetStudent(userId);

            if (student == null) return StatusCode(404, (new { message = "Not found account" }));
            if (await _iStudentService.UpdateStudent(student.Id, request)) return StatusCode(200, (new { message = "Update user successed" }));
            return StatusCode(500, (new { message = "Update user failed" }));
        }
    }
}
