using Admission.Data.Models;
using Admission.Data.Models.Context;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface IStudentRepository
    {
        Student GetStudent(int studentId);
        UserStudent GetUserStudent(int studentId);
        Hashtable GetStudents(string email, string fullname, string phone, int page, int limit);
        Task<bool> InsertStudent(Student student);
        Task<bool> UpdateStudent(Student newStudent);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public StudentRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public Student GetStudent(int studentId)
        {
            return _admissionsDBContext.Students.Where(user => user.Id == studentId).FirstOrDefault();
        }

        public UserStudent GetUserStudent(int studentId)
        {
            return (from student in _admissionsDBContext.Students
                    join user in _admissionsDBContext.Users
                    on student.Id equals user.Id
                    where user.Id == studentId && user.RoleId == 3
                    select new UserStudent
                    {
                        Id = user.Id,
                        Email = user.Email,
                        IsActive = user.IsActive,
                        RoleId = user.RoleId,
                        FullName = student.FullName,
                        Phone = student.Phone,
                        Avatar = student.Avatar,
                        Address = student.Address,
                        Dob = student.Dob,
                        OldSchool = _admissionsDBContext.OldSchools
                        .Where(oldSchool => oldSchool.Id == student.OldSchoolId)
                        .Select(oldSchool => new OldSchoolSQL
                        {
                            Id = oldSchool.Id,
                            Name = oldSchool.Name
                        }).FirstOrDefault()
                    }).FirstOrDefault();
        }

        public Hashtable GetStudents(string email, string fullname, string phone, int page, int limit)
        {
            string where = "";
            var students = (from student in _admissionsDBContext.Students
                            join user in _admissionsDBContext.Users
                            on student.Id equals user.Id
                            where user.RoleId == 3
                            select new UserStudent
                            {
                                Id = user.Id,
                                Email = user.Email,
                                IsActive = user.IsActive,
                                RoleId = user.RoleId,
                                FullName = student.FullName,
                                Phone = student.Phone,
                                Avatar = student.Avatar,
                                Address = student.Address,
                                Dob = student.Dob,
                                OldSchool = _admissionsDBContext.OldSchools
                                .Where(oldSchool => oldSchool.Id == student.OldSchoolId)
                                .Select(oldSchool => new OldSchoolSQL
                                {
                                    Id = oldSchool.Id,
                                    Name = oldSchool.Name
                                }).FirstOrDefault()
                            });

            if (!string.IsNullOrEmpty(email))
            {
                where += "Email.Contains(\"" + email + "\")";
            }
            if (!string.IsNullOrEmpty(fullname))
            {
                if (!string.IsNullOrEmpty(where)) where += " OR ";
                where += "FullName.Contains(\"" + fullname + "\")";
            }
            if (!string.IsNullOrEmpty(phone))
            {
                if (!string.IsNullOrEmpty(where)) where += " OR ";
                where += "Phone.Contains(\"" + phone + "\")";
            }

            if (!string.IsNullOrEmpty(where)) students = students.Where(where);

            int count = students.Count();

            if (students != null && students.Any())
            {
                Hashtable result = new();
                if (page > 0 && limit > 0)
                {
                    students = students.Skip((page - 1) * limit).Take(limit);
                    result.Add("numPage", (int)Math.Ceiling(((float)count / limit)));
                }
                result.Add("students", students);
                return result;
            }
            return null;
        }

        public async Task<bool> InsertStudent(Student student)
        {
            if (student == null) return false;
            await _admissionsDBContext.Students.AddAsync(student);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStudent(Student newStudent)
        {
            if (newStudent == null) return false;
            Student student = _admissionsDBContext.Students.Where(s => s.Id == newStudent.Id).FirstOrDefault();
            if (student == null) return false;
            student = newStudent;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
