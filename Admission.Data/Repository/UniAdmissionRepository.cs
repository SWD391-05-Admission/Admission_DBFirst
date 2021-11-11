using Admission.Data.Models;
using Admission.Data.Models.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface IUniAdmissionRepository
    {
        UniAdmission GetUniAdmission(int uniId, int admissionId);
        Task<bool> InsertUniAdmission(UniAdmission uniAdmission);
        Task<bool> DeleteUniAdmission(UniAdmission uniAdmission);
    }
    public class UniAdmissionRepository : IUniAdmissionRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public UniAdmissionRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public UniAdmission GetUniAdmission(int uniId, int admissionId)
        {
            return _admissionsDBContext.UniAdmissions
                .Where(uniAdmission => uniAdmission.UniversityId == uniId
                && uniAdmission.AdmissionId == admissionId)
                .FirstOrDefault();
        }
        public async Task<bool> InsertUniAdmission(UniAdmission uniAdmission)
        {
            if (uniAdmission == null) return false;
            await _admissionsDBContext.UniAdmissions.AddAsync(uniAdmission);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUniAdmission(UniAdmission uniAdmission)
        {
            _admissionsDBContext.UniAdmissions.Remove(uniAdmission);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
