using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.SQLModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface IUniversityService
    {
        UniversitySQL GetUniversity(int uniId);
        Hashtable GetUniversities(SearchUniversity searchUniversity);
    }
}
