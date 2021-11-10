using Admission.Data.Models;
using Admission.Data.Models.Context;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface IUserRepository
    {
        User GetAdmin(int adminId);
        Hashtable GetAdmins(string email, int page, int limit);
        User GetUser(string Email);
        User GetUser(int UserId);
        Task<bool> InsertUser(User user);
        Task<bool> UpdateUser(User newUser);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public UserRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public User GetAdmin(int adminId)
        {
            return _admissionsDBContext.Users.Where(user => user.Id == adminId && user.RoleId == 1).FirstOrDefault();
        }

        public Hashtable GetAdmins(string email, int page, int limit)
        {
            string where = "";
            var admins = _admissionsDBContext.Users.Where(u => u.RoleId == 1);

            if (!string.IsNullOrEmpty(email))
            {
                where += "Email.Contains(\"" + email + "\")";
            }

            if (!string.IsNullOrEmpty(where)) admins = admins.Where(where);

            int count = admins.Count();

            if (admins != null && admins.Any())
            {
                Hashtable result = new();
                if (page > 0 && limit > 0)
                {
                    admins = admins.Skip((page - 1) * limit).Take(limit);
                    result.Add("numPage", (int)Math.Ceiling(((float)count / limit)));
                }
                result.Add("admins", admins);
                return result;
            }
            return null;
        }

        public User GetUser(string email)
        {
            return _admissionsDBContext.Users.Where(user => user.Email == email).FirstOrDefault();
        }

        public User GetUser(int userId)
        {
            return _admissionsDBContext.Users.Where(user => user.Id == userId).FirstOrDefault();
        }

        public async Task<bool> InsertUser(User user)
        {
            if (user == null) return false;
            await _admissionsDBContext.Users.AddAsync(user);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User newUser)
        {
            if (newUser == null) return false;
            User user = _admissionsDBContext.Users.Where(user => user.Id == newUser.Id).FirstOrDefault();
            if (user == null) return false;
            user = newUser;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
