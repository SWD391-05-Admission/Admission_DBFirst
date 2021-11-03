using Admission.Bussiness.Request;
using Admission.Bussiness.Response;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface ICounselorService
    {
        Hashtable GetCounselorsForUser(SearchCounselor searchCounselor);
        CounselorRes GetCounselor(int counselorId);
        Task<bool> UpdateCounselor(int counselorId, UpdateCounselor updateCounselor);
    }
}
