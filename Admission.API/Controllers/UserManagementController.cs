using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Admission.API.Controllers
{
    [Route("api/managerUser")]
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
        [HttpGet("getAdmin")]
        public ActionResult GetAdmin([FromQuery] GetById request)
        {
            if (request.Id <= 0) return StatusCode(400, (new { message = "Fields 'id' cannot be enpty or null, must be greater than 0" }));

            var result = _iUserManagementService.GetAdmin(request.Id);

            if (result != null) return StatusCode(200, (new { admin = result }));
            return StatusCode(404, (new { message = "Not found admin" }));
        }

        [HttpGet("getAdmins")]
        public ActionResult GeAdmins([FromQuery] SearchAdmin request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new { message = "Fields '_page', '_limit' cannot be empty or null  AND '_page', '_limit' must be greater than 0" }));

            var result = _iUserManagementService.GetAdmins(request);

            if (result != null) return StatusCode(200, (new
            {
                admins = result["admins"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any admin" }));
        }

        [HttpPost("createAdmin")]
        public async Task<ActionResult> CreateAdmin([FromBody] CreateAdmin request)
        {
            User user = _iUserManagementService.GetUserByEmail(request.Email);

            if (user != null) return StatusCode(400, (new { message = "Account already exists" }));
            if (await _iUserManagementService.CreateAdmin(request)) return StatusCode(201, (new { message = "Create user successed" }));
            return StatusCode(500, (new { message = "Create user failed" }));
        }


        // Counselor
        [HttpGet("getCounselor")]
        public ActionResult GetCounselor([FromQuery] GetById request)
        {
            if (request.Id <= 0) return StatusCode(400, (new { message = "Fields 'id' cannot be enpty or null, must be greater than 0" }));

            var result = _iUserManagementService.GetCounselor(request.Id);

            if (result != null) return StatusCode(200, (new { counselor = result }));
            return StatusCode(404, (new { message = "Not found counselor" }));
        }

        [HttpGet("getCounselors")]
        public ActionResult GetCounselors([FromQuery] SearchCounselor request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new { message = "Fields EC'_page', '_limit' cannot be empty or null  AND '_page', '_limit' must be greater than 0" }));

            var result = _iUserManagementService.GetCounselors(request);

            if (result != null) return StatusCode(200, (new
            {
                counselors = result["counselors"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any counselor" }));
        }

        [HttpPost("createCounselor")]
        public async Task<ActionResult> CreateCounselor([FromBody] CreateCounselor request)
        {
            User user = _iUserManagementService.GetUserByEmail(request.Email);

            if (user != null) return StatusCode(400, (new { message = "Account already exists" }));
            if (await _iUserManagementService.CreateCounselor(request)) return StatusCode(201, (new { message = "Create user successed" }));
            return StatusCode(500, (new { message = "Create user failed" }));
        }


        // Student
        [HttpGet("getStudent")]
        public ActionResult GetStudent([FromQuery] GetById request)
        {
            if (request.Id <= 0) return StatusCode(400, (new { message = "Fields 'id' cannot be enpty or null, must be greater than 0" }));

            var result = _iUserManagementService.GetStudent(request.Id);

            if (result != null) return StatusCode(200, (new { student = result }));
            return StatusCode(404, (new { message = "Not found student" }));
        }

        [HttpGet("getStudents")]
        public ActionResult GetStudent([FromQuery] SearchStudent request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new { message = "Fields '_page', '_limit' cannot be empty or null  AND '_page', '_limit' must be greater than 0" }));

            var result = _iUserManagementService.GetStudents(request);

            if (result != null) return StatusCode(200, (new
            {
                students = result["students"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { message = "Not found any student" }));
        }

        [HttpPost("createStudent")]
        public async Task<ActionResult> CreateStudent([FromBody] CreateStudent request)
        {
            User user = _iUserManagementService.GetUserByEmail(request.Email);

            if (user != null) return StatusCode(400, (new { message = "Account already exists" }));
            if (await _iUserManagementService.CreateStudent(request)) return StatusCode(201, (new { message = "Create user successed" }));
            return StatusCode(500, (new { message = "Create user failed" }));
        }




        [HttpPut("updateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUser request)
        {
            User user = _iUserManagementService.GetUserById(request.Id);

            if (user == null) return StatusCode(404, (new { message = "Not found account" }));
            if (await _iUserManagementService.UpdateUser(request)) return StatusCode(200, (new { message = "Update user successed" }));
            return StatusCode(500, (new { message = "Update user failed" }));
        }
    }
}
