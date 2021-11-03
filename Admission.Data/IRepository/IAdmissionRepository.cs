using Admission.Data.Models;
using System.Collections.Generic;

namespace Admission.Data.IRepository
{
    public interface IAdmissionRepository
    {
        IEnumerable<AdmissionForm> GetAdmissionForms();
    }
}
