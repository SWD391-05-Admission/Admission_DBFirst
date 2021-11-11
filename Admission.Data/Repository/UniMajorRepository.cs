using Admission.Data.Models;
using Admission.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface IUniMajorRepository
    {
        UniMajor GetUniMajor(int uniId, int majorId);
        Task<bool> InsertUniMajor(UniMajor uniMajor);
        Task<bool> DeleteUniMajor(UniMajor uniMajor);
    }
    public class UniMajorRepository : IUniMajorRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public UniMajorRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public UniMajor GetUniMajor(int uniId, int majorId)
        {
            return _admissionsDBContext.UniMajors
                .Where(uniMajor => uniMajor.UniversityId == uniId
                && uniMajor.MajorId == majorId)
                .FirstOrDefault();
        }
        public async Task<bool> InsertUniMajor(UniMajor uniMajor)
        {
            if (uniMajor == null) return false;
            await _admissionsDBContext.UniMajors.AddAsync(uniMajor);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUniMajor(UniMajor uniMajor)
        {
            _admissionsDBContext.UniMajors.Remove(uniMajor);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
