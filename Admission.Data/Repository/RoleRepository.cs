using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using System.Collections.Generic;
using System.Linq;

namespace Admission.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public RoleRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public Role GetRole(int? RoleId)
        {
            Role role = _admissionsDBContext.Roles
                .Where(role => role.Id == RoleId)
                .FirstOrDefault();
            if (role != null) return role;
            return null;
        }

        public IEnumerable<Role> GetListRoles()
        {
            IEnumerable<Role> roles = _admissionsDBContext.Roles.ToList();
            if (roles != null && roles.Any()) return roles;
            return null;
        }

        public int GetRoleIdByRolename(string roleName)
        {
            int? roleId = _admissionsDBContext.Roles
                .Where(role => role.RoleName.ToUpper().Equals(roleName.ToUpper()))
                .Select(role => role.Id)
                .FirstOrDefault();
            if (roleId != null) return (int)roleId;
            return -1;
        }
    }
}
