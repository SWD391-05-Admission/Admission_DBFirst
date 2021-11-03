using Admission.Bussiness.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers
{
    [Route("api/role")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getRoles")]
        public ActionResult GetList()
        {
            var listRoles = _roleService.GetListRoles();
            if(listRoles != null) return StatusCode(200, (new { listRoles }));
            return StatusCode(404, (new { error = "Not found any role"}));
        }
    }
}
