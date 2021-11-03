using Admission.Bussiness.Response;
using Admission.Data.Models;
using System.Collections.Generic;

namespace Admission.Bussiness.IService
{
    public interface IRoleService
    {
        IEnumerable<RoleRes> GetListRoles();
        Role GetRoleById(int? roleId);
        int GetRoleIdByRoleName(string roleName);
    }
}
