using Admission.Bussiness.Response;
using System.Collections.Generic;

namespace Admission.Bussiness.IService
{
    public interface IMajorService
    {
        public IEnumerable<MajorRes> GetMajors();
    }
}
