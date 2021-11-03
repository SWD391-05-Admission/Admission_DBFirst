using Admission.Bussiness.Request;
using Admission.Bussiness.Response;
using Admission.Data.Models;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface IUserManagementService
    {

        AdminRes GetAdmin(int adminId);
        Hashtable GetAdmins(SearchAdmin searchAdmin);
        Task<bool> CreateAdmin(CreateAdmin createAdmin);

        UserCounselor GetCounselor(int counselorId);
        Hashtable GetCounselors(SearchCounselor searchCounselor);
        Task<bool> CreateCounselor(CreateCounselor createCounselor);

        UserStudent GetStudent(int studentId);
        Hashtable GetStudents(SearchStudent searchStudent);
        Task<bool> CreateStudent(CreateStudent createStudent);

        User GetUserById(int userId);
        User GetUserByEmail(string email);

        Task<bool> UpdateUser(UpdateUser updateUser);
    }
}
