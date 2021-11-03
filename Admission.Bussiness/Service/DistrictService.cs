using Admission.Bussiness.IService;
using Admission.Bussiness.Response;
using Admission.Data.IRepository;
using Admission.Data.Models;
using System.Collections.Generic;

namespace Admission.Bussiness.Service
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _iDistrictRepository;
        public DistrictService(IDistrictRepository iDistrictRepository)
        {
            _iDistrictRepository = iDistrictRepository;
        }

        public IEnumerable<DistrictRes> GetDistricts()
        {
            var districts = _iDistrictRepository.GetDistricts();
            if (districts != null)
            {
                List<DistrictRes> result = new();
                foreach (District district in districts)
                {
                    result.Add(new DistrictRes
                    {
                        Id = district.Id,
                        Name = district.Name
                    });
                }
                return result;
            }
            return null;
        }
    }
}
