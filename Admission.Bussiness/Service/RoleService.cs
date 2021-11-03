using Admission.Bussiness.IService;
using Admission.Bussiness.Response;
using Admission.Data.IRepository;
using Admission.Data.Models;
using System.Collections.Generic;

namespace Admission.Bussiness.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _iRoleRepository;
        public RoleService(IRoleRepository iRoleRepository)
        {
            _iRoleRepository = iRoleRepository;
        }
        public IEnumerable<RoleRes> GetListRoles()
        {
            var roles = _iRoleRepository.GetListRoles();
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

        public Role GetRoleById(int? roleId)
        {
            return _iRoleRepository.GetRole(roleId);
        }

        public int GetRoleIdByRoleName(string roleName)
        {
            return _iRoleRepository.GetRoleIdByRolename(roleName); ;
        }
    }
}
