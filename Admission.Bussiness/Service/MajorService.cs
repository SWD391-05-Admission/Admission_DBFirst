using Admission.Bussiness.Response;
using Admission.Data.Models;
using Admission.Data.Repository;
using System.Collections.Generic;

namespace Admission.Bussiness.Service
{
    public interface IMajorService
    {
        public IEnumerable<MajorRes> GetMajors();
    }

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
