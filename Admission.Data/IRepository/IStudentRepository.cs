using Admission.Data.Models;
using Admission.Data.SQLModels;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Data.IRepository
{
    public interface IStudentRepository
    {
        Student GetStudent(int studentId);
        UserStudent GetUserStudent(int studentId);
        Hashtable GetStudents(string email, string fullname, string phone, int page, int limit);
        Task<bool> InsertStudent(Student student);
        Task<bool> UpdateStudent(Student newStudent);
    }
}
