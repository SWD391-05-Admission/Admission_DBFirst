using Admission.Bussiness.Request;
using Admission.Data.Repository;
using Admission.Data.SQLModels;
using System.Collections;

namespace Admission.Bussiness.Service
{
    public interface IUniversityService
    {
        UniversitySQL GetUniversity(int uniId);
        Hashtable GetUniversities(SearchUniversity searchUniversity);
    }

    public class UniversityService : IUniversityService
    {
        private readonly IUniversityRepository _iUniversityRepository;

        public UniversityService(IUniversityRepository iUniversityRepository)
        {
            _iUniversityRepository = iUniversityRepository;
        }

        public UniversitySQL GetUniversity(int uniId)
        {
            return _iUniversityRepository.GetUniversity(uniId, true);
        }

        public Hashtable GetUniversities(SearchUniversity searchUniversity)
        {
            return _iUniversityRepository.GetUniversities(searchUniversity.Page, searchUniversity.Limit, true);
        }
    }
}
