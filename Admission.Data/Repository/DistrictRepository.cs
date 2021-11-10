using Admission.Data.Models;
using Admission.Data.Models.Context;
using System.Collections.Generic;
using System.Linq;

namespace Admission.Data.Repository
{
    public interface IDistrictRepository
    {
        IEnumerable<District> GetDistricts();
    }

    public class DistrictRepository : IDistrictRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public DistrictRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public IEnumerable<District> GetDistricts()
        {
            IEnumerable<District> districts = _admissionsDBContext.Districts.ToList();
            if (districts != null && districts.Any()) return districts;
            return null;
        }
    }
}
