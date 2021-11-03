using Admission.Bussiness.Response;
using Admission.Data.Models;
using System.Collections.Generic;

namespace Admission.Bussiness.IService
{
    public interface IDistrictService
    {
        IEnumerable<DistrictRes> GetDistricts();
    }
}
