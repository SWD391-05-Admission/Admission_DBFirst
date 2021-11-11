using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IUniMajorService
    {
        UniMajor GetUniMajor(int uniId, int majorId);
        Task<bool> CreateUniMajor(CreateUniMajor createUniMajor);
        Task<bool> DeleteUniMajor(UniMajor uniMajor);
    }
    public class UniMajorService : IUniMajorService
    {
        private readonly IUniMajorRepository _iUniMajorRepository;

        public UniMajorService(IUniMajorRepository iUniMajorRepository)
        {
            _iUniMajorRepository = iUniMajorRepository;
        }

        public UniMajor GetUniMajor(int uniId, int majorId)
        {
            return _iUniMajorRepository.GetUniMajor(uniId, majorId);
        }

        public async Task<bool> CreateUniMajor(CreateUniMajor createUniMajor)
        {
            var uniMajor = new UniMajor()
            {
                UniversityId = createUniMajor.UniversityId,
                MajorId = createUniMajor.MajorId
            };

            return await _iUniMajorRepository.InsertUniMajor(uniMajor);
        }

        public async Task<bool> DeleteUniMajor(UniMajor uniMajor)
        {
            return await _iUniMajorRepository.DeleteUniMajor(uniMajor);
        }
    }

}
