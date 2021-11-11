using Admission.Bussiness.Request;
using Admission.Bussiness.Response;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using Admission.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IUniAdmissionService
    {
        UniAdmission GetUniAdmission(int uniId, int admissionId);
        Task<bool> CreateUniAdmission(CreateUniAdmission createUniAdmission);
        Task<bool> DeleteUniAdmission(UniAdmission uniAdmission);
    }
    public class UniAdmissionService : IUniAdmissionService
    {
        private readonly IUniAdmissionRepository _iUniAdmissionRepository;

        public UniAdmissionService(IUniAdmissionRepository iUniAdmissionRepository)
        {
            _iUniAdmissionRepository = iUniAdmissionRepository;
        }

        public UniAdmission GetUniAdmission(int uniId, int admissionId)
        {
            return _iUniAdmissionRepository.GetUniAdmission(uniId, admissionId);
        }

        public async Task<bool> CreateUniAdmission(CreateUniAdmission createUniAdmission)
        {
            var uniAdmission = new UniAdmission()
            {
                UniversityId = createUniAdmission.UniversityId,
                AdmissionId = createUniAdmission.AdmissionId
            };

            return await _iUniAdmissionRepository.InsertUniAdmission(uniAdmission);
        }

        public async Task<bool> DeleteUniAdmission(UniAdmission uniAdmission)
        {
            return await _iUniAdmissionRepository.DeleteUniAdmission(uniAdmission);
        }
    }
}
