using Admission.Data.Models;
using Admission.Data.Models.Context;
using System.Collections.Generic;
using System.Linq;

namespace Admission.Data.Repository
{
    public interface IMajorRepository
    {
        public IEnumerable<Major> GetMajors();
    }

    public class MajorRepository : IMajorRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public MajorRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public IEnumerable<Major> GetMajors()
        {
            IEnumerable<Major> majors = _admissionsDBContext.Majors.ToList();
            if (majors != null && majors.Any()) return majors;
            return null;
        }
    }
}
