using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers
{
    [Route("api/v1/role")]
    [Authorize]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getRoles")]
        public ActionResult GetRoles()
        {
            var listRoles = _roleService.GetRoles();
            if (listRoles != null) return StatusCode(200, (new { listRoles }));
            return StatusCode(404, (new { error = "Not found any role" }));
        }
    }
}
