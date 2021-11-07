using Admission.Bussiness.IService;
using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.SQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
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
