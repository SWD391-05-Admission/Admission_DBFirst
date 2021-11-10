using Admission.Bussiness.Response;
using Admission.Data.Models;
using Admission.Data.Repository;
using System.Collections.Generic;

namespace Admission.Bussiness.Service
{
    public interface IRoleService
    {
        IEnumerable<RoleRes> GetRoles();
        int GetRoleId(string roleName);
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _iRoleRepository;
        public RoleService(IRoleRepository iRoleRepository)
        {
            _iRoleRepository = iRoleRepository;
        }
        public IEnumerable<RoleRes> GetRoles()
        {
            var roles = _iRoleRepository.GetRoles();
            if (roles != null)
            {
                List<RoleRes> listRoles = new();
                foreach (Role role in roles)
                {
                    listRoles.Add(new RoleRes
                    {
                        Id = role.Id,
                        RoleName = role.RoleName,
                        Description = role.Description
                    });
                }
                return listRoles;
            }
            return null;
        }

        public int GetRoleId(string roleName)
        {
            return _iRoleRepository.GetRoleIdByRolename(roleName); ;
        }
    }
}
