using Admission.Data.Models;
using FirebaseAdmin.Auth;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface ILoginService
    {
        Task<UserRecord> GetUserRecord(string firebaseToken);
        User GetUser(UserRecord userRecord);
        Task<User> CreateUser(UserRecord userRecord, string app);
        string GenerateJWT(User localUserModel);
    }
}
