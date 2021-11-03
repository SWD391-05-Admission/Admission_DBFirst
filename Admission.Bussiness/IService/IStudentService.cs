using Admission.Bussiness.Request;
using Admission.Bussiness.Response;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface IStudentService
    {
        StudentRes GetStudent(int studentId);
        Task<bool> UpdateStudent(int studentId, UpdateStudent updateStudent);
    }
}
