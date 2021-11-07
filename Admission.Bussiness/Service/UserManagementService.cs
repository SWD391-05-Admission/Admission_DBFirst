using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Bussiness.Response;
using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.SQLModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _iUserRepository;
        private readonly ICounselorRepository _iCounselorRepository;
        private readonly IStudentRepository _iStudentRepository;
        private readonly IWalletRepository _iWalletRepository;

        public UserManagementService(IUserRepository iUserRepository, ICounselorRepository iCounselorRepository, IStudentRepository iStudentRepository, IWalletRepository iWalletRepository)
        {
            _iUserRepository = iUserRepository;
            _iCounselorRepository = iCounselorRepository;
            _iStudentRepository = iStudentRepository;
            _iWalletRepository = iWalletRepository;
        }

        // Admin
        public AdminRes GetAdmin(int adminId)
        {
            var admin = _iUserRepository.GetAdmin(adminId);
            if (admin != null) return new AdminRes
            {
                Id = admin.Id,
                Email = admin.Email,
                IsActive = admin.IsActive,
                RoleId = admin.RoleId
            };
            return null;
        }

        public Hashtable GetAdmins(SearchAdmin searchAdmin)
        {
            var adminHash = _iUserRepository.GetAdmins(searchAdmin.Email
                , searchAdmin.Page, searchAdmin.Limit);
            if (adminHash != null)
            {
                Hashtable result = new();

                IQueryable<User> admins = (IQueryable<User>)adminHash["admins"];

                List<AdminRes> adminsRes = new();
                foreach (var admin in admins)
                {
                    adminsRes.Add(new AdminRes
                    {
                        Id = admin.Id,
                        Email = admin.Email,
                        IsActive = admin.IsActive,
                        RoleId = admin.RoleId
                    });
                }

                result.Add("admins", adminsRes);
                result.Add("numPage", adminHash["numPage"]);

                return result;
            }
            return null;
        }
        public async Task<bool> CreateAdmin(CreateAdmin createAdmin)
        {
            User newUser = new()
            {
                Email = createAdmin.Email,
                IsActive = true,
                RoleId = 1
            };

            return await _iUserRepository.InsertUser(newUser);
        }

        // Counselor
        public UserCounselor GetCounselor(int counselorId)
        {
            return _iCounselorRepository.GetUserCounselor(counselorId);
        }

        public Hashtable GetCounselors(SearchCounselor searchCounselor)
        {
            return _iCounselorRepository.GetCounselors(searchCounselor.Email, searchCounselor.FullName, searchCounselor.Phone
                , searchCounselor.Page, searchCounselor.Limit, true);
        }

        public async Task<bool> CreateCounselor(CreateCounselor createCounselor)
        {
            User newUser = new()
            {
                Email = createCounselor.Email,
                IsActive = true,
                RoleId = 2
            };
            if (await _iUserRepository.InsertUser(newUser))
            {
                User user = _iUserRepository.GetUser(createCounselor.Email);

                Counselor counselor = new()
                {
                    Id = user.Id,
                    FullName = createCounselor.FullName,
                    Phone = createCounselor.Phone,
                    Avatar = createCounselor.Avatar,
                    Description = createCounselor.Description
                };

                if (await _iCounselorRepository.InsertCounselor(counselor)) return true;
            }
            return false;
        }

        // Student
        public UserStudent GetStudent(int studentId)
        {
            return _iStudentRepository.GetUserStudent(studentId);
        }

        public Hashtable GetStudents(SearchStudent searchStudent)
        {
            return _iStudentRepository.GetStudents(searchStudent.Email, searchStudent.FullName, searchStudent.Phone
               , searchStudent.Page, searchStudent.Limit);
        }

        public async Task<bool> CreateStudent(CreateStudent createStudent)
        {
            User newUser = new()
            {
                Email = createStudent.Email,
                IsActive = true,
                RoleId = 3
            };
            if (await _iUserRepository.InsertUser(newUser))
            {
                User user = _iUserRepository.GetUser(createStudent.Email);

                Student student = new()
                {
                    Id = user.Id,
                    FullName = createStudent.FullName,
                    Phone = createStudent.Phone,
                    Address = createStudent.Address,
                    Dob = createStudent.Dob,
                    OldSchoolId = createStudent.OldSchoolId,
                    Avatar = createStudent.Avatar,
                };

                if (await _iStudentRepository.InsertStudent(student))
                {
                    Wallet wallet = new()
                    {
                        Amount = 100,
                        StudentId = user.Id
                    };
                    if (await _iWalletRepository.InsertWallet(wallet)) return true;
                }
            }
            return false;
        }




        public User GetUserByEmail(string email)
        {
            User user = _iUserRepository.GetUser(email);
            if (user != null) return user;
            return null;
        }

        public User GetUserById(int userId)
        {
            User user = _iUserRepository.GetUser(userId);
            if (user != null) return user;
            return null;
        }



        public async Task<bool> UpdateUser(UpdateUser updateUser)
        {
            User newUser = _iUserRepository.GetUser(updateUser.Id);
            newUser.IsActive = updateUser.IsActive;

            return await _iUserRepository.UpdateUser(newUser);
        }
    }
}
