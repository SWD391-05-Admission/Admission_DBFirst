using Admission.Bussiness.Response;
using System.Collections.Generic;

namespace Admission.Bussiness.IService
{
    public interface IAdmisstionService
    {
        IEnumerable<AdmissionFormRes> GetAdmissionForms();
    }
}
