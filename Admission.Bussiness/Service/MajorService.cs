using Admission.Bussiness.IService;
using Admission.Bussiness.Response;
using Admission.Data.IRepository;
using Admission.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public class MajorService : IMajorService
    {
        private readonly IMajorRepository _iMajorRepository;
        public MajorService(IMajorRepository iMajorRepository)
        {
            _iMajorRepository = iMajorRepository;
        }
        public IEnumerable<MajorRes> GetMajors()
        {
            var majors = _iMajorRepository.GetMajors();
            if (majors != null)
            {
                List<MajorRes> result = new();
                foreach (Major major in majors)
                {
                    result.Add(new MajorRes
                    {
                        Id = major.Id,
                        Name = major.Name,
                        Description = major.Description
                    });
                }
                return result;
            }
            return null;
        }
    }
}
