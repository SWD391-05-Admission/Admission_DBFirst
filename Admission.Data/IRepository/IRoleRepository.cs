using Admission.Data.Models;
using System.Collections.Generic;

namespace Admission.Data.IRepository
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetListRoles();
        Role GetRole(int? RoleId);
        int GetRoleIdByRolename(string roleName);
    }
}
