using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Admission.Data.IRepository;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public class UniversityManagementService : IUniversityManagementService
    {
        private readonly IUniversityRepository _iUniversityRepository;

        public UniversityManagementService(IUniversityRepository iUniversityRepository)
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
