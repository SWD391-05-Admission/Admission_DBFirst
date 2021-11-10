using Admission.Data.Models.Context;
using Admission.Data.SQLModels;
using System.Collections.Generic;
using System.Linq;

namespace Admission.Data.Repository
{
    public interface IOldSchoolRepository
    {
        IEnumerable<OldSchoolSQL> GetOldSchools();
    }

    public class OldSchoolRepository : IOldSchoolRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public OldSchoolRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }
        public IEnumerable<OldSchoolSQL> GetOldSchools()
        {
            var oldSchools = _admissionsDBContext.OldSchools
                .Select(oldSchool => new OldSchoolSQL
                {
                    Id = oldSchool.Id,
                    Name = oldSchool.Name
                });
            if (oldSchools != null || oldSchools.Any()) return oldSchools;
            return null;
        }
    }
}
