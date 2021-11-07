using Admission.Data.Models;
using Admission.Data.SQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface IOldSchoolService
    {
        IEnumerable<OldSchoolSQL> GetOldSchools();
    }
}
