using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Admission.Data.Models;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        private readonly IRoleService _roleService;
        private readonly IUserManagementService _iUserManagementService;

        public LoginController(ILoginService loginService, IRoleService roleService, IUserManagementService iUserManagementService)
        {
            _loginService = loginService;

            _roleService = roleService;
            _iUserManagementService = iUserManagementService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            if (login == null) return StatusCode(400, (new { message = "Not found data in request body" })); if (string.IsNullOrEmpty(login.FirebaseToken)) return StatusCode(400, (new { message = "'firebareToken' is null or empty" }));
            if (string.IsNullOrEmpty(login.App)) return StatusCode(400, (new { message = "'app' null or empty" }));

            // get user record in Firebare API
            UserRecord userRecord = await _loginService.GetUser(login.FirebaseToken);

            if (userRecord != null)
            {
                // get user in database
                User user = _loginService.GetUser(userRecord);

                if (user != null)
                {
                    Role role = _loginService.GetRole(user.RoleId);
                    if (role != null)
                    {
                        if (!role.RoleName.Equals(login.App)) return StatusCode(400, (new { message = "Account is not allowed access in " + login.App + " app" }));
                        if ((bool)!user.IsActive) return StatusCode(400, (new { message = "Account is deactive" }));
                        return StatusCode(200, (new { token = _loginService.GenerateJWT(user) }));
                    }
                    return StatusCode(500, (new { message = "Server error" }));
                }
                if (login.App.Equals("Admin")) return StatusCode(400, (new { message = "Account is not allowed access in Admin app" }));

                //create new user
                User newUser = await _loginService.CreateUser(userRecord, login.App);

                if (newUser != null) return StatusCode(201, (new { token = _loginService.GenerateJWT(newUser) }));
                return StatusCode(500, (new { message = "Register new account failed" }));
            }
            return Unauthorized();
        }

        [HttpGet("test")]
        public async Task<ActionResult> GetJWT(string role)
        {
            User user = _iUserManagementService.GetUserByEmail(role.ToUpper());
            if (user != null) return StatusCode(200, (new { token = _loginService.GenerateJWT(user) }));
            else
            {
                int roleId = _roleService.GetRoleId(role);
                if (roleId > 0)
                {
                    switch (roleId)
                    {
                        case 1:
                            CreateAdmin admin = new()
                            {
                                Email = role.ToUpper()
                            };
                            if (await _iUserManagementService.CreateAdmin(admin))
                            {
                                User newUser = _iUserManagementService.GetUserByEmail(role.ToUpper());
                                return StatusCode(201, (new { token = _loginService.GenerateJWT(newUser) }));
                            }
                            break;
                        case 2:
                            CreateCounselor counselor = new()
                            {
                                Email = role.ToUpper(),
                                FullName = role
                            };
                            if (await _iUserManagementService.CreateCounselor(counselor))
                            {
                                User newUser = _iUserManagementService.GetUserByEmail(role.ToUpper());
                                return StatusCode(201, (new { token = _loginService.GenerateJWT(newUser) }));
                            }
                            break;
                        case 3:
                            CreateStudent student = new()
                            {
                                Email = role.ToUpper(),
                                FullName = role
                            };
                            if (await _iUserManagementService.CreateStudent(student))
                            {
                                User newUser = _iUserManagementService.GetUserByEmail(role.ToUpper());
                                return StatusCode(201, (new { token = _loginService.GenerateJWT(newUser) }));
                            }
                            break;
                    }
                }
                return BadRequest();
            }
        }
    }
}
