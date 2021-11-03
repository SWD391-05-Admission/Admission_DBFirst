using Admission.Data.Models;
using Admission.Data.SQLModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admission.Data.IRepository
{
    public interface IUniversityRepository
    {
        UniversitySQL GetUniversity(int uniId, bool isShowAll);
        Hashtable GetUniversities(int page, int limit, bool isShowAll);
        Task<bool> InsertUniversity(University university);
        Task<bool> UpdateUniversity(University university);
    }
}
