using Admission.Bussiness.Request;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface IUniversityManagementService
    {
        UniversitySQL GetUniversity(int uniId);
        Hashtable GetUniversities(SearchUniversity searchUniversity);
        Task<bool> CreateUniversity(CreateUniversity createUniversity);
        Task<bool> UpdateUniversity(UpdateUniversity updateUniversity);
    }
}
