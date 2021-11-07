using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using System.Collections.Generic;
using System.Linq;

namespace Admission.Data.Repository
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public AdmissionRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public IEnumerable<AdmissionForm> GetAdmissionForms()
        {
            var adForms = _admissionsDBContext.AdmissionForms.ToList();
            if (adForms != null || adForms.Any()) return adForms;
            return null;
        }
    }
}
