using Admission.Data.Repository;
using Admission.Data.SQLModels;
using System.Collections.Generic;

namespace Admission.Bussiness.Service
{
    public interface IOldSchoolService
    {
        IEnumerable<OldSchoolSQL> GetOldSchools();
    }
    public class OldSchoolService : IOldSchoolService
    {
        private readonly IOldSchoolRepository _iOldSchoolRepository;

        public OldSchoolService(IOldSchoolRepository iOldSchoolRepository)
        {
            _iOldSchoolRepository = iOldSchoolRepository;
        }
        public IEnumerable<OldSchoolSQL> GetOldSchools()
        {
            return _iOldSchoolRepository.GetOldSchools();
        }
    }
}
