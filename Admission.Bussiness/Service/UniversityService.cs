using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Data.IRepository;
using Admission.Data.SQLModels;
using System.Collections;

namespace Admission.Bussiness.Service
{
    public class UniversityService : IUniversityService
    {
        private readonly IUniversityRepository _iUniversityRepository;

        public UniversityService(IUniversityRepository iUniversityRepository)
        {
            _iUniversityRepository = iUniversityRepository;
        }

        public UniversitySQL GetUniversity(int uniId)
        {
            return _iUniversityRepository.GetUniversity(uniId, false);
        }

        public Hashtable GetUniversities(SearchUniversity searchUniversity)
        {
            return _iUniversityRepository.GetUniversities(searchUniversity.Page, searchUniversity.Limit, false);
        }
    }
}
