using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Bussiness.Response;
using Admission.Data.IRepository;
using Admission.Data.Models;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _iStudentRepository;

        public StudentService(IStudentRepository iStudentRepository)
        {
            _iStudentRepository = iStudentRepository;
        }

        public StudentRes GetStudent(int studentId)
        {
            var userStudent = _iStudentRepository.GetUserStudent(studentId);
            if (userStudent != null)
            {
                return new StudentRes
                {
                    Id = userStudent.Id,
                    Email = userStudent.Email,
                    FullName = userStudent.FullName,
                    Phone = userStudent.Phone,
                    Avatar = userStudent.Avatar,
                    Address = userStudent.Address,
                    Dob = userStudent.Dob,
                    OldSchool = userStudent.OldSchool
                };
            }
            return null;
        }

        public async Task<bool> UpdateStudent(int studentId, UpdateStudent updateStudent)
        {
            var newStudent = _iStudentRepository.GetStudent(studentId);
            newStudent.FullName = updateStudent.FullName;
            newStudent.Phone = updateStudent.Phone;
            newStudent.Avatar = updateStudent.Avatar;
            newStudent.Address = updateStudent.Address;
            newStudent.Dob = updateStudent.Dob;
            newStudent.OldSchoolId = updateStudent.OldSchool;
            if (await _iStudentRepository.UpdateStudent(newStudent)) return true;
            return false;
        }
    }
}
