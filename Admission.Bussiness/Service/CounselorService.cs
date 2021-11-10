using Admission.Bussiness.Request;
using Admission.Bussiness.Response;
using Admission.Data.Repository;
using Admission.Data.SQLModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface ICounselorService
    {
        CounselorRes GetCounselor(int counselorId);
        Hashtable GetCounselors(SearchCounselor searchCounselor);
        Task<bool> UpdateCounselor(int counselorId, UpdateCounselor updateCounselor);
    }

    public class CounselorService : ICounselorService
    {
        private readonly ICounselorRepository _iCounselorRepository;

        public CounselorService(ICounselorRepository iCounselorRepository)
        {
            _iCounselorRepository = iCounselorRepository;
        }

        public Hashtable GetCounselors(SearchCounselor searchCounselor)
        {
            var counselorsHash = _iCounselorRepository.GetCounselors(searchCounselor.Email, searchCounselor.FullName, searchCounselor.Phone
                , searchCounselor.Page, searchCounselor.Limit, true);
            if (counselorsHash != null)
            {
                Hashtable result = new();

                IQueryable<UserCounselor> counselors = (IQueryable<UserCounselor>)counselorsHash["counselors"];

                List<CounselorRes> counselorsRes = new();
                foreach (var counselor in counselors)
                {
                    counselorsRes.Add(new CounselorRes
                    {
                        Id = counselor.Id,
                        Email = counselor.Email,
                        FullName = counselor.FullName,
                        Phone = counselor.Phone,
                        Avatar = counselor.Avatar,
                        Description = counselor.Description
                    });
                }
                result.Add("counselors", counselorsRes);
                result.Add("numPage", counselorsHash["numPage"]);

                return result;
            }
            return null;
        }

        public CounselorRes GetCounselor(int counselorId)
        {
            var userCounselor = _iCounselorRepository.GetUserCounselor(counselorId);
            if (userCounselor != null)
            {
                return new CounselorRes
                {
                    Id = userCounselor.Id,
                    Email = userCounselor.Email,
                    FullName = userCounselor.FullName,
                    Phone = userCounselor.Phone,
                    Avatar = userCounselor.Avatar,
                    Description = userCounselor.Description
                };
            }
            return null;
        }

        public async Task<bool> UpdateCounselor(int counselorId, UpdateCounselor updateCounselor)
        {
            var newCounselor = _iCounselorRepository.GetCounselor(counselorId);
            newCounselor.FullName = updateCounselor.FullName;
            newCounselor.Phone = updateCounselor.Phone;
            newCounselor.Avatar = updateCounselor.Avatar;
            newCounselor.Description = updateCounselor.Description;
            if (await _iCounselorRepository.UpdateCounselor(newCounselor)) return true;
            return false;
        }
    }
}
