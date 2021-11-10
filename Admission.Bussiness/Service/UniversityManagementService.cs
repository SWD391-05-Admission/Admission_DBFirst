using Admission.Bussiness.Request;
using Admission.Data.Repository;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IUniversityManagementService
    {
        UniversitySQL GetUniversity(int uniId);
        Hashtable GetUniversities(SearchUniversity searchUniversity);
        Task<bool> CreateUniversity(CreateUniversity createUniversity);
        Task<bool> UpdateUniversity(UpdateUniversity updateUniversity);
    }

    public class UniversityManagementService : IUniversityManagementService
    {
        private readonly IUniversityRepository _iUniversityRepository;

        public UniversityManagementService(IUniversityRepository iUniversityRepository)
        {
            _iUniversityRepository = iUniversityRepository;
        }

        public UniversitySQL GetUniversity(int uniId)
        {
            return _iUniversityRepository.GetUniversity(uniId, null);
        }

        public Hashtable GetUniversities(SearchUniversity searchUniversity)
        {
            return _iUniversityRepository.GetUniversities(searchUniversity.Page, searchUniversity.Limit, null);
        }

        public Task<bool> CreateUniversity(CreateUniversity createUniversity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUniversity(UpdateUniversity updateUniversity)
        {
            throw new NotImplementedException();
        }
    }
}
