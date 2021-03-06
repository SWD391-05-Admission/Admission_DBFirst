using Admission.Data.Models;
using Admission.Data.Models.Context;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface ICounselorRepository
    {
        Counselor GetCounselor(int counselorId);
        UserCounselor GetUserCounselor(int counselorId);
        Hashtable GetCounselors(string email, string fullname, string phone, int page, int limit, bool? isActive);
        Task<bool> InsertCounselor(Counselor counselor);
        Task<bool> UpdateCounselor(Counselor newCounselor);
    }

    public class CounselorRepository : ICounselorRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public CounselorRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public Counselor GetCounselor(int counselorId)
        {
            return _admissionsDBContext.Counselors.Where(user => user.Id == counselorId).FirstOrDefault();
        }

        public UserCounselor GetUserCounselor(int counselorId)
        {
            return (from counselor in _admissionsDBContext.Counselors
                    join user in _admissionsDBContext.Users
                    on counselor.Id equals user.Id
                    where user.Id == counselorId && user.RoleId == 2
                    select new UserCounselor
                    {
                        Id = user.Id,
                        Email = user.Email,
                        IsActive = user.IsActive,
                        RoleId = user.RoleId,
                        FullName = counselor.FullName,
                        Phone = counselor.Phone,
                        Avatar = counselor.Avatar,
                        Description = counselor.Description
                    }).FirstOrDefault();
        }

        public Hashtable GetCounselors(string email, string fullname, string phone, int page, int limit, bool? isActive)
        {
            var counselors = (from counselor in _admissionsDBContext.Counselors
                              join user in _admissionsDBContext.Users
                              on counselor.Id equals user.Id
                              where user.RoleId == 2
                              select new UserCounselor
                              {
                                  Id = user.Id,
                                  Email = user.Email,
                                  IsActive = user.IsActive,
                                  RoleId = user.RoleId,
                                  FullName = counselor.FullName,
                                  Phone = counselor.Phone,
                                  Avatar = counselor.Avatar,
                                  Description = counselor.Description
                              });

            string where = "";
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
            if (!string.IsNullOrEmpty(where)) counselors = counselors.Where(where);
            if (isActive != null) counselors = counselors.Where(counselor => counselor.IsActive == isActive);

            int count = counselors.Count();

            if (counselors != null && counselors.Any())
            {
                Hashtable result = new();
                if (page > 0 && limit > 0)
                {
                    counselors = counselors.Skip((page - 1) * limit).Take(limit);
                    result.Add("numPage", (int)Math.Ceiling(((float)count / limit)));
                }
                result.Add("counselors", counselors);
                return result;
            }
            return null;
        }

        public async Task<bool> InsertCounselor(Counselor counselor)
        {
            if (counselor == null) return false;
            await _admissionsDBContext.Counselors.AddAsync(counselor);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCounselor(Counselor newCounselor)
        {
            if (newCounselor == null) return false;
            Counselor counselor = _admissionsDBContext.Counselors.Where(s => s.Id == newCounselor.Id).FirstOrDefault();
            if (counselor == null) return false;
            counselor = newCounselor;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
