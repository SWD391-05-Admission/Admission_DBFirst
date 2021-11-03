using Admission.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admission.Data.IRepository
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
}
