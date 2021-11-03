using Admission.Data.Models;
using Admission.Data.SQLModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admission.Data.IRepository
{
    public interface ICounselorRepository
    {
        Counselor GetCounselor(int counselorId);
        UserCounselor GetUserCounselor(int counselorId);
        Hashtable GetCounselors(string email, string fullname, string phone, int page, int limit, bool isShowAll);
        Task<bool> InsertCounselor(Counselor counselor);
        Task<bool> UpdateCounselor(Counselor newCounselor);
    }
}
