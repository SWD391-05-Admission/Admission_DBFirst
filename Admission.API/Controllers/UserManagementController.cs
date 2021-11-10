using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Admission.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Admission.API.Controllers
{
    [Route("api/v1/userManagement")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _iUserManagementService;
        public UserManagementController(IUserManagementService iUserManagementService)
        {
            _iUserManagementService = iUserManagementService;
        }

        // Admin
        [HttpGet("admin")]
        public ActionResult GetAdmin([FromQuery] GetById request)
        {
            var result = _iUserManagementService.GetAdmin(request.Id);

            if (result != null) return StatusCode(200, (new { admin = result }));
            return StatusCode(404, (new { message = "Not found admin" }));
        }

        [HttpGet("admins")]
        public ActionResult GeAdmins([FromQuery] SearchAdmin request)
        {
            var result = _iUserManagementService.GetAdmins(request);

            if (result != null) return StatusCode(200, (new
            {
                admins = result["admins"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any admin" }));
        }

        [HttpPost("admin")]
        public async Task<ActionResult> CreateAdmin([FromBody] CreateAdmin request)
        {
            User user = _iUserManagementService.GetUserByEmail(request.Email);

            if (user != null) return StatusCode(400, (new { message = "Account already exists" }));
            if (await _iUserManagementService.CreateAdmin(request)) return StatusCode(201, (new { message = "Create user successed" }));
            return StatusCode(500, (new { message = "Create user failed" }));
        }


        // Counselor
        [HttpGet("counselor")]
        public ActionResult GetCounselor([FromQuery] GetById request)
        {
            var result = _iUserManagementService.GetCounselor(request.Id);

            if (result != null) return StatusCode(200, (new { counselor = result }));
            return StatusCode(404, (new { message = "Not found counselor" }));
        }

        [HttpGet("counselors")]
        public ActionResult GetCounselors([FromQuery] SearchCounselor request)
        {
            var result = _iUserManagementService.GetCounselors(request);

            if (result != null) return StatusCode(200, (new
            {
                counselors = result["counselors"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any counselor" }));
        }

        [HttpPost("counselor")]
        public async Task<ActionResult> CreateCounselor([FromBody] CreateCounselor request)
        {
            User user = _iUserManagementService.GetUserByEmail(request.Email);

            if (user != null) return StatusCode(400, (new { message = "Account already exists" }));
            if (await _iUserManagementService.CreateCounselor(request)) return StatusCode(201, (new { message = "Create user successed" }));
            return StatusCode(500, (new { message = "Create user failed" }));
        }


        // Student
        [HttpGet("student")]
        public ActionResult GetStudent([FromQuery] GetById request)
        {
            var result = _iUserManagementService.GetStudent(request.Id);

            if (result != null) return StatusCode(200, (new { student = result }));
            return StatusCode(404, (new { message = "Not found student" }));
        }

        [HttpGet("students")]
        public ActionResult GetStudent([FromQuery] SearchStudent request)
        {
            var result = _iUserManagementService.GetStudents(request);

            if (result != null) return StatusCode(200, (new
            {
                students = result["students"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any student" }));
        }

        [HttpPost("student")]
        public async Task<ActionResult> CreateStudent([FromBody] CreateStudent request)
        {
            User user = _iUserManagementService.GetUserByEmail(request.Email);

            if (user != null) return StatusCode(400, (new { message = "Account already exists" }));
            if (await _iUserManagementService.CreateStudent(request)) return StatusCode(201, (new { message = "Create user successed" }));
            return StatusCode(500, (new { message = "Create user failed" }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUser request)
        {
            User user = _iUserManagementService.GetUserById(request.Id);

            if (user == null) return StatusCode(404, (new { message = "Not found account" }));
            if (await _iUserManagementService.UpdateUser(request)) return StatusCode(200, (new { message = "Update user successed" }));
            return StatusCode(500, (new { message = "Update user failed" }));
        }
    }
}
